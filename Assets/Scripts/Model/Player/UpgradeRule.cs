/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:模型层：升级规则类
 *
 *	Description:
 *		1.开放-封闭原则 单一职责原则
 *
 *	Date:2017.02.25
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

namespace Model
{
    public class UpgradeRule
    {
        private static UpgradeRule _Instance;

        private UpgradeRule() { }

        public static UpgradeRule GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new UpgradeRule();
            }
            return _Instance;
        }

        /// <summary>
        /// 升级条件
        /// </summary>
        public void UpgradeCondition(int exp)
        {
            int currentLevel = 0;
            currentLevel = PlayerExternalDataProxy.GetInstance().GetLevel();

            if (exp >= 100 && exp < 300 && currentLevel == 0)
            {
                PlayerExternalDataProxy.GetInstance().AddLevel();
            }
            else if (exp >= 300 && exp < 500 && currentLevel == 1)
            {
                PlayerExternalDataProxy.GetInstance().AddLevel();
            }
            else if (exp >= 500 && exp < 1000 && currentLevel == 2)
            {
                PlayerExternalDataProxy.GetInstance().AddLevel();
            }
            else if (exp >= 1000 && exp < 3000 && currentLevel == 3)
            {
                PlayerExternalDataProxy.GetInstance().AddLevel();
            }
            else if (exp >= 3000 && exp < 5000 && currentLevel == 4)
            {
                PlayerExternalDataProxy.GetInstance().AddLevel();
            }
            else if (exp >= 5000 && exp < 10000 && currentLevel == 5)
            {
                PlayerExternalDataProxy.GetInstance().AddLevel();
            }
        }

        /// <summary>
        /// 升级操作
        /// 1.处理核心最大数值增加
        /// 2.对应的核心数值，加满为最大数值
        /// </summary>
        public void UpgradeOperation(LevelName lvName)
        {
            switch (lvName)
            {
                case LevelName.Level_0:
                    UpgradeRuleOperation(10, 10, 2, 1, 10);
                    break;
                case LevelName.Level_1:
                    UpgradeRuleOperation(10, 10, 2, 1, 10);
                    break;
                case LevelName.Level_2:
                    UpgradeRuleOperation(10, 10, 2, 1, 10);
                    break;
                case LevelName.Level_3:
                    UpgradeRuleOperation(10, 10, 2, 1, 10);
                    break;
                case LevelName.Level_4:
                    UpgradeRuleOperation(10, 10, 2, 1, 10);
                    break;
                case LevelName.Level_5:
                    UpgradeRuleOperation(10, 10, 2, 1, 10);
                    break;
                case LevelName.Level_6:
                    UpgradeRuleOperation(10, 10, 2, 1, 10);
                    break;
                case LevelName.Level_7:
                    UpgradeRuleOperation(10, 10, 2, 1, 10);
                    break;
                case LevelName.Level_8:
                    UpgradeRuleOperation(10, 10, 2, 1, 10);
                    break;
                case LevelName.Level_9:
                    UpgradeRuleOperation(10, 10, 2, 1, 10);
                    break;
                case LevelName.Level_10:
                    UpgradeRuleOperation(10, 10, 2, 1, 10);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 具体的升级规则
        /// </summary>
        /// <param name="maxhp">最大生命值增量</param>
        /// <param name="maxmp">最大魔法值增量</param>
        /// <param name="maxATK">最大攻击力增量</param>
        /// <param name="maxDEF">最大防御力增量</param>
        /// <param name="maxDEX">最大敏捷度增量</param>
        public void UpgradeRuleOperation(float maxhp, float maxmp, float maxATK, float maxDEF, float maxDEX)
        {
            //处理核心最大数值增加
            PlayerKernalDataProxy.GetInstance().IncreaseMaxHealth(maxhp);
            PlayerKernalDataProxy.GetInstance().IncreaseMaxMagic(maxmp);
            PlayerKernalDataProxy.GetInstance().IncreaseMaxATK(maxATK);
            PlayerKernalDataProxy.GetInstance().IncreaseMaxDEF(maxDEF);
            PlayerKernalDataProxy.GetInstance().IncreaseMaxDEX(maxDEX);

            //hp mp加满为最大数值
            PlayerKernalDataProxy.GetInstance().IncreaseHealthValues(PlayerKernalDataProxy.GetInstance().GetMaxHealth());
            PlayerKernalDataProxy.GetInstance().IncreaseMagicValues(PlayerKernalDataProxy.GetInstance().GetMaxMagic());
        }
    }
}