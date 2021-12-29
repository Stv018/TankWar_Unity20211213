using Fusion;
using UnityEngine;

/// <summary>
/// �l�u���ʳt�סB�s���ɶ�
/// </summary>
public class Bullet : NetworkBehaviour//MonoBehaviour
{
    #region ���
    [Header("���ʳt��"), Range(0, 100)]
    public float speed = 5;
    [Header("�s���ɶ�"), Range(0, 10)]
    public float lifeTime = 5;
    

    /// <summary>
    /// �s�u���ⱱ�
    /// </summary>
    private NetworkCharacterController ncc;

    #endregion

    #region �ݩ�
    /// <summary>
    /// �s���p�ɾ�
    /// </summary>
    [Networked]
    private TickTimer life { get; set; }
   
    #endregion

    #region ��k
    public void init()
    {
        //�s���p�ɾ� = �p�ɾ�.�q��ƫإ�(�s�u���澹,�s���ɶ�)
        life = TickTimer.CreateFromSeconds(Runner, lifeTime);
    }

    #endregion

    /// <summary>
    /// NetWork Behavior �����O���Ѫ��ƥ�
    /// �s�u�� �T�w��s 50FPS
    /// </summary>
    public override void FixedUpdateNetwork() 
    {
        //Runner �s�u���澹
        //Expired() �O�_���
        //Despawn()�R��
        //Object �s�u����
        //�p�G�p�ɾ��L��(���s) �N�R�� ���s�u����
        //�_�h�N����
        
        

        if (life.Expired(Runner)) Runner.Despawn(Object);
        else transform.Translate(0, 0, speed * Runner.DeltaTime);
    
    }

    /// <summary>
    /// ����
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
