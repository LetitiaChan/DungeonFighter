using UnityEngine;
using UnityEngine.UI;
using Global;
using Control;

namespace View
{
    public class View_LogonScenes : MonoBehaviour
    {
        public GameObject goSwordHero;
        public GameObject goMagicHero;
        public GameObject goUISwordHeroInfo;
        public GameObject goUIMagicHeroInfo;
        public InputField inpUserName;


        void Start()
        {
            GlobalParaMgr.PlayerTypes = PlayerType.SwordHero;
            inpUserName.text = "李大侠";
        }


        public void ChangeToSwordHero()
        {
            goSwordHero.SetActive(true);
            goMagicHero.SetActive(false);
            goUISwordHeroInfo.SetActive(true);
            goUIMagicHeroInfo.SetActive(false);
            GlobalParaMgr.PlayerTypes = PlayerType.SwordHero;
            Ctrl_LogonScenes.Instance.PlayAudioEffectBySword();
        }

        public void ChangeToMagicHero()
        {
            goSwordHero.SetActive(false);
            goMagicHero.SetActive(true);
            goUISwordHeroInfo.SetActive(false);
            goUIMagicHeroInfo.SetActive(true);
            GlobalParaMgr.PlayerTypes = PlayerType.MagicHero;
            Ctrl_LogonScenes.Instance.PlayAudioEffectByMagic();
        }

        public void SubmitInfo()
        {
            GlobalParaMgr.PlayerName = inpUserName.text;
            Ctrl_LogonScenes.Instance.EnterNextScenes();
        }
    }
}
