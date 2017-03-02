/*
 *
 *  Title:  学习“高级对象缓冲池”技术
 *  
 *          多模复合缓冲池管理器
 *          
 *  Descripts: 
 *          “多模复合缓冲池”管理器，含义就是可以管理多类型、多样式的游戏对象缓冲处理。
 *           层级关系是： 
 *               PoolManger(管理大类)-->Pools(管理小类[多种类型游戏对象])-->PoolOption（单类型游戏对象管理）
 *          
 *  Author: Liuguozhu
 *
 *  Date:  2015
 *
 *  Version: 0.1
 *
 *  Modify Record:
 *        [描述版本修改记录]
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kernal
{
    public class PoolManager : MonoBehaviour
    {
        //“缓冲池”集合
        public static Dictionary<string, Pools> PoolsArray = new Dictionary<string, Pools>();


        /// <summary>
        /// 加入“池”
        /// </summary>
        /// <param name="pool"></param>
        public static void Add(Pools pool)
        {
            if (PoolsArray.ContainsKey(pool.name)) return;
            PoolsArray.Add(pool.name, pool);
        }

        /// <summary>
        /// 删除不用的
        /// </summary>
        public static void DestroyAllInactive()
        {
            foreach (KeyValuePair<string, Pools> keyValue in PoolsArray)
            {
                keyValue.Value.DestoryUnused();
            }
        }

        /// <summary>
        /// 清空“池”
        /// </summary>
        void OnDestroy()
        {
            PoolsArray.Clear();
        }
    }
}