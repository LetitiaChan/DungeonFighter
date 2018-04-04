using UnityEngine;
using System.Collections;

namespace Kernal
{
    /// <summary>
    /// 资源动态加载管理器,“对象缓冲”
    /// </summary>
    public class ResourcesMgr : MonoBehaviour
    {
        private static ResourcesMgr _instance;
        private Hashtable _resCache = null;

        private ResourcesMgr()
        {
            _resCache = new Hashtable();
        }

        public static ResourcesMgr GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GameObject("_ResourceMgr").AddComponent<ResourcesMgr>();
            }
            return _instance;
        }

        public T LoadResource<T>(string path, bool isAddToCache) where T : UnityEngine.Object
        {
            if (_resCache.Contains(path))
            {
                return _resCache[path] as T;
            }

            T TResource = Resources.Load<T>(path);
            if (TResource == null)
            {
                Debug.LogWarning(GetType() + "/LoadResource()/TResource can not find, please check. Path = " + path);
            }
            else if (isAddToCache)
            {
                _resCache.Add(path, TResource);
            }

            return TResource;
        }

        //加载克隆资源（带对象缓冲技术）
        public GameObject LoadAsset(string path, bool isAddToCache)
        {
            GameObject go = LoadResource<GameObject>(path, isAddToCache);
            GameObject goClone = GameObject.Instantiate<GameObject>(go);
            if (goClone == null)
            {
                Debug.LogWarning(GetType() + "/LoadAsset()/GameObject Instantiate failed, please check. Path = " + path);
            }
            return goClone;
        }
    }
}
