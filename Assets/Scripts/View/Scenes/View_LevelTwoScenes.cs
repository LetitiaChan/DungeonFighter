using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Global;
using Control;

namespace View
{
    public class View_LevelTwoScenes : MonoBehaviour
    {
        public GameObject goUINormalATK;
        public GameObject goUIMagicATK_A;
        public GameObject goUIMagicATK_B;
        public GameObject goUIMagicATK_C;
        public GameObject goUIMagicATK_D;

        IEnumerator Start()
        {
            View_PlayerInfoResponse.Instance.HideWelcomePanel();
            /*使用协程，保证View_ATKButtonCDEffect脚本先初始化*/
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT2);

            /* 大招的是否启用控制 */
            goUIMagicATK_A.GetComponent<View_ATKButtonCDEffect>().EnableSelf();
            goUIMagicATK_B.GetComponent<View_ATKButtonCDEffect>().EnableSelf();
            goUIMagicATK_C.GetComponent<View_ATKButtonCDEffect>().EnableSelf();
            goUIMagicATK_D.GetComponent<View_ATKButtonCDEffect>().DisableSelf();
        }
    }
}