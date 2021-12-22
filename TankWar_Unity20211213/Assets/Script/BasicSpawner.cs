using UnityEngine;
using UnityEngine.UI;
//引用Fusion
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 連線基底生成器
/// </summary>
public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
//連線執行回呼介面，Runner 執行器處理行為後會回呼此介面的方法
//INetworkRunnerCallbacks介面 點燈泡地一個直接實作
{
    #region 欄位
    [Header("創建與加入房間欄位")]
    public InputField inputFieldCreateRoom;
    public InputField inputFieldJoinRoom;
    [Header("玩家控制物件")]
    // public GameObject goPlayer;      //未連線的測試
    public NetworkPrefabRef goPlayer;   //連線之後要改成這個類型

    [Header("畫布連線")]
    public GameObject goCanvas;

    [Header("版本文字")]
    public Text textVersion;

    /// <summary>
    /// 玩家輸入的房間名稱
    /// </summary>
    private string roomNameInput;
    private string stringVersion = "Copyright Ce 2022. | Ver 1.0 ";


    private void Awake()
    {
        //W,H,是否為全螢幕
        Screen.SetResolution(1920, 1080, false);
        textVersion.text = stringVersion + Application.version;
    }

    /// <summary>
    /// 連線執行器
    /// </summary>
    private NetworkRunner runner;

    private Dictionary<PlayerRef, NetworkObject> players = new Dictionary<PlayerRef, NetworkObject>();

    [Header("玩家生成位置")]
    public Transform[] traSpawnPoint;


    #endregion

    #region 方法
    /// <summary>
    /// 按鈕點擊，創建房間
    /// </summary>
    public void BtnCreateRoom()
    {
        roomNameInput = inputFieldCreateRoom.text;
        print("創建房間" + roomNameInput);
        StartGame(GameMode.Host);
    }

    /// <summary>
    /// 按鈕點擊，創建房間
    /// </summary>
    public void BtnJoinRoom()
    {
        roomNameInput = inputFieldJoinRoom.text;
        print("加入房間：" + roomNameInput);
        StartGame(GameMode.Client);
    }

    // async 非同步處理
    /// <summary>
    /// 開始連線遊戲
    ///
    /// </summary>
    /// 
    /// <param name="mode">連線模式：主機、客戶</param>
    /// 
    public async void StartGame(GameMode mode)
    {
        print("<color=yellow>開始測試連線</color>");

        runner = gameObject.AddComponent<NetworkRunner>();  //連線執行器 = 添加元件<連線執行器>
        runner.ProvideInput = true;                         //連線執行器，是否提供輸入

        await runner.StartGame(new StartGameArgs()
        {
            //
            GameMode = mode,
            //
            SessionName = roomNameInput,
            //SceneManager.GetActiveScene().buildIndex 如果場景不存在返回
            Scene = SceneManager.GetActiveScene().buildIndex,
            //
            SceneObjectProvider = gameObject.AddComponent<NetworkSceneManagerDefault>()

        });
        print("<color=yellow>連線完成</color>");
        //goCanvas 隱藏畫布
        goCanvas.SetActive(false);
        //print("開始遊戲");
    }

    #region Fusion回呼函式區域
    /// <summary>
    /// INetworkRunnerCallbacks介面
    /// 回復函式(回呼函式)
    /// throw new NotImplementedException();
    /// 用Alt+Shift+> 選取 刪除 在按ESC跳出
    /// </summary>
    /// <param name="runner"></param>
    public void OnConnectedToServer(NetworkRunner runner)
    {

    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {

    }



    /// <summary>
    /// 玩家連線輸入行為
    /// </summary>
    /// <param name="runner">連線執行器</param>
    /// <param name="input">輸入資訊</param>
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        //新增連線輸入資料 結構 
        NetworkInputdata inputData = new NetworkInputdata();

        #region 自訂輸入按鍵與移動資訊
        //上下左右
        if (Input.GetKey(KeyCode.W)) inputData.direction += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) inputData.direction += Vector3.back;
        if (Input.GetKey(KeyCode.A)) inputData.direction += Vector3.left;
        if (Input.GetKey(KeyCode.D)) inputData.direction += Vector3.right;
        
        //左艦發射
        inputData.inputFire = Input.GetKey(KeyCode.Mouse0);
        
        input.Set(inputData);           //輸入資訊.設定(連線輸入資料)

        #endregion

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    /// <summary>
    /// 玩家資料集合：玩家參考資訊、玩家連線物件
    /// 字典有Key可以直接抓取key值
    /// 
    /// </summary>
   
    /// <summary>
    /// 當玩家成功進入房間之後
    /// </summary>
    /// <param name="runner">連線執行器</param>
    /// <param name="player">玩家資訊</param>
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        int randSpawnPoint = UnityEngine.Random.Range(0, traSpawnPoint.Length);
        //NetworkPrefabRef 在連線的時候才會看到 跟Pun類似
        //Y給>0 避免插在土裡
        //連線執行器.生成(NetworkPrefabRef類型的物件,座標,角度,玩家資訊)
        //runner.Spawn(goPlayer, new Vector3(-5,1,-10), Quaternion.identity,player);
        NetworkObject playerNetworkObject = runner.Spawn(goPlayer, traSpawnPoint[randSpawnPoint].position, Quaternion.identity, player);
        //將玩家參考資訊與玩家連線物件添加到字典集合中
        players.Add(player, playerNetworkObject);
    }

    /// <summary>
    /// 當玩家離開房間後
    /// </summary>
    /// <param name="runner"></param>
    /// <param name="player"></param>
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        //如果離開的玩家連線物件存在就刪除   =>利用抓取Key值，刪除該物件
        if (players.TryGetValue(player,out NetworkObject playerNetworkObject))
        {
            runner.Despawn(playerNetworkObject);    //連線執行器.取消生成(該玩家連線物件移除)
            players.Remove(player);                 //玩家集合.移除(該玩家)
        }
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {

    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }
    /// 回呼   
    #endregion

    #endregion




    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
