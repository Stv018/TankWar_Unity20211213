using Fusion;
using UnityEngine;

/// <summary>
/// �l�u���ʳt�סB�s���ɶ�
/// </summary>
public class Bullet : MonoBehaviour
{
    #region ���
    [Header("���ʳt��"), Range(0, 100)]
    public float speed = 5;
    [Header("�s���ɶ�"), Range(0, 10)]
    public float lifeTime = 5;
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



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
