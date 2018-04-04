using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Global;
using Model;
using Kernal;

namespace View
{
    public class View_PlayerInfoDisplay : MonoBehaviour
    {
        //玩家屏幕信息
        public Text txtPlayerName;
        public Button btnPlayer;
        public Text txtCurLevelByScreen;
        public Slider sliHP;
        public Slider sliMP;
        public Text txtCurHPByScreen;
        public Text txtMaxHPByScreen;
        public Text txtCurMPByScreen;
        public Text txtMaxMPByScreen;
        public Text txtExpByScreen;
        public Text txtGoldByScreen;
        public Text txtDiamondsByScreen;

        //玩家详细信息
        public Text txtPlayerNameByDetailPanel;
        public Text txtCurLevel;
        public Text txtHP_Cur;
        public Text txtHP_Max;
        public Text txtMP_Cur;
        public Text txtMP_Max;
        public Text txtExp_Cur;
        public Text txtExp_Max;

        public Text txtATK_Cur;
        public Text txtATK_Max;
        public Text txtDEF_Cur;
        public Text txtDEF_Max;
        public Text txtDEX_Cur;
        public Text txtDEX_Max;

        public Text txtKillNumber;
        public Text txtGold;
        public Text txtDiamonds;

        private const float WAIT_FOR_SECONDS_ON_START = 0.5f;

        void Awake()
        {
            PlayerKernalData.evePlayerKernal += DisplayHP;
            PlayerKernalData.evePlayerKernal += DisplayMaxHP;
            PlayerKernalData.evePlayerKernal += DisplayMP;
            PlayerKernalData.evePlayerKernal += DisplayMaxMP;
            PlayerKernalData.evePlayerKernal += DisplayATK;
            PlayerKernalData.evePlayerKernal += DisplayMaxATK;
            PlayerKernalData.evePlayerKernal += DisplayDEF;
            PlayerKernalData.evePlayerKernal += DisplayMaxDEF;
            PlayerKernalData.evePlayerKernal += DisplayDEX;
            PlayerKernalData.evePlayerKernal += DisplayMaxDEX;

            PlayerExternalData.evePlayerExtenalData += DisplayExp;
            PlayerExternalData.evePlayerExtenalData += DisplayKillNumber;
            PlayerExternalData.evePlayerExtenalData += DisplayLevel;
            PlayerExternalData.evePlayerExtenalData += DisplayGold;
            PlayerExternalData.evePlayerExtenalData += DisplayDiamonds;
        }

        IEnumerator Start()
        {
            yield return new WaitForSeconds(WAIT_FOR_SECONDS_ON_START);

            PlayerKernalDataProxy.GetInstance().DisplayerAllOriginalValues();
            PlayerExternalDataProxy.GetInstance().DisplayAllOriginalValues();

            if (!string.IsNullOrEmpty(GlobalParaMgr.PlayerName))
            {
                txtPlayerName.text = GlobalParaMgr.PlayerName;
                txtPlayerNameByDetailPanel.text = GlobalParaMgr.PlayerName;
            }
        }

        #region 事件注册方法
        private void DisplayHP(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Health"))
            {
                if (txtCurHPByScreen && txtHP_Cur && sliHP)
                {
                    txtCurHPByScreen.text = kv.Value.ToString();
                    txtHP_Cur.text = kv.Value.ToString();

                    sliHP.value = (float)kv.Value;
                }
                if (((float)kv.Value) <= 0)
                {
                    View_PlayerInfoResponse.Instance.DisplayRecoverPanel();
                    //StartCoroutine("RecoverLifeDisplay");
                }
            }
        }

        //IEnumerator RecoverLifeDisplay()
        //{
        //    yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_5);
        //    View_PlayerInfoResponse.Instance.DisplayRecoverPanel();
        //    StopCoroutine("RecoverLifeDisplay");
        //}

        private void DisplayMaxHP(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxHealth") && txtMaxHPByScreen && txtHP_Max)
            {
                txtMaxHPByScreen.text = kv.Value.ToString();
                txtHP_Max.text = kv.Value.ToString();

                sliHP.maxValue = (float)kv.Value;
                sliHP.minValue = 0;
            }
        }
        private void DisplayMP(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Magic") && txtCurMPByScreen && txtMP_Cur)
            {
                txtCurMPByScreen.text = kv.Value.ToString();
                txtMP_Cur.text = kv.Value.ToString();

                sliMP.value = (float)kv.Value;
            }
        }
        private void DisplayMaxMP(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxMagic") && txtMaxMPByScreen && txtMP_Max)
            {
                txtMaxMPByScreen.text = kv.Value.ToString();
                txtMP_Max.text = kv.Value.ToString();

                sliMP.maxValue = (float)kv.Value;
                sliMP.minValue = 0;
            }
        }
        private void DisplayATK(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Attack") && txtATK_Cur)
            {
                txtATK_Cur.text = kv.Value.ToString();
            }
        }
        private void DisplayMaxATK(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxAttack") && txtATK_Max)
            {
                txtATK_Max.text = kv.Value.ToString();
            }
        }
        private void DisplayDEF(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Defence") && txtDEF_Cur)
            {
                txtDEF_Cur.text = kv.Value.ToString();
            }
        }
        private void DisplayMaxDEF(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxDefence") && txtDEF_Max)
            {
                txtDEF_Max.text = kv.Value.ToString();
            }
        }
        private void DisplayDEX(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Dexterity") && txtDEX_Cur)
            {
                txtDEX_Cur.text = kv.Value.ToString();
            }
        }
        private void DisplayMaxDEX(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxDexterity") && txtDEX_Max)
            {
                txtDEX_Max.text = kv.Value.ToString();
            }
        }


        private void DisplayExp(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Experience") && txtExpByScreen && txtExp_Cur)
            {
                txtExpByScreen.text = kv.Value.ToString();
                txtExp_Cur.text = kv.Value.ToString();
            }
        }
        private void DisplayKillNumber(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("KillNumber") && txtKillNumber)
            {
                txtKillNumber.text = kv.Value.ToString();
            }
        }
        private void DisplayLevel(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Level") && txtCurLevelByScreen && txtCurLevel)
            {
                txtCurLevelByScreen.text = kv.Value.ToString();
                txtCurLevel.text = kv.Value.ToString();
            }
        }
        private void DisplayGold(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Gold") && txtGoldByScreen && txtGold)
            {
                txtGoldByScreen.text = kv.Value.ToString();
                txtGold.text = kv.Value.ToString();
            }
        }
        private void DisplayDiamonds(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Diamonds") && txtDiamondsByScreen && txtDiamonds)
            {
                txtDiamondsByScreen.text = kv.Value.ToString();
                txtDiamonds.text = kv.Value.ToString();
            }
        }

        #endregion
    }
}