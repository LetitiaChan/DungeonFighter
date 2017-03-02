/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:核心层： 资源动态加载管理器
 *
 *	Description:
 *		1.属于“脚本插件”，适用于任何项目。
 *		2.开发目的： 开发出具备“对象缓冲”功能的资源加载脚本。。
 *
 *	Date:2017.02.28
 *
 *	Version:
 *		1.0
 *
 *	Author:chenx
 *
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Kernal
{
    public class ResourcesMgr : MonoBehaviour
    {
        private static ResourcesMgr _Instance;                                 //本脚本私有单例实例
        private Hashtable _ht = null;                                           //容器键值对集合

        private ResourcesMgr()
        {
            _ht = new Hashtable();
        }

        /// <summary>
        /// 得到实例(单例)
        /// </summary>
        /// <returns></returns>
        public static ResourcesMgr GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("_ResourceMgr").AddComponent<ResourcesMgr>();
            }
            return _Instance;
        }

        /// <summary>
        /// 调用资源（带对象缓冲技术）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="isCatch"></param>
        /// <returns></returns>
        public T LoadResource<T>(string path, bool isCatch) where T : UnityEngine.Object
        {
            if (_ht.Contains(path))
            {
                return _ht[path] as T;
            }

            T TResource = Resources.Load<T>(path);
            if (TResource == null)
            {
                Debug.LogWarning(GetType() + "/GetInstance()/TResource 提取的资源找不到，请检查。 path=" + path);
            }
            else if (isCatch)
            {
                _ht.Add(path, TResource);
            }

            return TResource;
        }

        /// <summary>
        /// 调用资源（带对象缓冲技术）
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isCatch"></param>
        /// <returns></returns>
        public GameObject LoadAsset(string path, bool isCatch)
        {
            GameObject goObj = LoadResource<GameObject>(path, isCatch);
            GameObject goObjClone = GameObject.Instantiate<GameObject>(goObj);
            if (goObjClone == null)
            {
                Debug.LogWarning(GetType() + "/LoadAsset()/克隆资源不成功，请检查。 path=" + path);
            }
            return goObjClone;
        }
    }//Class_end
}
