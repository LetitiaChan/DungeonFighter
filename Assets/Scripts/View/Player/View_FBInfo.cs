using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using Control;

namespace View
{
    public class View_FBInfo : MonoBehaviour
    {
        public GameObject goFBInfoPanel;
        public Image ImgLevel;
        public Sprite[] spLevels;
        public Text txtKill_king;
        public Text txtKill_archer;
        public Text txtKill_mage;
        public Text txtKill_warrior;
        public Text txtAward_coin;
        public Text txtAward_exp;

        void Awake()
        {
            Ctrl_LevelTwoScenes.eveFB_End += DisplayFBInfo;
        }

        void Start()
        {
            HideFBInfo();
        }

        public void DisplayFBInfo()
        {
            if (goFBInfoPanel)
            {
                goFBInfoPanel.SetActive(true);
                goFBInfoPanel.transform.SetAsLastSibling();

                if (Ctrl_LevelTwoScenes.Instance && txtKill_king)
                {
                    txtKill_king.text = "X" + Ctrl_LevelTwoScenes.Instance.KillNum_king;
                    txtKill_archer.text = "X" + Ctrl_LevelTwoScenes.Instance.KillNum_archer;
                    txtKill_mage.text = "X" + Ctrl_LevelTwoScenes.Instance.KillNum_mage;
                    txtKill_warrior.text = "X" + Ctrl_LevelTwoScenes.Instance.KillNum_warrior;
                    txtAward_coin.text = "X" + Ctrl_LevelTwoScenes.Instance.AwardNum_coin;
                    txtAward_exp.text = "X" + Ctrl_LevelTwoScenes.Instance.AwardNum_exp;
                    SetLevel(Ctrl_LevelTwoScenes.Instance.FBLevel);
                }
            }
        }
        public void HideFBInfo()
        {
            goFBInfoPanel.SetActive(false);
        }

        private void SetLevel(int level)
        {
            if (ImgLevel)
            {
                if (spLevels[level])
                {
                    ImgLevel.gameObject.SetActive(true);
                    ImgLevel.sprite = spLevels[level];
                    ImgLevel.SetNativeSize();
                    ImgLevel.transform.localScale = new Vector3(3, 3, 3);
                    ImgLevel.transform.DOScale(new Vector3(2, 2, 2), 0.5f);
                }
                else
                    ImgLevel.gameObject.SetActive(false);
            }
        }

        public void FBConfirm()
        {
            Ctrl_HeroProperty.Instance.AddExp(Ctrl_LevelTwoScenes.Instance.AwardNum_exp);
            Ctrl_HeroProperty.Instance.AddGold(Ctrl_LevelTwoScenes.Instance.AwardNum_coin);
            Ctrl_PlayerUIResponse.Instance.PlayerRecoverLife();
            Ctrl_PlayerUIResponse.Instance.BackToMajorCity();
        }
    }
}