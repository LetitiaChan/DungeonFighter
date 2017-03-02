/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层:主城场景控制 
 *
 *	Description:
 *		1.
 *
 *	Date:
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

using Kernal;
using Global;

namespace Control
{
    public class Ctrl_MajorCityScenes : BaseControl
    {
        public AudioClip AcBackground;                                         //主城背景音乐


        void Start()
        {
            if (AcBackground != null)
            {
                AudioManager.PlayBackground(AcBackground);
            }
        }

    }
}


