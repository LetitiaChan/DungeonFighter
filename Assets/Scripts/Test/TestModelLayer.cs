/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:测试模型数据使用
 *
 *	Description:
 *		1.
 *
 *	Date:
 *
 *	Version:
 *		1.0
 *
 *	Author:chenx
 *
*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Global;
using Model;

namespace View
{
    public class TestModelLayer : MonoBehaviour
    {
        public Text TxtHP;
        public Text TxtMP;
        public Text TxtATK;
        public Text TxtDEF;
        public Text TxtDEX;
        public Text TxtMaxHP;
        public Text TxtMaxMP;
        public Text TxtMaxATK;
        public Text TxtMaxDEF;
        public Text TxtMaxDEX;

        //扩展数值
        public Text TxtExp;
        public Text TxtKillNum;
        public Text TxtLevel;
        public Text TxtGold;
        public Text TxtDiamond;

        void Awake()
        {
            //核心数值事件注册
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

            //扩展数值事件注册
            PlayerExternalData.evePlayerExtenalData += DisplayExp;
            PlayerExternalData.evePlayerExtenalData += DisplayKillNumber;
            PlayerExternalData.evePlayerExtenalData += DisplayLevel;
            PlayerExternalData.evePlayerExtenalData += DisplayGold;
            PlayerExternalData.evePlayerExtenalData += DisplayDiamonds;
        }


        void Start()
        {
            //PlayerKernalDataProxy playerKernalDataObject = new PlayerKernalDataProxy(100, 100, 10, 5, 45, 100, 100, 10, 10, 50, 0, 0, 0);
            //PlayerExternalDataProxy playerExternalDataObj = new PlayerExternalDataProxy(0, 0, 0, 0, 0);

            //PlayerKernalDataProxy.GetInstance().DisplayerAllOriginalValues();
            //PlayerExternalDataProxy.GetInstance().DisplayAllOriginalValues();
        }

        #region 事件用户点击
        public void IncreaseHP()
        {
            //调用模型层方法
            PlayerKernalDataProxy.GetInstance().IncreaseHealthValues(30);
        }
        public void DecreaseHP()
        {
            //调用模型层方法
            PlayerKernalDataProxy.GetInstance().DecreaseHealthValues(10);
        }

        public void IncreaseMP()
        {
            //调用模型层方法
            PlayerKernalDataProxy.GetInstance().IncreaseMagicValues(40);
        }
        public void DecreaseMP()
        {
            //调用模型层方法
            PlayerKernalDataProxy.GetInstance().DecreaseMagicValues(15);
        }
        #endregion

        #region 事件注册方法
        private void DisplayHP(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Health"))
            {
                TxtHP.text = kv.Value.ToString();
            }
        }
        private void DisplayMaxHP(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxHealth"))
            {
                TxtMaxHP.text = kv.Value.ToString();
            }
        }
        private void DisplayMP(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Magic"))
            {
                TxtMP.text = kv.Value.ToString();
            }
        }
        private void DisplayMaxMP(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxMagic"))
            {
                TxtMaxMP.text = kv.Value.ToString();
            }
        }
        private void DisplayATK(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Attack"))
            {
                TxtATK.text = kv.Value.ToString();
            }
        }
        private void DisplayMaxATK(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxAttack"))
            {
                TxtMaxATK.text = kv.Value.ToString();
            }
        }
        private void DisplayDEF(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Defence"))
            {
                TxtDEF.text = kv.Value.ToString();
            }
        }
        private void DisplayMaxDEF(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxDefence"))
            {
                TxtMaxDEF.text = kv.Value.ToString();
            }
        }
        private void DisplayDEX(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Dexterity"))
            {
                TxtDEX.text = kv.Value.ToString();
            }
        }
        private void DisplayMaxDEX(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxDexterity"))
            {
                TxtMaxDEX.text = kv.Value.ToString();
            }
        }

        /* 扩展数值 */
        private void DisplayExp(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Experience"))
            {
                TxtExp.text = kv.Value.ToString();
            }
        }
        private void DisplayKillNumber(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("KillNumber"))
            {
                TxtKillNum.text = kv.Value.ToString();
            }
        }
        private void DisplayLevel(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Level"))
            {
                TxtLevel.text = kv.Value.ToString();
            }
        }
        private void DisplayGold(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Gold"))
            {
                TxtGold.text = kv.Value.ToString();
            }
        }
        private void DisplayDiamonds(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Diamonds"))
            {
                TxtDiamond.text = kv.Value.ToString();
            }
        }

        public void IncreaseExp()
        {
            PlayerExternalDataProxy.GetInstance().AddExp(100);
        }
        #endregion

    }
}