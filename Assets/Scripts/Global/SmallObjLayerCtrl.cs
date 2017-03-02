/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:公共层: 层消隐技术 
 *
 *	Description:
 *		1.小物件远距离消隐，近距离显示。 
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

namespace Global
{
    public class SmallObjLayerCtrl : MonoBehaviour
    {
        public int intDisappearDistance = 10;                                  //消隐距离
        private float[] distanceArray = new float[32];

        void Start()
        {
            distanceArray[8] = intDisappearDistance;
            Camera.main.layerCullDistances = distanceArray;
        }

    }
}


