/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层： 按照一定时间销毁道具
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

namespace Control
{
    public class DestroyObjByTime : BaseControl
    {
        public float floDestroyTime = 2F;                                      //销毁的时间

        void Start()
        {
            Destroy(this.gameObject, floDestroyTime);
        }
    }
}
