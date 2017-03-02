/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:玩家扩展数值类
 *
 *	Description:
 *		1.
 *
 *	Date:2017.02.24
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
using Kernal;

namespace Model
{
    public class PlayerExternalData
    {
        public static event del_PlayerKernalModel evePlayerExtenalData;//玩家扩展数值

        private int _IntExperience;             //经验值
        private int _IntKillNumber;             //杀敌数量
        private int _IntLevel;                  //当前等级
        private int _IntGold;                   //金币
        private int _IntDiamonds;               //钻石

        #region 属性
        /// <summary>
        /// 经验值
        /// </summary>
        public int Experience
        {
            get
            {
                return _IntExperience;
            }

            set
            {
                _IntExperience = value;
                //事件调用
                if (evePlayerExtenalData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Experience", Experience);
                    evePlayerExtenalData(kv);
                }
            }
        }
        /// <summary>
        /// 杀敌数量
        /// </summary>
        public int KillNumber
        {
            get
            {
                return _IntKillNumber;
            }

            set
            {
                _IntKillNumber = value;
                //事件调用
                if (evePlayerExtenalData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("KillNumber", KillNumber);
                    evePlayerExtenalData(kv);
                }
            }
        }
        /// <summary>
        /// 当前等级
        /// </summary>
        public int Level
        {
            get
            {
                return _IntLevel;
            }

            set
            {
                _IntLevel = value;
                //事件调用
                if (evePlayerExtenalData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Level", Level);
                    evePlayerExtenalData(kv);
                }
            }
        }
        /// <summary>
        /// 金币
        /// </summary>
        public int Gold
        {
            get
            {
                return _IntGold;
            }

            set
            {
                _IntGold = value;
                //事件调用
                if (evePlayerExtenalData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Gold", Gold);
                    evePlayerExtenalData(kv);
                }
            }
        }
        /// <summary>
        /// 钻石
        /// </summary>
        public int Diamonds
        {
            get
            {
                return _IntDiamonds;
            }

            set
            {
                _IntDiamonds = value;
                //事件调用
                if (evePlayerExtenalData != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Diamonds", Diamonds);
                    evePlayerExtenalData(kv);
                }
            }
        }
        #endregion

        public PlayerExternalData() { }

        public PlayerExternalData(int exp, int killNumber, int level, int gold, int diamonds)
        {
            this._IntExperience = exp;
            this._IntKillNumber = killNumber;
            this._IntLevel = level;
            this._IntGold = gold;
            this._IntDiamonds = diamonds;
        }
    }
}