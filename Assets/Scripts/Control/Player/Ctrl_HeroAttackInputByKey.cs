using UnityEngine;
using Global;

namespace Control
{
    /// <summary>
    /// 主角攻击输入，通过键盘方式
    /// </summary>
    public class Ctrl_HeroAttackInputByKey : BaseControl
    {
        //#if UNITY_STANDALONE_WIN || UNITY_EDITOR
#if UNITY_STANDALONE_WIN
        public static event del_PlayerControlWithStr evePlayerControl;

        void Update()
        {
            if (Input.GetButton(GlobalParameter.INPUT_MGR_ATTACKNAME_NORMAL))
            {
                if (evePlayerControl != null)
                {
                    //print("NormalAttack J");
                    evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_NORMAL);
                }
            }
            else if (Input.GetButtonDown(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_A))
            {
                //print("MagicTrickA K ");
                if (evePlayerControl != null)
                {
                    evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_A);
                }
            }
            else if (Input.GetButtonDown(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_B))
            {
                //print("MagicTrickB L ");
                if (evePlayerControl != null)
                {
                    evePlayerControl(GlobalParameter.INPUT_MGR_ATTACKNAME_MAGICTRICK_B);
                }
            }
        }
#endif
    }
}