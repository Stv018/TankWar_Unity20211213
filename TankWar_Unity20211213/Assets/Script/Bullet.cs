using Fusion;
using UnityEngine;

/// <summary>
/// 子彈移動速度、存活時間
/// </summary>
public class Bullet : NetworkBehaviour//MonoBehaviour
{
    #region 欄位
    [Header("移動速度"), Range(0, 100)]
    public float speed = 5;
    [Header("存活時間"), Range(0, 10)]
    public float lifeTime = 5;
    

    /// <summary>
    /// 連線角色控制器
    /// </summary>
    private NetworkCharacterController ncc;

    #endregion

    #region 屬性
    /// <summary>
    /// 存活計時器
    /// </summary>
    [Networked]
    private TickTimer life { get; set; }
   
    #endregion

    #region 方法
    public void init()
    {
        //存活計時器 = 計時器.從秒數建立(連線執行器,存活時間)
        life = TickTimer.CreateFromSeconds(Runner, lifeTime);
    }

    #endregion

    /// <summary>
    /// NetWork Behavior 父類別提供的事件
    /// 連線用 固定更新 50FPS
    /// </summary>
    public override void FixedUpdateNetwork() 
    {
        //Runner 連線執行器
        //Expired() 是否到期
        //Despawn()刪除
        //Object 連線物件
        //如果計時器過期(為零) 就刪除 此連線物件
        //否則就移動
        
        

        if (life.Expired(Runner)) Runner.Despawn(Object);
        else transform.Translate(0, 0, speed * Runner.DeltaTime);
    
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
