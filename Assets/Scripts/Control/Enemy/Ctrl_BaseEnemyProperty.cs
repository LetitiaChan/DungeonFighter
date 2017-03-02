/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层：敌人属性父类
 *
 *	Description:
 *		1.运用“重构”的思想，来构造更加灵活与低耦合度的系统
 *		2.包含所有敌人的公共属性
 *
 *	Date:2017.02.27
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

namespace Control
{
    public class Ctrl_BaseEnemyProperty : BaseControl
    {
        public int HeroExpenrence = 0;                                         //英雄的经验数值
        public int ATK = 0;                                                    //攻击力
        public int DEF = 0;                                                    //防御力
        public int MaxHealth = 0;                                              //最大的生命数值

        private float _FloCurrentHealth = 0;                                   //当前生命数值 
        private EnemyState _CurrentState = EnemyState.Idle;                    //当前状态

        /// <summary>
        /// 属性： 当前的状态
        /// </summary>
        public EnemyState CurrentState
        {
            get { return _CurrentState; }
            set { _CurrentState = value; }
        }

        void OnEnable()
        {
            //判断是否生命存活
            StartCoroutine("CheckLifeContinue");
        }

        void OnDisable()
        {
            //判断是否生命存活
            StopCoroutine("CheckLifeContinue");
        }


        /// <summary>
        /// 在子类中运行的方法
        /// </summary>
        public void RunMethodInChilden()
        {
            _FloCurrentHealth = MaxHealth;
            //判断是否生命存活
            //StartCoroutine("CheckLifeContinue");
        }

        /// <summary>
        /// 判断是否生命存活
        /// </summary>
        /// <returns></returns>
        IEnumerator CheckLifeContinue()
        {
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);

                if (_FloCurrentHealth <= MaxHealth * 0.01)
                {
                    //_IsAlive = false;
                    _CurrentState = EnemyState.Dead;
                    //关于英雄增加相关数值。
                    //增加经验值。
                    Ctrl_HeroProperty.Instance.AddExp(HeroExpenrence);
                    //增加杀敌数量
                    Ctrl_HeroProperty.Instance.AddKillNumber();
                    //死亡状态
                    _CurrentState = EnemyState.Dead;
                    //销毁对象
                    Destroy(this.gameObject, 5F);//5秒死亡延迟
                    //回收对象
                    StartCoroutine("RecoverEnemys");
                }
            }
        }

        /// <summary>
        /// 伤害处理
        /// </summary>
        /// <param name="hurtValue"></param>
        public void OnHurt(int hurtValue)
        {
            //print("伤害处理受到了！！！！");
            int hurtValues = 0;

            //受伤状态
            _CurrentState = EnemyState.Hurt;
            hurtValues = Mathf.Abs(hurtValue);
            if (hurtValues > 0)
            {
                _FloCurrentHealth -= hurtValues;
            }
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <returns></returns>
        IEnumerator RecoverEnemys()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_3);
            //敌人回收前状态重置
            _FloCurrentHealth = MaxHealth;
            _CurrentState = EnemyState.Idle;
            //回收对象
            PoolManager.PoolsArray["_Enemys"].RecoverGameObjectToPools(this.gameObject);
        }
    }
}
