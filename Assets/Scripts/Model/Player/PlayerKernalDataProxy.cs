/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:玩家核心数值代理类
 *
 *	Description:
 *		本质是代理设计模式的应用
 *		本类必须设计为带有构造函数的单例模式
 *		作用：数据的生成与复杂计算
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

namespace Model
{
    public class PlayerKernalDataProxy : PlayerKernalData
    {
        private static PlayerKernalDataProxy _Instance = null;
        public const int ENEMY_MIN_ATK = 1;//敌人最低攻击力

        public PlayerKernalDataProxy(float health, float magic, float ATK, float DEF, float DEX,
                float maxHealth, float maxMagic, float maxATK, float maxDEF, float maxDEX,
                float ATKByProp, float DEFByProp, float DEXByProp)
            : base(health, magic, ATK, DEF, DEX, maxHealth, maxMagic, maxATK, maxDEF, maxDEX, ATKByProp, DEFByProp, DEXByProp)
        {
            if (_Instance == null)
            {
                _Instance = this;
            }
            else
            {
                Debug.LogError(GetType() + "/PlayerKernalDataProxy()/不允许构造函数重复实例化，请检查");
            }
        }

        public static PlayerKernalDataProxy GetInstance()
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

        #region 生命数值操作
        /// <summary>
        /// 减少生命数值
        /// 公式：_Health = _Health-（敌人攻击力-主角防御力-主角武器防御力）
        /// </summary>
        /// <param name="enemyAttackValue">敌人攻击力</param>
        public void DecreaseHealthValues(float enemyAttackValue)
        {
            float enemyReallyATK = 0F;
            enemyReallyATK = enemyAttackValue - base.Defence - base.DefenceByProp;

            if (enemyReallyATK > 0)
            {
                base.Health -= enemyReallyATK;
            }
            else
            {
                base.Health -= ENEMY_MIN_ATK;
            }

            //更新攻击力、防御力、敏捷度
            this.UpdateATKValues();
            this.UpdateDEFValues();
            this.UpdateDEXValues();
        }
        /// <summary>
        /// 增加生命数值
        /// </summary>
        /// <param name="healthValue"></param>
        public void IncreaseHealthValues(float healthValue)
        {
            float floReallyIncreseHealth = 0F;
            floReallyIncreseHealth = base.Health + healthValue;
            if (floReallyIncreseHealth < base.MaxHealth)
            {
                base.Health += healthValue;
            }
            else
            {
                base.Health = base.MaxHealth;
            }
        }
        /// <summary>
        /// 得到生命数值
        /// </summary>
        /// <returns></returns>
        public float GetCurrentHealth()
        {
            return base.Health;
        }

        /// <summary>
        /// 增加最大生命数值
        /// </summary>
        /// <param name="increaseHealth">增量</param>
        public void IncreaseMaxHealth(float increaseHealth)
        {
            base.MaxHealth += Mathf.Abs(increaseHealth);
        }
        /// <summary>
        /// 得到最大生命数值
        /// </summary>
        /// <returns></returns>
        public float GetMaxHealth()
        {
            return base.MaxHealth;
        }
        #endregion

        #region 魔法数值操作
        /// <summary>
        /// 减少魔法数值
        /// 公式：_Magic = _Magic - ( 释放一次“特定魔法”的损耗 )
        /// </summary>
        /// <param name="magicValue">魔法数值损耗</param>
        public void DecreaseMagicValues(float magicValue)
        {
            float reallyMagicValuesResult = 0F;//实际的剩余魔法数值
            reallyMagicValuesResult = base.Magic - magicValue;

            if (reallyMagicValuesResult > 0)
            {
                base.Magic -= Mathf.Abs(magicValue);
            }
            else
            {
                base.Magic = 0;
            }
        }
        /// <summary>
        /// 增加魔法数值
        /// </summary>
        /// <param name="MagicValue"></param>
        public void IncreaseMagicValues(float MagicValue)
        {
            float floReallyIncreseMagic = 0F;

            floReallyIncreseMagic = base.Magic + MagicValue;
            if (floReallyIncreseMagic < base.MaxMagic)
            {
                base.Magic += MagicValue;
            }
            else
            {
                base.Magic = base.MaxMagic;
            }
        }
        public float GetCurrentMagic()
        {
            return base.Magic;
        }
        /// <summary>
        /// 增加最大魔法值
        /// </summary>
        /// <param name="increaseMagic"></param>
        public void IncreaseMaxMagic(float increaseMagic)
        {
            base.MaxMagic += Mathf.Abs(increaseMagic);
        }
        /// <summary>
        /// 得到最大魔法值
        /// </summary>
        /// <returns></returns>
        public float GetMaxMagic()
        {
            return base.MaxMagic;
        }
        #endregion

