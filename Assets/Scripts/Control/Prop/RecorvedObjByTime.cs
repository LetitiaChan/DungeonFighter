/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层： 使用“对象缓冲池”技术，做按指定时间，回收对象
 *
 *	Description:
 *		1.
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
using System.Collections;
using System.Collections.Generic;

using Global;
using Kernal;

namespace Control
{
    public class RecorvedObjByTime : BaseControl
    {
        //回收时间
        public float FloRecoverTime = 1F;

        void OnEnable()
        {
            StartCoroutine("RecoverdGameObjectByTime");
        }

        void OnDisable()
        {
            StopCoroutine("RecoverdGameObjectByTime");
        }

        //void Start () 
        //{
        //    StartCoroutine("RecoverdGameObjectByTime");
        //}

        /// <summary>
        /// 回收对象，根据指定的时间点
        /// </summary>
        /// <returns></returns>
        IEnumerator RecoverdGameObjectByTime()
        {
            yield return new WaitForSeconds(FloRecoverTime);
            PoolManager.PoolsArray["_ParticalSys"].RecoverGameObjectToPools(this.gameObject);
        }
    }
}
