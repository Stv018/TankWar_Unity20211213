using Fusion;
using UnityEngine;

/// <summary>
/// 子彈移動速度、存活時間
/// </summary>
public class Bullet : MonoBehaviour
{
    #region 欄位
    [Header("移動速度"), Range(0, 100)]
    public float speed = 5;
    [Header("存活時間"), Range(0, 10)]
    public float lifeTime = 5;
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



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
