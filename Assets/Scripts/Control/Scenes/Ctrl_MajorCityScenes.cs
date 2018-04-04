using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kernal;
using Global;

namespace Control
{
    public class Ctrl_MajorCityScenes : BaseControl
    {
        public AudioClip AcBackground;

        IEnumerator Start()
        {
            ResetPlayer();

            AudioManager.AudioBackgroundVolumn = 0.3f;
            AudioManager.AudioEffectVolumn = 1;
            if (AcBackground != null)
                AudioManager.PlayBackground(AcBackground);

            if (GlobalParaMgr.CurGameType == CurrentGameType.Continue)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_2);
                SaveAndLoading.GetInstance().LoadingGame_PlayerData();
            }
            //StartCoroutine("TestXML");
        }

        private void ResetPlayer()
        {
            var player = GameObject.FindGameObjectWithTag(Tag.Player);
            if (player == null)
            {
                player = ResourcesMgr.GetInstance().LoadAsset("Prefabs/Player/SwordAnimatorHero", false);
            }
            var HeroStartPos = GameObject.Find("_Environment/HeroStartPos");
            if (HeroStartPos && player)
            {
                player.transform.position = HeroStartPos.transform.position;
                player.GetComponent<Ctrl_HeroAnimationCtrl>().HeroDisplayParticalEffect();
            }
        }

        //IEnumerator TestXML()
        //{
        //    XMLDialogsDataAnalysisMgr.GetInstance().SetXMLPathAndRootNodeName(KernalParameter.GetDialogConfigXMLPath(), KernalParameter.GetDialogConfigXMLRootNodeName());
        //    yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_2);
        //    List<DialogDataFormat> dialogsDataArray = XMLDialogsDataAnalysisMgr.GetInstance().GetAllXMLDatas();
        //    //print(GetType() + " 对话数据量：" + dialogsDataArray.Count);
        //    bool result = DialogDataMgr.GetInstance().LoadAllDialogData(dialogsDataArray);
        //    if (!result)
        //    {
        //        print(GetType() + "/ScenesPreProgressing_LevelOne()/‘对话数据管理器’加载数据失败");
        //    }
        //}
    }
}