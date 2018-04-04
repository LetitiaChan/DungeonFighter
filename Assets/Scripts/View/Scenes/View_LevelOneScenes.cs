using UnityEngine;
using System.Collections;
using Global;
using Control;
using Kernal;

namespace View
{
    public class View_LevelOneScenes : MonoBehaviour
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


            StartCoroutine("CheckEnemyClear");
        }

        IEnumerator CheckEnemyClear()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_5);
            while (GameObject.FindGameObjectsWithTag(Tag.Tag_Enemys).Length > 0 || TriggerDialogs.Instance.goBackground.activeSelf)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_5);
                //GameObject[] objs = GameObject.FindGameObjectsWithTag(Tag.Tag_Enemys);
                //DebugConsole.Log("CheckEnemyClear  Length = " + objs.Length);
            }

            View_PlayerInfoResponse.Instance.DisplayWelcomePanel();
            StopCoroutine("CheckEnemyClear");
        }

        public void EnterMajorCity()
        {
            Ctrl_LevelOneScenes.Instance.EnterNextScenes();
        }
    }
}
