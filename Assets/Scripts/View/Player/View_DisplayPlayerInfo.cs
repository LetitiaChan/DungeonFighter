/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:视图层：显示玩家信息
 *
 *	Description:
 *		1.
 *
 *	Date:2017.02.26
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
    public class View_DisplayPlayerInfo : MonoBehaviour
    {
        //玩家屏幕信息
        public Text TxtPlayerName;                                              //玩家名字
        public Button BtnPlayer;                                                //头像按钮
        public Text TxtCurLevelByScreen;                                        //当前等级
        public Slider SliHP;                                                    //血条
        public Slider SliMP;                                                    //蓝条
        public Text TxtCurHPByScreen;                                           //当前生命
        public Text TxtMaxHPByScreen;                                           //最大生命
        public Text TxtCurMPByScreen;                                           //当前魔法
        public Text TxtMaxMPByScreen;                                           //最大魔法
        public Text TxtExpByScreen;                                             //经验值
        public Text TxtGoldByScreen;                                            //金币
        public Text TxtDiamondsByScreen;                                        //钻石

        //玩家详细信息
        public Text TxtPlayerNameByDetailPanel;                                 //玩家名字
        public Text TxtCurLevel;                                                //当前等级
        public Text TxtHP_Cur;                                                  //当前生命
        public Text TxtHP_Max;                                                  //最大生命
        public Text TxtMP_Cur;                                                  //当前魔法
        public Text TxtMP_Max;                                                  //最大魔法
        public Text TxtExp_Cur;                                                 //当前经验
        public Text TxtExp_Max;                                                 //最大经验

        public Text TxtATK_Cur;                                                 //当前攻击力
        public Text TxtATK_Max;                                                 //最大攻击力
        public Text TxtDEF_Cur;                                                 //当前防御
        public Text TxtDEF_Max;                                                 //最大防御
        public Text TxtDEX_Cur;                                                 //当前敏捷
        public Text TxtDEX_Max;                                                 //最大敏捷

        public Text TxtKillNumber;                                              //杀敌数
        public Text TxtGold;                                                    //金币
        public Text TxtDiamonds;                                                //金币

        public const float WAIT_FOR_SECONDS_ON_START = 0.3F;                   //Start 方法等待时间

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

        IEnumerator Start()
        {
            yield return new WaitForSeconds(WAIT_FOR_SECONDS_ON_START);

            //显示初始值
            PlayerKernalDataProxy.GetInstance().DisplayerAllOriginalValues();
            PlayerExternalDataProxy.GetInstance().DisplayAllOriginalValues();
            //玩家的姓名
            if (!string.IsNullOrEmpty(GlobalParaMgr.PlayerName))
            {
                TxtPlayerName.text = GlobalParaMgr.PlayerName;
                TxtPlayerNameByDetailPanel.text = GlobalParaMgr.PlayerName;
            }
        }

        #region 事件注册方法
        private void DisplayHP(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Health") && TxtCurHPByScreen && TxtHP_Cur)
            {
                TxtCurHPByScreen.text = kv.Value.ToString();
                TxtHP_Cur.text = kv.Value.ToString();

                SliHP.value = (float)kv.Value;
            }
        }
        private void DisplayMaxHP(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxHealth") && TxtMaxHPByScreen && TxtHP_Max)
            {
                TxtMaxHPByScreen.text = kv.Value.ToString();
                TxtHP_Max.text = kv.Value.ToString();

                //滑动条处理
                SliHP.maxValue = (float)kv.Value;
                SliHP.minValue = 0;
            }
        }
        private void DisplayMP(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Magic") && TxtCurMPByScreen && TxtMP_Cur)
            {
                TxtCurMPByScreen.text = kv.Value.ToString();
                TxtMP_Cur.text = kv.Value.ToString();

                SliMP.value = (float)kv.Value;
            }
        }
        private void DisplayMaxMP(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxMagic") && TxtMaxMPByScreen && TxtMP_Max)
            {
                TxtMaxMPByScreen.text = kv.Value.ToString();
                TxtMP_Max.text = kv.Value.ToString();

                //滑动条处理
                SliMP.maxValue = (float)kv.Value;
                SliMP.minValue = 0;
            }
        }
        private void DisplayATK(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Attack") && TxtATK_Cur)
            {
                TxtATK_Cur.text = kv.Value.ToString();
            }
        }
        private void DisplayMaxATK(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxAttack") && TxtATK_Max)
            {
                TxtATK_Max.text = kv.Value.ToString();
            }
        }
        private void DisplayDEF(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Defence") && TxtDEF_Cur)
            {
                TxtDEF_Cur.text = kv.Value.ToString();
            }
        }
        private void DisplayMaxDEF(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxDefence") && TxtDEF_Max)
            {
                TxtDEF_Max.text = kv.Value.ToString();
            }
        }
        private void DisplayDEX(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Dexterity") && TxtDEX_Cur)
            {
                TxtDEX_Cur.text = kv.Value.ToString();
            }
        }
        private void DisplayMaxDEX(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("MaxDexterity") && TxtDEX_Max)
            {
                TxtDEX_Max.text = kv.Value.ToString();
            }
        }

        /* 扩展数值 */
        private void DisplayExp(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Experience") && TxtExpByScreen && TxtExp_Cur)
            {
                TxtExpByScreen.text = kv.Value.ToString();
                TxtExp_Cur.text = kv.Value.ToString();
            }
        }
        private void DisplayKillNumber(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("KillNumber") && TxtKillNumber)
            {
                TxtKillNumber.text = kv.Value.ToString();
            }
        }
        private void DisplayLevel(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Level") && TxtCurLevelByScreen && TxtCurLevel)
            {
                TxtCurLevelByScreen.text = kv.Value.ToString();
                TxtCurLevel.text = kv.Value.ToString();
            }
        }
        private void DisplayGold(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Gold") && TxtGoldByScreen && TxtGold)
            {
                TxtGoldByScreen.text = kv.Value.ToString();
                TxtGold.text = kv.Value.ToString();
            }
        }
        private void DisplayDiamonds(KeyValuesUpdate kv)
        {
            if (kv.Key.Equals("Diamonds") && TxtDiamondsByScreen && TxtDiamonds)
            {
                TxtDiamondsByScreen.text = kv.Value.ToString();
                TxtDiamonds.text = kv.Value.ToString();
            }
        }

        #endregion
    }
}