/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title: 玩家核心数值类
 *
 *	Description:
 *		作用：存取数据/XML对象持久化/用事件实现观察者模式[即自动更新视图层]
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
    public class PlayerKernalData
    {
        public static event del_PlayerKernalModel evePlayerKernal;//玩家核心数值

        private float _FloHealth;               //血条
        private float _FloMagic;                //魔法
        private float _FloAttack;               //攻击力
        private float _FloDefence;              //防御力
        private float _FloDexterity;            //敏捷度

        private float _FloMaxHealth;            //最大血条
        private float _FloMaxMagic;             //最大魔法
        private float _FloMaxAttack;            //最大攻击力
        private float _FloMaxDefence;           //最大防御力
        private float _FloMaxDexterity;         //最大敏捷度

        private float _FloAttackByProp;         //武器攻击力
        private float _FloDefenceByProp;        //武器防御力
        private float _FloDexterityByProp;      //道具敏捷度

        #region 属性
        /// <summary>
        /// 生命值
        /// </summary>
        public float Health
        {
            get
            {
                return _FloHealth;
            }

            set
            {
                _FloHealth = value;
                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Health", Health);
                    evePlayerKernal(kv);
                }
            }
        }
        /// <summary>
        /// 魔法值
        /// </summary>
        public float Magic
        {
            get
            {
                return _FloMagic;
            }

            set
            {
                _FloMagic = value;

                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Magic", Magic);
                    evePlayerKernal(kv);
                }
            }
        }
        /// <summary>
        /// 攻击力
        /// </summary>
        public float Attack
        {
            get
            {
                return _FloAttack;
            }

            set
            {
                _FloAttack = value;

                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Attack", Attack);
                    evePlayerKernal(kv);
                }
            }
        }
        /// <summary>
        /// 防御力
        /// </summary>
        public float Defence
        {
            get
            {
                return _FloDefence;
            }

            set
            {
                _FloDefence = value;
                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Defence", Defence);
                    evePlayerKernal(kv);
                }
            }
        }
        /// <summary>
        /// 敏捷度
        /// </summary>
        public float Dexterity
        {
            get
            {
                return _FloDexterity;
            }

            set
            {
                _FloDexterity = value;

                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("Dexterity", Dexterity);
                    evePlayerKernal(kv);
                }
            }
        }


        /// <summary>
        /// 最大生命值
        /// </summary>
        public float MaxHealth
        {
            get
            {
                return _FloMaxHealth;
            }

            set
            {
                _FloMaxHealth = value;
                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("MaxHealth", MaxHealth);
                    evePlayerKernal(kv);
                }
            }
        }
        /// <summary>
        /// 最大魔法值
        /// </summary>
        public float MaxMagic
        {
            get
            {
                return _FloMaxMagic;
            }

            set
            {
                _FloMaxMagic = value;

                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("MaxMagic", MaxMagic);
                    evePlayerKernal(kv);
                }
            }
        }
        /// <summary>
        /// 最大攻击力
        /// </summary>
        public float MaxAttack
        {
            get
            {
                return _FloMaxAttack;
            }

            set
            {
                _FloMaxAttack = value;

                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("MaxAttack", MaxAttack);
                    evePlayerKernal(kv);
                }
            }
        }
        /// <summary>
        /// 最大防御力
        /// </summary>
        public float MaxDefence
        {
            get
            {
                return _FloMaxDefence;
            }

            set
            {
                _FloMaxDefence = value;
                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("MaxDefence", MaxDefence);
                    evePlayerKernal(kv);
                }
            }
        }
        /// <summary>
        /// 最大敏捷度
        /// </summary>
        public float MaxDexterity
        {
            get
            {
                return _FloMaxDexterity;
            }

            set
            {
                _FloMaxDexterity = value;
                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("MaxDexterity", MaxDexterity);
                    evePlayerKernal(kv);
                }
            }
        }


        /// <summary>
        /// 武器攻击力
        /// </summary>
        public float AttackByProp
        {
            get
            {
                return _FloAttackByProp;
            }

            set
            {
                _FloAttackByProp = value;
                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("AttackByProp", AttackByProp);
                    evePlayerKernal(kv);
                }
            }
        }
        /// <summary>
        /// 武器防御力
        /// </summary>
        public float DefenceByProp
        {
            get
            {
                return _FloDefenceByProp;
            }

            set
            {
                _FloDefenceByProp = value;
                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("DefenceByProp", DefenceByProp);
                    evePlayerKernal(kv);
                }
            }
        }
        /// <summary>
        /// 道具敏捷度
        /// </summary>
        public float DexterityByProp
        {
            get
            {
                return _FloDexterityByProp;
            }

            set
            {
                _FloDexterityByProp = value;
                //事件调用
                if (evePlayerKernal != null)
                {
                    KeyValuesUpdate kv = new KeyValuesUpdate("DexterityByProp", DexterityByProp);
                    evePlayerKernal(kv);
                }
            }
        }
        #endregion

        public PlayerKernalData()
        {

        }

        public PlayerKernalData(float health, float magic, float attack, float defence, float dexterity,
            float maxHealth, float maxMagic, float maxAttack, float maxDefence, float maxDexterity,
            float attackByProp, float defenceByProp, float dexterityByProp)
        {
            this._FloHealth = health;
            this._FloMagic = magic;
            this._FloAttack = attack;
            this._FloDefence = defence;
            this._FloDexterity = dexterity;

            this._FloMaxHealth = maxHealth;
            this._FloMaxMagic = maxMagic;
            this._FloMaxAttack = maxAttack;
            this._FloMaxDefence = maxDefence;
            this._FloMaxDexterity = maxDexterity;

            this._FloAttackByProp = attackByProp;
            this._FloDefenceByProp = defenceByProp;
            this._FloDexterityByProp = dexterityByProp;
        }
    }
}