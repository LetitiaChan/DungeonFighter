/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层： 使得道具旋转
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
    public class MakeObjRotation : BaseControl
    {

        public float floRotateSpeed = 1F;                                      //旋转的速度


        void Update()
        {
            this.gameObject.transform.Rotate(Vector3.up, floRotateSpeed);
        }

    }
}
