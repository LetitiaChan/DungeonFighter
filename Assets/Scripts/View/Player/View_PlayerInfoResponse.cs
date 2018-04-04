using UnityEngine;
using UnityEngine.UI;
using Global;
using Control;
using Model;

namespace View
{
    public class View_PlayerInfoResponse : MonoBehaviour
    {
        public static View_PlayerInfoResponse Instance;
        public GameObject goPlayerDetailInfoPanel;
        public GameObject goWelcomePanel;
        public GameObject goRecoverPanel;
        public GameObject goET;
        public GameObject goHeroInfo;
        //定义攻击虚拟按键
        public GameObject goNormalATK;
        public GameObject goMagicA;
        public GameObject goMagicB;
        public GameObject goMagicC;
        public GameObject goMagicD;
        public GameObject goAddingHP;

        private Transform DrugRedNum;

        void Awake()
        {
            Instance = this;
            PlayerPackageData.evePlayerPackageData += DisplayProp;
        }

        void Start()
        {
            //DisplayET();
            DrugRedNum = goAddingHP.transform.Find("txtDrugNum");
            if (DrugRedNum && DrugRedNum.GetComponent<Text>())
                DrugRedNum.GetComponent<Text>().text = PlayerPackageDataProxy.GetInstance().DisplayBloodBottleNum().ToString();
        }

        public void DisplayPlayerRoles()
        {
            if (goPlayerDetailInfoPanel != null)
            {
                BeforeOpenWindow(goPlayerDetailInfoPanel);
                goPlayerDetailInfoPanel.SetActive(true);
            }
        }
        public void HidePlayerRoles()
        {
            if (goPlayerDetailInfoPanel != null)
            {
                BeforeCloseWindow();
                goPlayerDetailInfoPanel.SetActive(false);
            }
        }

        public void DisplayWelcomePanel()
        {
            if (goWelcomePanel != null)
            {
                BeforeOpenWindow(goWelcomePanel);
                goWelcomePanel.SetActive(true);
            }
        }
        public void HideWelcomePanel()
        {
            if (goWelcomePanel != null)
            {
                BeforeCloseWindow();
                goWelcomePanel.SetActive(false);
            }
        }

        public void DisplayRecoverPanel()
        {
            if (goRecoverPanel != null)
            {
                BeforeOpenWindow(goRecoverPanel);
                goRecoverPanel.SetActive(true);
            }
        }
        public void HideRecoverPanel()
        {
            if (goRecoverPanel != null)
            {
                BeforeCloseWindow();
                goRecoverPanel.SetActive(false);
            }
        }

        public void DisplayET()
        {
            goET.SetActive(true);
        }

        public void HideET()
        {
            if (Ctrl_HeroAnimationCtrl.Instance)
                Ctrl_HeroAnimationCtrl.Instance.CancelRunningState();
            goET.SetActive(false);
        }

        public void DisplayAllUIKey()
        {
            goNormalATK.SetActive(true);
            goMagicA.SetActive(true);
            goMagicB.SetActive(true);
            goMagicC.SetActive(true);
            goMagicD.SetActive(true);
            goAddingHP.SetActive(false);
        }

        public void HideAllUIKey()
        {
            goNormalATK.SetActive(false);
            goMagicA.SetActive(false);
            goMagicB.SetActive(false);
            goMagicC.SetActive(false);
            goMagicD.SetActive(false);
            goAddingHP.SetActive(false);
        }

        public void DisplayMainATK()
        {
            goNormalATK.SetActive(true);
            goMagicA.SetActive(false);
            goMagicB.SetActive(false);
            goMagicC.SetActive(false);
            goMagicD.SetActive(false);
            goAddingHP.SetActive(false);
        }

        public void DisplayHeroUIInfo()
        {
            goHeroInfo.SetActive(true);
        }

        public void HideHeroUIInfo()
        {
            goHeroInfo.SetActive(false);
        }

        public void ExitGame()
        {
            Ctrl_PlayerUIResponse.Instance.ExitGame();
        }


        private void BeforeOpenWindow(GameObject goNeedDisplayPanel)
        {
            HideET();
            GetComponent<UIMaskMgr>().SetMaskWindow(goNeedDisplayPanel);
        }

        private void BeforeCloseWindow()
        {
            DisplayET();
            GetComponent<UIMaskMgr>().CancleMaskWindow();
        }


        //#if UNITY_ANDROID || UNITY_IPHONE
        #region 相应玩家虚拟按键点击
        //普攻方法已写在虚拟按键Pressed状态脚本事件里
        //public void ResponseNormalATK()
        //{
        //    Ctrl_HeroAttackInputByET.Instance.ResponseATKByNormal();
        //}

        public void ResponseATKByMagicA()
        {
            Ctrl_HeroAttackInputByET.Instance.ResponseATKByMagicA();
        }

        public void ResponseATKByMagicB()
        {
            Ctrl_HeroAttackInputByET.Instance.ResponseATKByMagicB();
        }

        public void ResponseATKByMagicC()
        {
            Ctrl_HeroAttackInputByET.Instance.ResponseATKByMagicC();
        }

        public void ResponseATKByMagicD()
        {
            Ctrl_HeroAttackInputByET.Instance.ResponseATKByMagicD();
        }

        public void ResponseDrugRed()
        {
            Ctrl_PlayerUIResponse.Instance.HandleDrugRed();
        }

        #endregion
        //#endif

        public void DisplayProp(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("BloodBottleNum"))
            {
                if (DrugRedNum && DrugRedNum.GetComponent<Text>())
                {
                    if (System.Convert.ToInt32(kv.Value) >= 1)
                    {
                        DrugRedNum.GetComponent<Text>().text = kv.Value.ToString();
                    }
                    else
                        DrugRedNum.GetComponent<Text>().text = "0";
                }
            }
        }

        /// <summary>
        /// 复活-原地复活
        /// </summary>
        public void BtnEvent_RecoverLifeInPlace()
        {
            HideRecoverPanel();
            Ctrl_PlayerUIResponse.Instance.PlayerRecoverLife();
        }
        /// <summary>
        /// 复活-回到主城
        /// </summary>
        public void BtnEvent_RecoverLifeMajorCity()
        {
            HideRecoverPanel();
            Ctrl_PlayerUIResponse.Instance.PlayerRecoverLife();
            Ctrl_PlayerUIResponse.Instance.BackToMajorCity();
        }
    }
}