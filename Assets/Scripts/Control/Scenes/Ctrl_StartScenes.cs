using UnityEngine;
using System.Collections;
using Global;
using Kernal;

namespace Control
{
    public class Ctrl_StartScenes : BaseControl
    {
        public static Ctrl_StartScenes Instance;
        public AudioClip AucBackground;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            AudioManager.AudioBackgroundVolumn = 0.5f;
            AudioManager.AudioEffectVolumn = 1;
            /*对于大的音频文件（背景音乐），采用PlayBackground(AudioClip audioClip)方式*/
            //AudioManager.PlayBackground("StartScenes");//方式1
            AudioManager.PlayBackground(AucBackground);//方式2

            XMLDialogsDataAnalysisMgr.GetInstance().SetXMLPathAndRootNodeName(KernalParameter.GetDialogConfigXMLPath(), KernalParameter.GetDialogConfigXMLRootNodeName());
            //DebugConsole.Log("XML解析 - 00000000000000000000000");
        }


        internal void ClickNewGame()
        {
            StartCoroutine("EnterNewGame");
        }

        internal void ClickGameContinue()
        {
            StartCoroutine("EnterContinueGame");
        }

        IEnumerator EnterNewGame()
        {
            FadeInAndOut.Instance.SetSceneToBlack();
            yield return new WaitForSeconds(1.5f);
            base.EnterNextScenes(Scenes.LogonScene);
        }

        IEnumerator EnterContinueGame()
        {
            SaveAndLoading.GetInstance().LoadingGame_GlobalParaData();

            FadeInAndOut.Instance.SetSceneToBlack();
            yield return new WaitForSeconds(1.5f);

            base.EnterNextScenes(GlobalParaMgr.NextSceneName);
        }
    }
}
