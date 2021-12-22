using Fusion;
using UnityEngine;

    /// <summary>
    /// 連線輸入資料
    /// 保存玩家輸入資訊
    /// </summary>
    
    // IN 裡面是結構，所以改成結構\struct
    public struct NetworkInputdata : INetworkInput
    {
        /// <summary>
        /// 坦克移動方向
        /// </summary>
        public Vector3 direction;
        
        /// <summary>
        /// 是否點擊左鍵
        /// </summary>
        public bool inputFire;
    }



