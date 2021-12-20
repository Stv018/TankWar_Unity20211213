using UnityEngine;
using UnityEngine.UI;
//�ޥ�Fusion
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �s�u�򩳥ͦ���
/// </summary>
public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
//�s�u����^�I�����ARunner ���澹�B�z�欰��|�^�I����������k
//INetworkRunnerCallbacks���� �I�O�w�a�@�Ӫ�����@
{
    #region ���
    [Header("�ЫػP�[�J�ж����")]
    public InputField inputFieldCreateRoom;
    public InputField inputFieldJoinRoom;
    [Header("���a�����")]
    // public GameObject goPlayer;      //���s�u������
    public NetworkPrefabRef goPlayer;   //�s�u����n�令�o������

    [Header("�e���s�u")]
    public GameObject goCanvas;
    
    /// <summary>
    /// ���a��J���ж��W��
    /// </summary>
    private string roomNameInput;

    /// <summary>
    /// �s�u���澹
    /// </summary>
    private NetworkRunner runner;




    #endregion

    #region ��k
    /// <summary>
    /// ���s�I���A�Ыةж�
    /// </summary>
    public void BtnCreateRoom()
    {
        roomNameInput = inputFieldCreateRoom.text;
        print("�Ыةж�" + roomNameInput);
        StartGame(GameMode.Host);
    }

    /// <summary>
    /// ���s�I���A�Ыةж�
    /// </summary>
    public void BtnJoinRoom()
    {
        roomNameInput = inputFieldJoinRoom.text;
        print("�[�J�ж��G" + roomNameInput);
        StartGame(GameMode.Client);
    }

    // async �D�P�B�B�z
    /// <summary>
    /// �}�l�s�u�C��
    ///
    /// </summary>
    /// 
    /// <param name="mode">�s�u�Ҧ��G�D���B�Ȥ�</param>
    /// 
    public async void StartGame(GameMode mode)
    {
        print("<color=yellow>�}�l���ճs�u</color>");

        runner = gameObject.AddComponent<NetworkRunner>();  //�s�u���澹 = �K�[����<�s�u���澹>
        runner.ProvideInput = true;                         //�s�u���澹�A�O�_���ѿ�J

        await runner.StartGame(new StartGameArgs()
        {
            //
            GameMode = mode,
            //
            SessionName = roomNameInput,
            //SceneManager.GetActiveScene().buildIndex �p�G�������s�b��^
            Scene = SceneManager.GetActiveScene().buildIndex,
            //
            SceneObjectProvider = gameObject.AddComponent<NetworkSceneManagerDefault>()
            
        }) ;
        print("<color=yellow>�s�u����</color>");
        //goCanvas ���õe��
        goCanvas.SetActive(false);
        //print("�}�l�C��");
    }

    #region Fusion�^�I�禡�ϰ�
    /// <summary>
    /// INetworkRunnerCallbacks����
    /// �^�_�禡(�^�I�禡)
    /// throw new NotImplementedException();
    /// ��Alt+Shift+> ��� �R�� �b��ESC���X
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

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    /// <summary>
    /// ���a���\�i�J�ж�����
    /// </summary>
    /// <param name="runner">�s�u���澹</param>
    /// <param name="player">���a��T</param>
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        //NetworkPrefabRef �b�s�u���ɭԤ~�|�ݨ� ��Pun����
        //�s�u���澹.�ͦ�(NetworkPrefabRef����������,�y��,����,���a��T)
        //Y��>0 �קK���b�g��
        runner.Spawn(goPlayer, new Vector3(-5,1,-10), Quaternion.identity,player);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {

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
    /// �^�I   
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
