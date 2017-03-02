/*
 *
 *  Title:  学习“高级对象缓冲池”技术
 *          
 *          多模缓冲池管理器       
 * 
 *  Descripts: 
 *          
 *  Author: Liuguozhu
 *
 *  Date:  2015
 * 
 *
 *  Version: 0.1
 *
 *
 *  Modify Record:
 *        [描述版本修改记录]
 *
 *
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kernal
{
    public class Pools : MonoBehaviour
    {
        [HideInInspector]
        public Transform ThisGameObjectPosition;                                   //本类挂载游戏对象位置              
        public List<PoolOption> PoolOptionArrayLib = new List<PoolOption>();       //“单模缓冲池”集合容器
        public bool IsUsedTime = false;                                            //是否用“时间戳”

        void Awake()
        {
            PoolManager.Add(this);                                                 //加入“多模复合池”管理器
            ThisGameObjectPosition = transform;
            //预加载
            PreLoadGameObject();
        }

        void Start()
        {
            //开始时间戳处理
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
            //循环体为定义的“种类”数量
            for (int i = 0; i < PoolOptionArrayLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionArrayLib[i];
                //所有正在使用的活动状态游戏对象的时间戳减去10秒
                //检查每个活动状态的游戏对象名称的时间戳如果小于等于0，则进入禁用状态
                opt.AllActiveGameObjectTimeSubtraction();
            }//for_end    
        }

        /// <summary>
        /// 预加载
        /// </summary>
        public void PreLoadGameObject()
        {
            for (int i = 0; i < this.PoolOptionArrayLib.Count; i++)
            {              //“多模”集合
                PoolOption opt = this.PoolOptionArrayLib[i];                       //“单模”集合
                for (int j = opt.totalCount; j < opt.IntPreLoadNumber; j++)
                {
                    GameObject obj = opt.PreLoad(opt.Prefab, Vector3.zero, Quaternion.identity);
                    //所有预加载的游戏对象规定为Pool类所挂游戏对象的子对象。
                    obj.transform.parent = ThisGameObjectPosition;
                }
            }
        }

        /// <summary>
        ///  得到游戏对象，从缓冲池中（“多模”集合）
        /// 
        ///  功能描述： 
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

            //循环体为定义的“种类”数量
            for (int i = 0; i < PoolOptionArrayLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionArrayLib[i];
                if (opt.Prefab == prefab)
                {
                    //激活指定“预设”
                    obj = opt.Active(pos, rot);
                    if (obj == null) return null;

                    //所有激活的游戏对象必须是本类所挂空对象的子对象。
                    if (obj.transform.parent != ThisGameObjectPosition)
                    {
                        obj.transform.parent = ThisGameObjectPosition;
                    }
                }
            }//for_end

            return obj;
        }//BirthGameObject_end

        /// <summary>
        /// 收回游戏对象（“多模”集合）
        /// </summary>
        /// <param name="instance"></param>
        public void RecoverGameObjectToPools(GameObject instance)
        {
            for (int i = 0; i < this.PoolOptionArrayLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionArrayLib[i];
                //检查自己的每一类“池”中是否包含指定的“预设”对象。
                if (opt.ActiveGameObjectArray.Contains(instance))
                {
                    if (instance.transform.parent != ThisGameObjectPosition)
                        instance.transform.parent = ThisGameObjectPosition;
                    //特定“池”回收这个指定的对象。
                    opt.Deactive(instance);
                }
            }
        }

        /// <summary>
        /// 销毁无用的对象（“多模”集合）
        /// </summary>
        public void DestoryUnused()
        {
            for (int i = 0; i < this.PoolOptionArrayLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionArrayLib[i];
                opt.ClearUpUnused();
            }
        }

        /// <summary>
        /// 销毁指定数量的游戏对象
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="count"></param>
        public void DestoryPrefabCount(GameObject prefab, int count)
        {
            for (int i = 0; i < this.PoolOptionArrayLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionArrayLib[i];
                if (opt.Prefab == prefab)
                {
                    opt.DestoryCount(count);
                    return;
                }
            }

        }

        /// <summary>
        /// 当本脚本所挂载的游戏对象销毁时候，清空所有集合内全部数据
        /// </summary>
        public void OnDestroy()
        {
            //停止时间戳的检查
            if (IsUsedTime)
            {
                CancelInvoke("ProcessGameObject_NameTime");
            }
            for (int i = 0; i < this.PoolOptionArrayLib.Count; i++)
            {
                PoolOption opt = this.PoolOptionArrayLib[i];
                opt.ClearAllArray();
            }
        }

    }//Pool.cs_end


    /// <summary>
    /// 游戏（单类型）缓冲对象管理（即：单模池操作管理）
    ///          功能： 激活、收回、预加载等。
    /// </summary>
    [System.Serializable]
    public class PoolOption
    {
        public GameObject Prefab;                                                  //存储的“预设”
        public int IntPreLoadNumber = 0;                                           //初始缓冲数量
        public int IntAutoDeactiveGameObjectByTime = 30;                            //按时间自动禁用游戏对象

        [HideInInspector]
        public List<GameObject> ActiveGameObjectArray = new List<GameObject>();    //活动使用的游戏对象集合
        [HideInInspector]
        public List<GameObject> InactiveGameObjectArray = new List<GameObject>();   //非活动状态（禁用）的游戏对象集合
        private int _Index = 0;


        /// <summary>
        /// 预加载
        /// </summary>
        /// <param name="prefab">“预设”体</param>
        /// <param name="positon">位置</param>
        /// <param name="rotation">旋转</param>
        /// <returns></returns>
        internal GameObject PreLoad(GameObject prefab, Vector3 positon, Quaternion rotation)
        {
            GameObject obj = null;

            if (prefab)
            {
                obj = Object.Instantiate(prefab, positon, rotation) as GameObject;
                Rename(obj);
                obj.SetActive(false);                                              //设置非活动状态
                                                                                   //加入到“非活动游戏对象”集合中。
                InactiveGameObjectArray.Add(obj);
            }
            return obj;
        }

        /// <summary>
        /// 激活游戏对象
        /// </summary>
        /// <param name="pos">位置</param>
        /// <param name="rot">旋转</param>
        /// <returns></returns>
        internal GameObject Active(Vector3 pos, Quaternion rot)
        {
            GameObject obj;

            if (InactiveGameObjectArray.Count != 0)
            {
                //从“非活动游戏集合”容器中取出下标为0的游戏对象
                obj = InactiveGameObjectArray[0];
                //从“非活动游戏集合”容器中移除下标为0的游戏对象
                InactiveGameObjectArray.RemoveAt(0);
            }
            else
            {
                //“池”中没有多余的对象，则产生新的对象
                obj = Object.Instantiate(Prefab, pos, rot) as GameObject;
                //新的对象进行名称“格式化”处理
                Rename(obj);
            }
            //对象的方位处理
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            //新对象正式加入“活动池”容器中。
            ActiveGameObjectArray.Add(obj);
            obj.SetActive(true);

            return obj;
        }

        /// <summary>
        /// 禁用游戏对象
        /// </summary>
        /// <param name="obj"></param>
        internal void Deactive(GameObject obj)
        {
            ActiveGameObjectArray.Remove(obj);
            InactiveGameObjectArray.Add(obj);
            obj.SetActive(false);
        }

        /// <summary>
        /// 统计两个“池”中所有对象的数量
        /// </summary>
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

        /// <summary>
        /// 全部清空集合（两个“池”）
        /// </summary>
        internal void ClearAllArray()
        {
            ActiveGameObjectArray.Clear();
            InactiveGameObjectArray.Clear();
        }

        /// <summary>
        /// 彻底删除所有“非活动”集合容器中的游戏对象。
        /// </summary>
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
            instance.name = IntAutoDeactiveGameObjectByTime + "@" + instance.name;
            _Index++;
        }

        /// <summary>
        /// 删除“非活动”容器集合中的一部分指定数量数据
        /// </summary>
        /// <param name="count"></param>
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
        /// 　　　
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
                    //游戏对象自动转入禁用
                    goActiveObj.name = IntAutoDeactiveGameObjectByTime.ToString() + "@" + strTail;
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
    }//PoolTimeObject.cs_end

}