/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层：英雄属性脚本
 *
 *	Description:
 *		1.实例化对应模型层类且初始化数据
 *		2.整个模型层关于“玩家”模块的核心方法，供控制层其他脚本调用
 *
 *	Date:2017-02-26
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

namespace Control
{
    public class Ctrl_HeroProperty : BaseControl
    {

        public static Ctrl_HeroProperty Instance;

        //玩家核心数值
        public float FloHP_Cur = 100F;                                         //当前生命
        public float FloHP_Max = 100F;                                         //最大生命
        public float FloMP_Cur = 100F;                                         //当前魔法
        public float FloMP_Max = 100F;                                         //最大魔法
        public float FloATK_Cur = 10F;                                         //当前攻击
        public float FloATK_Max = 10F;                                         //当前攻击
        public float FloDEF_Cur = 5F;                                          //当前防御
        public float FloDEF_Max = 5F;                                          //当前防御
        public float FloDEX_Cur = 45F;                                         //当前敏捷
        public float FloDEX_Max = 50F;                                         //当前敏捷

        public float FloATKByProp = 0F;                                        //道具攻击力
        public float FloDEFByProp = 0F;                                        //道具防御力
        public float FloDEXByProp = 0F;                                        //道具敏捷度

        //玩家扩展数值
        public int IntEXP = 0;                                                 //当前经验
        public int IntLevel = 0;                                               //当前等级
        public int IntKillNum = 0;                                             //当前杀敌
        public int IntGold = 0;                                                //当前金币
        public int IntDiamonds = 0;                                            //当前钻石

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            //初始化模型层数据
            PlayerKernalDataProxy playerKernalDataObject = new PlayerKernalDataProxy(FloHP_Cur, FloMP_Cur, FloATK_Cur, FloDEF_Cur, FloDEX_Cur, FloHP_Max, FloMP_Max, FloATK_Max, FloDEF_Max, FloDEX_Max, FloATKByProp, FloDEFByProp, FloDEXByProp);
            PlayerExternalDataProxy playerExternalDataObj = new PlayerExternalDataProxy(IntEXP, IntKillNum, IntLevel, IntGold, IntDiamonds);
        }

        #region 生命数值操作
        /// <summary>
        /// 减少生命数值
        /// 公式：_Health = _Health-（敌人攻击力-主角防御力-主角武器防御力）
        /// </summary>
        /// <param name="enemyAttackValue">敌人攻击力</param>
        public void DecreaseHealthValues(float enemyAttackValue)
        {
            if (enemyAttackValue > 0)
            {
                PlayerKernalDataProxy.GetInstance().DecreaseHealthValues(enemyAttackValue);
            }
        }
        /// <summary>
        /// 增加生命数值
        /// </summary>
        /// <param name="healthValue"></param>
        public void IncreaseHealthValues(float healthValue)
        {
            if (healthValue > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseHealthValues(healthValue);
            }
        }
        /// <summary>
        /// 得到生命数值
        /// </summary>
        /// <returns></returns>
        public float GetCurrentHealth()
        {
            return PlayerKernalDataProxy.GetInstance().GetCurrentHealth();
        }