        #region 攻击力数值操作
        /// <summary>
        /// 更新攻击力（典型应用场景：主角健康值改变；取得新武器）
        /// 公式：_AttackForce=MaxATK/2*(_Health/MaxHealth)+[“武器攻击力”]
        /// </summary>
        /// <param name="newWeaponValues">新武器数值</param>
        public void UpdateATKValues(float newWeaponValues = 0)
        {
            float reallyATKValues = 0F;//实际的攻击数值

            if (newWeaponValues == 0)//没有获取新的武器道具
            {
                reallyATKValues = base.MaxAttack / 2 * (base.Health / base.MaxHealth) + base.AttackByProp;
            }
            else if (newWeaponValues > 0)//取得武器道具
            {
                base.AttackByProp = newWeaponValues;
                reallyATKValues = base.MaxAttack / 2 * (base.Health / base.MaxHealth) + base.AttackByProp;
            }
            //数值有效性验证
            if (reallyATKValues > base.MaxAttack)
            {
                base.Attack = base.MaxAttack;
            }
            else
            {
                base.Attack = reallyATKValues;
            }
        }
        /// <summary>
        /// 得到当前攻击力
        /// </summary>
        /// <returns></returns>
        public float GetCurrentATK()
        {
            return base.Attack;
        }
        /// <summary>
        /// 增加最大攻击力
        /// </summary>
        /// <param name="increaseATK"></param>
        public void IncreaseMaxATK(float increaseATK)
        {
            base.MaxAttack += Mathf.Abs(increaseATK);
        }
        /// <summary>
        /// 得到最大攻击力
        /// </summary>
        /// <returns></returns>
        public float GetMaxATK()
        {
            return base.MaxAttack;
        }
        #endregion

        #region 防御力数值操作
        /// <summary>
        /// 更新防御力（典型应用场景：主角健康值改变；取得新武器）
        /// 公式：_Defence=MaxDEF/2*(_Health/MaxHealth)+[武器防御力]
        /// </summary>
        /// <param name="newWeaponDEFValues">新防御武器数值</param>
        public void UpdateDEFValues(float newWeaponDEFValues = 0)
        {
            float reallyDEFValues = 0F;//实际的攻击数值

            if (newWeaponDEFValues == 0)//没有获取新的武器道具
            {
                reallyDEFValues = base.MaxDefence / 2 * (base.Health / base.MaxHealth) + base.DefenceByProp;
            }
            else if (newWeaponDEFValues > 0)//取得武器道具
            {
                base.DefenceByProp = newWeaponDEFValues;
                reallyDEFValues = base.MaxDefence / 2 * (base.Health / base.MaxHealth) + base.DefenceByProp;
            }
            //数值有效性验证
            if (reallyDEFValues > base.MaxDefence)
            {
                base.Defence = base.MaxDefence;
            }
            else
            {
                base.Defence = reallyDEFValues;
            }
        }
        /// <summary>
        /// 得到当前防御力
        /// </summary>
        /// <returns></returns>
        public float GetCurrentDEF()
        {
            return base.Defence;
        }
        /// <summary>
        /// 增加最大防御力
        /// </summary>
        /// <param name="increaseDEF"></param>
        public void IncreaseMaxDEF(float increaseDEF)
        {
            base.MaxDefence += Mathf.Abs(increaseDEF);
        }
        /// <summary>
        /// 得到最大防御力
        /// </summary>
        /// <returns></returns>
        public float GetMaxDEF()
        {
            return base.MaxDefence;
        }
        #endregion

        #region 敏捷度数值操作
        /// <summary>
        /// 更新敏捷度（典型应用场景：主角健康值改变；防御力改变；取得新武器）
        /// 公式：_MoveSpeed=MaxMoveSpeed/2*(_Health/MaxHealth)-_Defence+[道具敏捷力]
        /// </summary>
        /// <param name="newWeaponValues">新武器数值</param>
        public void UpdateDEXValues(float newWeaponValues = 0)
        {
            float reallyDEXValues = 0F;//实际的敏捷度数值

            if (newWeaponValues == 0)//没有获取新的武器道具
            {
                reallyDEXValues = base.MaxDexterity / 2 * (base.Health / base.MaxHealth) - base.Defence + base.DexterityByProp;
            }
            else if (newWeaponValues > 0)//取得武器道具
            {
                base.DexterityByProp = newWeaponValues;
                reallyDEXValues = base.MaxDexterity / 2 * (base.Health / base.MaxHealth) - base.Defence + base.DexterityByProp;
            }
            //数值有效性验证
            if (reallyDEXValues > base.MaxDexterity)
            {
                base.Dexterity = base.MaxDexterity;
            }
            else
            {
                base.Dexterity = reallyDEXValues;
            }
        }
        /// <summary>
        /// 得到当前敏捷度
        /// </summary>
        /// <returns></returns>
        public float GetCurrentDEX()
        {
            return base.Dexterity;
        }
        /// <summary>
        /// 增加最大敏捷度
        /// </summary>
        /// <param name="increaseDEX"></param>
        public void IncreaseMaxDEX(float increaseDEX)
        {
            base.MaxDexterity += Mathf.Abs(increaseDEX);
        }
        /// <summary>
        /// 得到最大敏捷度
        /// </summary>
        /// <returns></returns>
        public float GetMaxDEX()
        {
            return base.MaxDexterity;
        }
        #endregion

        /// <summary>
        /// 显示所有的初始值
        /// </summary>
        public void DisplayerAllOriginalValues()
        {
            base.Health = base.Health;
            base.Magic = base.Magic;
            base.Attack = base.Attack;
            base.Defence = base.Defence;
            base.Dexterity = base.Dexterity;

            base.MaxHealth = base.MaxHealth;
            base.MaxMagic = base.MaxMagic;
            base.MaxAttack = base.MaxAttack;
            base.MaxDefence = base.MaxDefence;
            base.MaxDexterity = base.MaxDexterity;

            base.AttackByProp = base.AttackByProp;
            base.DefenceByProp = base.DefenceByProp;
            base.DexterityByProp = base.DexterityByProp;
        }
    }
}