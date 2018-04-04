using UnityEngine;
using System.Collections.Generic;

namespace Kernal
{
    /// <summary>
    /// 多模缓冲池管理器
    /// </summary>
    public class Pools : MonoBehaviour
    {
        public List<PoolOption> PoolOptionLib = new List<PoolOption>();
        public bool IsUsedTime = false;
        private Transform ThisGameObjectPosition;

        void Awake()
        {
            PoolManager.Add(this);
            ThisGameObjectPosition = transform;
            PreLoadGameObject();
        }

        void Start()
        {
            if (IsUsedTime)
            {
                InvokeRepeating("ProcessGameObject_NameTime", 1F, 10F);
            }
        }

        /// <summary>
        /// 时间戳处理
        /// 主要业务逻辑:
        /// 1>： 每间隔10秒种，对所有正在使用的活动状态游戏对象的时间戳减去10秒。
        /// 2>:  检查每个活动状态的游戏对象名称的时间戳如果小于等于0，则进入禁用状态。
        /// 3>:  重新进入活动状态的游戏对象，获得预先设定的存活时间写入对象名称的时间戳中。
        /// </summary>
        void ProcessGameObject_NameTime()
        {
            for (int i = 0; i < PoolOptionLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionLib[i];
                //所有正在使用的活动状态游戏对象的时间戳减去10秒
                //检查每个活动状态的游戏对象名称的时间戳如果小于等于0，则进入禁用状态
                opt.AllActiveGameObjectTimeSubtraction();
            }
        }

        public void PreLoadGameObject()
        {
            for (int i = 0; i < this.PoolOptionLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionLib[i];
                for (int j = opt.totalCount; j < opt.preLoadNumber; j++)
                {
                    GameObject obj = opt.PreLoad(opt.Prefab, Vector3.zero, Quaternion.identity);
                    obj.transform.parent = ThisGameObjectPosition;
                }
            }
        }

        /// <summary>
        ///  得到游戏对象，从缓冲池中（“多模”集合）
        /// 
        /// 功能描述： 
        ///     1： 对指定“预设”在自己的缓冲池中激活一个，且加入自己缓冲池中的"可用激活池"。
        ///     2： 然后再建立一个池对象，且激活预设，再加入自己的缓冲池中的“可用激活池”中。
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <returns></returns>
        public GameObject GetGameObjectByPool(GameObject prefab, Vector3 pos, Quaternion rot)
        {
            GameObject obj = null;

            for (int i = 0; i < PoolOptionLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionLib[i];
                if (opt.Prefab == prefab)
                {
                    obj = opt.Active(pos, rot);
                    if (obj == null) return null;

                    if (obj.transform.parent != ThisGameObjectPosition)
                    {
                        obj.transform.parent = ThisGameObjectPosition;
                    }
                }
            }

            return obj;
        }

        public void RecoverGameObjectToPools(GameObject instance)
        {
            for (int i = 0; i < this.PoolOptionLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionLib[i];
                if (opt.ActiveGameObjectArray.Contains(instance))
                {
                    if (instance.transform.parent != ThisGameObjectPosition)
                        instance.transform.parent = ThisGameObjectPosition;
                    opt.Deactive(instance);
                }
            }
        }

        public void DestoryUnused()
        {
            for (int i = 0; i < this.PoolOptionLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionLib[i];
                opt.ClearUpUnused();
            }
        }

        public void DestoryPrefabCount(GameObject prefab, int count)
        {
            for (int i = 0; i < this.PoolOptionLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionLib[i];
                if (opt.Prefab == prefab)
                {
                    opt.DestoryCount(count);
                    return;
                }
            }

        }

        public void OnDestroy()
        {
            if (IsUsedTime)
            {
                CancelInvoke("ProcessGameObject_NameTime");
            }
            for (int i = 0; i < this.PoolOptionLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionLib[i];
                opt.ClearAllArray();
            }
        }

    }


    /// <summary>
    /// 游戏（单类型）缓冲对象管理（即：单模池操作管理）
    ///          功能： 激活、收回、预加载等。
    /// </summary>
    [System.Serializable]
    public class PoolOption
    {
        public GameObject Prefab;
        public int preLoadNumber = 0;
        public int autoDeactiveGameObjectByTime = 30;

