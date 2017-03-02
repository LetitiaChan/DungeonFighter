/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:玩家扩展数值代理类
 *
 *	Description:
 *		本质是代理设计模式的应用
 *		本类必须设计为带有构造函数的单例模式
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
    public class PlayerExternalDataProxy : PlayerExternalData
    {
        private static PlayerExternalDataProxy _Instance = null;

        public PlayerExternalDataProxy(int exp, int killNumber, int level, int gold, int diamonds)
            : base(exp, killNumber, level, gold, diamonds)
        {
            if (_Instance == null)
            {
                _Instance = this;
            }
            else
            {
                Debug.LogError(GetType() + "/PlayerExternalDataProxy()/不允许构造函数重复实例化，请检查");
            }
        }

        public static PlayerExternalDataProxy GetInstance()
        {
            if (_Instance != null)
            {
                return _Instance;
            }
            else
            {
                Debug.LogWarning("/GetInstance()/请先调用构造函数");
                return null;
            }
        }

        #region 经验值
        /// <summary>
        /// 增加经验值
        /// </summary>
        /// <param name="addExp">经验数值</param>
        public void AddExp(int addExp)
        {
            base.Experience += Mathf.Abs(addExp);
            //经验值到达一定阶段 升级 
            UpgradeRule.GetInstance().UpgradeCondition(base.Experience);
        }
        /// <summary>
        /// 得到经验值
        /// </summary>
        /// <returns></returns>
        public int GetExp()
        {
            return base.Experience;
        }
        #endregion

        #region 杀敌数量
        /// <summary>
        /// 增加杀敌数量
        /// </summary>
        public void AddKillNumber()
        {
            ++base.KillNumber;
        }
        /// <summary>
        /// 得到杀敌数量
        /// </summary>
        /// <returns></returns>
        public int GetKillNumber()
        {
            return base.KillNumber;
        }
        #endregion

        #region 等级
        /// <summary>
        /// 增加等级
        /// </summary>
        public void AddLevel()
        {
            ++base.Level;
            //等级提升，属性提升
            UpgradeRule.GetInstance().UpgradeOperation((LevelName)base.Level);
        }
        /// <summary>
        /// 得到等级
        /// </summary>
        /// <returns></returns>
        public int GetLevel()
        {
            return base.Level;
        }
        #endregion

        #region 金币
        /// <summary>
        /// 增加金币
        /// </summary>
        /// <param name="goldNumber"></param>
        public void AddGold(int goldNumber)
        {
            base.Gold += Mathf.Abs(goldNumber);
        }
        /// <summary>
        /// 得到金币
        /// </summary>
        /// <returns></returns>
        public int GetGold()
        {
            return base.Gold;
        }
        #endregion

        #region 钻石
        /// <summary>
        /// 增加钻石
        /// </summary>
        public void AddDiamonds(int diamondNumber)
        {
            base.Diamonds += Mathf.Abs(diamondNumber);
        }
        /// <summary>
        /// 得到钻石
        /// </summary>
        /// <returns></returns>
        public int GetDiamonds()
        {
            return base.Diamonds;
        }
        #endregion

        /// <summary>
        /// 显示所有初始数值
        /// </summary>
        public void DisplayAllOriginalValues()
        {
            base.Experience = base.Experience;
            base.KillNumber = base.KillNumber;
            base.Level = base.Level;
            base.Gold = base.Gold;
            base.Diamonds = base.Diamonds;
        }
    }
}