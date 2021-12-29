using UnityEngine;
using Fusion;
using UnityEngine.UI;

/// <summary>
/// 坦克玩家控制器
/// 前後左右移動
/// 旋轉塔與發射子彈
/// </summary>
public class PlayerControl : NetworkBehaviour
{
    [Header("移動速度"), Range(0, 100)]
    public float speed = 7.5f;
    [Header("發射子彈間隔"), Range(0, 1.5f)]
    public float intervalFire = 0.35f;
    [Header("子彈物件")]
    public Bullet bullet;

    [Header("子彈生成位置")]
    public Transform pointFire;
    [Header("砲塔")]
    public Transform traTower;

    /// <summary>
    /// 聊天輸入區域
    /// </summary>
    private InputField inputMessage;

    /// <summary>
    /// 聊天訊息
    /// </summary>
    private Text textAllMessage;

    /// <summary>
    /// 連線角色控制器
    /// </summary>
    private NetworkCharacterController ncc;


    /// <summary>
    /// 開槍間隔計時器
    /// </summary>
    public TickTimer interval { get; set; }

    #region 事件
    private void Awake()
    {
        ncc = GetComponent<NetworkCharacterController>();
        textAllMessage = GameObject.Find("聊天訊息").GetComponent<Text>();
        inputMessage = GameObject.Find("聊天輸入區域").GetComponent<InputField>();
        inputMessage.onEndEdit.AddListener((string message)=>{ InputMessage(message); });

    }

    private void OnCollisionEnter(Collision collision)
    {
        //如果 碰到 物件名稱 包含 子彈 就刪除
        if (collision.gameObject.name.Contains("子彈")) Destroy(gameObject);
    }
    #endregion





    #region 方法

    /// <summary>
    /// 輸入訊息與同步訊息
    /// </summary>
    /// <param name="message"></param>
    private void InputMessage(string message) 
    {
        if (Object.HasInputAuthority)
        {
            RPC_SendMessage(message);
        }
    
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_SendMessage(string message,RpcInfo info = default) 
    {
        textAllMessage.text += message;
    
    }

    public override void FixedUpdateNetwork()
    {
        Move();
        Fire();
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        //如果有輸入資料
        if (GetInput(out NetworkInputdata dataInput))
        {
            //連線角色控制器.移動(速度*方向*連線一幀時間)
            ncc.Move(speed * dataInput.direction * Runner.DeltaTime);

            //取得滑鼠座標，並將Y指定與砲塔一樣的高度避免砲塔歪掉
            Vector3 positionMouse = dataInput.positionMouse;
            positionMouse.y = traTower.position.y;

            //砲塔 的前方軸向 =  滑鼠 - 坦克(向量)
            traTower.forward = positionMouse - transform.position;
        
        }
    }

    /// <summary>
    /// 開槍
    /// </summary>
    private void Fire()
    {
        if (GetInput(out NetworkInputdata dataInput))   //如果  有玩家輸入資料
        {
            if (interval.ExpiredOrNotRunning(Runner))   //如果 開槍間隔計時器 過期或沒有執行
            {

                if (dataInput.inputFire)            //如果 輸入資料是開槍左鍵
                {
                    interval = TickTimer.CreateFromSeconds(Runner, intervalFire);  
                    //建立計時器

                    //連線.生成(連線物件，座標，角度，輸入權限，匿名函式(執行器，生成物件)=>{})
                    Runner.Spawn(
                        bullet,
                        pointFire.position,
                        pointFire.rotation,
                        Object.InputAuthority,
                        (runner, objectSpawn) =>
                        {
                            objectSpawn.GetComponent<Bullet>().init();

                        });
                }
            }


        }
    }

    #endregion

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