        [HideInInspector]
        public List<GameObject> ActiveGameObjectArray = new List<GameObject>();
        [HideInInspector]
        public List<GameObject> InactiveGameObjectArray = new List<GameObject>();
        private int _Index = 0;


        internal GameObject PreLoad(GameObject prefab, Vector3 positon, Quaternion rotation)
        {
            GameObject obj = null;

            if (prefab)
            {
                obj = Object.Instantiate(prefab, positon, rotation) as GameObject;
                Rename(obj);
                obj.SetActive(false);
                InactiveGameObjectArray.Add(obj);
            }
            return obj;
        }

        internal GameObject Active(Vector3 pos, Quaternion rot)
        {
            GameObject obj;

            if (InactiveGameObjectArray.Count != 0)
            {
                obj = InactiveGameObjectArray[0];
                InactiveGameObjectArray.RemoveAt(0);
            }
            else
            {
                obj = Object.Instantiate(Prefab, pos, rot) as GameObject;
                Rename(obj);
            }
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            ActiveGameObjectArray.Add(obj);
            obj.SetActive(true);

            return obj;
        }

        internal void Deactive(GameObject obj)
        {
            ActiveGameObjectArray.Remove(obj);
            InactiveGameObjectArray.Add(obj);
            obj.SetActive(false);
        }

        internal int totalCount
        {
            get
            {
                int count = 0;
                count += this.ActiveGameObjectArray.Count;
                count += this.InactiveGameObjectArray.Count;
                return count;
            }
        }

        internal void ClearAllArray()
        {
            ActiveGameObjectArray.Clear();
            InactiveGameObjectArray.Clear();
        }

        internal void ClearUpUnused()
        {
            foreach (GameObject obj in InactiveGameObjectArray)
            {
                Object.Destroy(obj);
            }

            InactiveGameObjectArray.Clear();
        }

        /// <summary>
        /// 游戏对象重命名
        /// 对新产生的游戏对象做统一格式处理，目的是做“时间戳”处理。
        /// </summary>
        /// <param name="instance"></param>    
        private void Rename(GameObject instance)
        {
            instance.name += (_Index + 1).ToString("#000");
            //游戏对象（自动禁用）时间戳  [Adding]
            instance.name = autoDeactiveGameObjectByTime + "@" + instance.name;
            _Index++;
        }

        internal void DestoryCount(int count)
        {
            if (count > InactiveGameObjectArray.Count)
            {
                ClearUpUnused();
                return;
            }
            for (int i = InactiveGameObjectArray.Count - 1; i >= InactiveGameObjectArray.Count - count; i--)
            {

                Object.Destroy(InactiveGameObjectArray[i]);
            }
            InactiveGameObjectArray.RemoveRange(InactiveGameObjectArray.Count - count, count);
        }

        /// <summary>
        /// 回调函数：时间戳管理
        /// 功能：所有游戏对象进行时间倒计时管理，时间小于零则进行“非活动”容器集合中，即:按时间自动回收游戏对象。
        /// </summary>
        internal void AllActiveGameObjectTimeSubtraction()
        {
            for (int i = 0; i < ActiveGameObjectArray.Count; i++)
            {
                string strHead = null;
                string strTail = null;
                int intTimeInfo = 0;
                GameObject goActiveObj = null;

                goActiveObj = ActiveGameObjectArray[i];
                //得到每个对象的时间戳
                string[] strArray = goActiveObj.name.Split('@');
                strHead = strArray[0];
                strTail = strArray[1];

                //时间戳-10 处理
                intTimeInfo = System.Convert.ToInt32(strHead);
                if (intTimeInfo >= 10)
                {
                    strHead = (intTimeInfo - 10).ToString();
                }
                else if (intTimeInfo <= 0)
                {
                    goActiveObj.name = autoDeactiveGameObjectByTime.ToString() + "@" + strTail;
                    this.Deactive(goActiveObj);
                    continue;
                }
                //时间戳重新生成
                goActiveObj.name = strHead + '@' + strTail;
            }
        }

    }//PoolOption.cs_end


    /// <summary>
    /// 内部类： 池时间
    /// </summary>
    //[System.Serializable]
    public class PoolTimeObject
    {
        public GameObject instance;
        public float time;
    }
}
