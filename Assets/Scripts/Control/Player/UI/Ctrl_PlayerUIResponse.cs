using UnityEngine;
using System.Collections;
using Kernal;
using Global;
using Model;

namespace Control
{
    public class Ctrl_PlayerUIResponse : BaseControl
    {
        public static Ctrl_PlayerUIResponse Instance;

        void Awake()
        {
            Instance = this;
        }

        public void ExitGame()
        {
            StartCoroutine("HandleSavingGame");
        }

        IEnumerator HandleSavingGame()
        {
            SaveAndLoading.GetInstance().SaveGameProgress();
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }


        public void PlayerRecoverLife()
        {
            Ctrl_HeroProperty.Instance.RecoverLife();
            Ctrl_HeroAnimationCtrl.Instance.RecoverLife();
        }

        public void BackToMajorCity()
        {
            base.EnterNextScenes(Scenes.MajorCity);
        }

        public void HandleDrugRed()
        {
            float BloodBottleHP = 500f;
            if (CanDrugRed())
            {
                PlayerPackageDataProxy.GetInstance().DecreaseBloodBottleNum(1);
                Ctrl_HeroProperty.Instance.IncreaseHealthValues(BloodBottleHP);//1个血瓶加500HP
            }
        }

        public bool CanDrugRed()
        {
            if (PlayerPackageDataProxy.GetInstance().DisplayBloodBottleNum() > 0)
                return true;
            return false;
        }

    }
}