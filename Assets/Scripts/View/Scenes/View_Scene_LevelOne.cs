/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:视图层：第一关卡场景
 *
 *	Description:
 *		1.第一关卡场景的界面控制
 *
 *	Date:2017.02.27
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

using Global;
using Kernal;

namespace View
{
    public class View_Scene_LevelOne : MonoBehaviour
    {
        public GameObject goUINormalATK;                                       //普通攻击
        public GameObject goUIMagicATK_A;                                      //大招A
        public GameObject goUIMagicATK_B;                                      //大招B
        public GameObject goUIMagicATK_C;                                      //大招C
        public GameObject goUIMagicATK_D;                                      //大招D

        IEnumerator Start()
        {
            /* 大招的是否启用控制 */
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT2);
            goUIMagicATK_A.GetComponent<View_ATKButtonCDEffect>().EnableSelf();//启用
            goUIMagicATK_B.GetComponent<View_ATKButtonCDEffect>().EnableSelf();//启用
            goUIMagicATK_C.GetComponent<View_ATKButtonCDEffect>().DisableSelf();//不启用
            goUIMagicATK_D.GetComponent<View_ATKButtonCDEffect>().DisableSelf();//不启用
        }
    }
}
