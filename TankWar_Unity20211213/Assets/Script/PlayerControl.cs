using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fusion;

/// <summary>
/// �Z�J���a���
/// �e�ᥪ�k����
/// �����P�o�g�l�u
/// </summary>
public class PlayerControl: NetworkBehaviour//MonoBehaviour
{
    [Header("���ʳt��"), Range(0, 100)]
    public float speed = 7.5f;
    [Header("�o�g�l�u���j"), Range(0, 1.5f)]
    public float intervalFire = 0.35f;
    [Header("�l�u����")]
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
        //�p�G����J���
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
