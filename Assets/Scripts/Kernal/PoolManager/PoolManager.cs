using UnityEngine;
using System.Collections.Generic;

namespace Kernal
{
    /// <summary>
    /// 多模复合缓冲池管理器
    /// Description:
    ///     “多模复合缓冲池”管理器，含义就是可以管理多类型、多样式的游戏对象缓冲处理。
    ///     层级关系： 
    ///         PoolManger(管理大类)-->Pools(管理小类[多种类型游戏对象])-->PoolOption（单类型游戏对象管理）
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        public static Dictionary<string, Pools> PoolsArray = new Dictionary<string, Pools>();


        public static void Add(Pools pool)
        {
            if (PoolsArray.ContainsKey(pool.name)) return;
            PoolsArray.Add(pool.name, pool);
        }

        public static void DestroyAllInactive()
        {
            foreach (KeyValuePair<string, Pools> keyValue in PoolsArray)
            {
                keyValue.Value.DestoryUnused();
            }
        }
        void OnDestroy()
        {
            PoolsArray.Clear();
        }
    }
}
