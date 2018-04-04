using UnityEngine;
using System.Collections;
using Kernal;

namespace Control
{
    /// <summary>
    /// 使用“对象缓冲池”技术，做按指定时间，回收对象
    /// </summary>
    public class RecorvedObjByTime : BaseControl
    {
        public float recoverTime = 1f;

        void OnEnable()
        {
            StartCoroutine("RecoverdGameObjectByTime");
        }

        void OnDisable()
        {
            StopCoroutine("RecoverdGameObjectByTime");
        }

        IEnumerator RecoverdGameObjectByTime()
        {
            yield return new WaitForSeconds(recoverTime);
            PoolManager.PoolsArray["_ParticalSys"].RecoverGameObjectToPools(this.gameObject);
        }
    }
}