        /// <summary>
        /// 增加最大生命数值
        /// </summary>
        /// <param name="increaseHealth">增量</param>
        public void IncreaseMaxHealth(float increaseHealth)
        {
            if (increaseHealth > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseMaxHealth(increaseHealth);
            }
        }
        /// <summary>
        /// 得到最大生命数值
        /// </summary>
        /// <returns></returns>
        public float GetMaxHealth()
        {
            return PlayerKernalDataProxy.GetInstance().GetMaxHealth();
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
            if (magicValue > 0)
            {
                PlayerKernalDataProxy.GetInstance().DecreaseMagicValues(magicValue);
            }
        }
        /// <summary>
        /// 增加魔法数值
        /// </summary>
        /// <param name="MagicValue"></param>
        public void IncreaseMagicValues(float MagicValue)
        {
            if (MagicValue > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseMagicValues(MagicValue);
            }
        }
        /// <summary>
        /// 得到当前主角的魔法数值
        /// </summary>
        /// <returns></returns>
        public float GetCurrentMagic()
        {
            return PlayerKernalDataProxy.GetInstance().GetCurrentMagic();
        }
        /// <summary>
        /// 增加最大魔法值
        /// </summary>
        /// <param name="increaseMagic"></param>
        public void IncreaseMaxMagic(float increaseMagic)
        {
            if (increaseMagic > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseMaxMagic(increaseMagic);
            }
        }
        /// <summary>
        /// 得到最大魔法值
        /// </summary>
        /// <returns></returns>
        public float GetMaxMagic()
        {
            return PlayerKernalDataProxy.GetInstance().GetMaxMagic();
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
            PlayerKernalDataProxy.GetInstance().UpdateATKValues(newWeaponValues);
        }
        /// <summary>
        /// 得到当前攻击力
        /// </summary>
        /// <returns></returns>
        public float GetCurrentATK()
        {
            return PlayerKernalDataProxy.GetInstance().GetCurrentATK();
        }
        /// <summary>
        /// 增加最大攻击力
        /// </summary>
        /// <param name="increaseATK"></param>
        public void IncreaseMaxATK(float increaseATK)
        {
            if (increaseATK > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseMaxATK(increaseATK);
            }
        }
        /// <summary>
        /// 得到最大攻击力
        /// </summary>
        /// <returns></returns>
        public float GetMaxATK()
        {
            return PlayerKernalDataProxy.GetInstance().GetMaxATK();
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
            PlayerKernalDataProxy.GetInstance().UpdateDEFValues(newWeaponDEFValues);
        }
        /// <summary>
        /// 得到当前防御力
        /// </summary>
        /// <returns></returns>
        public float GetCurrentDEF()
        {
            return PlayerKernalDataProxy.GetInstance().GetCurrentDEF();
        }
        /// <summary>
        /// 增加最大防御力
        /// </summary>
        /// <param name="increaseDEF"></param>
        public void IncreaseMaxDEF(float increaseDEF)
        {
            if (increaseDEF > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseMaxDEF(increaseDEF);
            }
        }
        /// <summary>
        /// 得到最大防御力
        /// </summary>
        /// <returns></returns>
        public float GetMaxDEF()
        {
            return PlayerKernalDataProxy.GetInstance().GetMaxDEF();
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
            PlayerKernalDataProxy.GetInstance().UpdateDEXValues(newWeaponValues);
        }
        /// <summary>
        /// 得到当前敏捷度
        /// </summary>
        /// <returns></returns>
        public float GetCurrentDEX()
        {
            return PlayerKernalDataProxy.GetInstance().GetCurrentDEX();
        }
        /// <summary>
        /// 增加最大敏捷度
        /// </summary>
        /// <param name="increaseDEX"></param>
        public void IncreaseMaxDEX(float increaseDEX)
        {
            if (increaseDEX > 0)
            {
                PlayerKernalDataProxy.GetInstance().IncreaseMaxDEX(increaseDEX);
            }
        }
        /// <summary>
        /// 得到最大敏捷度
        /// </summary>
        /// <returns></returns>
        public float GetMaxDEX()
        {
            return PlayerKernalDataProxy.GetInstance().GetMaxDEX();
        }
        #endregion

        #region 经验值
        /// <summary>
        /// 增加经验值
        /// </summary>
        /// <param name="addExp">经验数值</param>
        public void AddExp(int addExp)
        {
            if (addExp > 0)
            {
                PlayerExternalDataProxy.GetInstance().AddExp(addExp);
            }
        }
        /// <summary>
        /// 得到经验值
        /// </summary>
        /// <returns></returns>
        public int GetExp()
        {
            return PlayerExternalDataProxy.GetInstance().GetExp();
        }
        #endregion

        #region 杀敌数量
        /// <summary>
        /// 增加杀敌数量
        /// </summary>
        public void AddKillNumber()
        {
            PlayerExternalDataProxy.GetInstance().AddKillNumber();
        }
        /// <summary>
        /// 得到杀敌数量
        /// </summary>
        /// <returns></returns>
        public int GetKillNumber()
        {
            return PlayerExternalDataProxy.GetInstance().GetKillNumber();
        }
        #endregion

        #region 等级
        /// <summary>
        /// 增加等级
        /// </summary>
        public void AddLevel()
        {
            PlayerExternalDataProxy.GetInstance().AddLevel();
        }
        /// <summary>
        /// 得到等级
        /// </summary>
        /// <returns></returns>
        public int GetLevel()
        {
            return PlayerExternalDataProxy.GetInstance().GetLevel();
        }
        #endregion

        #region 金币
        /// <summary>
        /// 增加金币
        /// </summary>
        /// <param name="goldNumber"></param>
        public void AddGold(int goldNumber)
        {
            if (goldNumber > 0)
            {
                PlayerExternalDataProxy.GetInstance().AddGold(goldNumber);
            }
        }
        /// <summary>
        /// 得到金币
        /// </summary>
        /// <returns></returns>
        public int GetGold()
        {
            return PlayerExternalDataProxy.GetInstance().GetGold();
        }
        #endregion

        #region 钻石
        /// <summary>
        /// 增加钻石
        /// </summary>
        public void AddDiamonds(int diamondNumber)
        {
            if (diamondNumber > 0)
            {
                PlayerExternalDataProxy.GetInstance().AddDiamonds(diamondNumber);
            }
        }
        /// <summary>
        /// 得到钻石
        /// </summary>
        /// <returns></returns>
        public int GetDiamonds()
        {
            return PlayerExternalDataProxy.GetInstance().GetDiamonds();
        }
        #endregion
    }
}