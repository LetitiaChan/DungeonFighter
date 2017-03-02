/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层：主角攻击输入，通过键盘方式
 *
 *	Description:
 *		1.
 *
 *	Date:2017.02.22
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

namespace Control
{
    public class Ctrl_HeroAttackInputByKey : BaseControl
    {
        //#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        //事件：主角控制
        public static event del_PlayerControlWithStr evePlayerControl;

        void Update()
        {
            if (Input.GetButtonDown(GlobalParameter.INPUT_MGR_ATTACKNAME_NORMAL))
            {
                print("NormalAttack J");
                if (evePlayerControl != null)
                {
                    evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_NORMAL);
                }
            }
            else if (Input.GetButtonDown(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_A))
            {
                print("MagicTrickA K ");
                if (evePlayerControl != null)
                {
                    evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_A);
                }
            }
            else if (Input.GetButtonDown(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_B))
            {
                print("MagicTrickB L ");
                if (evePlayerControl != null)
                {
                    evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_B);
                }
            }
        }
        //#endif
    }
}