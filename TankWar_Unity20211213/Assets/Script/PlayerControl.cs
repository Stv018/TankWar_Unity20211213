using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fusion;

/// <summary>
/// 坦克玩家控制器
/// 前後左右移動
/// 旋轉塔與發射子彈
/// </summary>
public class PlayerControl: NetworkBehaviour//MonoBehaviour
{
    [Header("移動速度"), Range(0, 100)]
    public float speed = 7.5f;
    [Header("發射子彈間隔"), Range(0, 1.5f)]
    public float intervalFire = 0.35f;
    [Header("子彈物件")]
    public GameObject bullet;


    private NetworkCharacterController ncc;

    private void Awake()
    {
        ncc = GetComponent<NetworkCharacterController>();

    }

    public override void FixedUpdateNetwork()
    {
       Move();
    }


    private void Move()
    {
        //如果有輸入資料
        if (GetInput(out NetworkInput dataInput))
        {
            //
            ncc.Move(speed * dataInput.direction * Runner.DeltaTime);
        
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
