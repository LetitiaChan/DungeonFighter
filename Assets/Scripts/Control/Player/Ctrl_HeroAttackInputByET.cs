/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层： 主角攻击输入，通过虚拟按键
 *
 *	Description:
 *		1.
 *
 *	Date:22017.02.26
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
using Model;

namespace Control
{
    public class Ctrl_HeroAttackInputByET : BaseControl
    {
        //#if UNITY_ANDROID || UNITY_IPHONE
        public static Ctrl_HeroAttackInputByET Instance;
        //事件：主角控制
        public static event del_PlayerControlWithStr evePlayerControl;

        void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 响应普通攻击
        /// </summary>
        public void ResponseATKByNormal()
        {
            if (evePlayerControl != null)
            {
                evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_NORMAL);
            }
        }

        /// <summary>
        /// 响应大招A
        /// </summary>
        public void ResponseATKByMagicA()
        {
            if (evePlayerControl != null)
            {
                evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_A);
            }
        }

        /// <summary>
        /// 响应大招B
        /// </summary>
        public void ResponseATKByMagicB()
        {
            if (evePlayerControl != null)
            {
                evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_B);
            }
        }

        /// <summary>
        /// 响应大招C
        /// </summary>
        public void ResponseATKByMagicC()
        {
            if (evePlayerControl != null)
            {
                evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_C);
            }
        }

        /// <summary>
        /// 响应大招D
        /// </summary>
        public void ResponseATKByMagicD()
        {
            if (evePlayerControl != null)
            {
                evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_D);
            }
        }
        //#endif
    }
}