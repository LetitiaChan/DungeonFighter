/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:敌人（属性）
 *
 *	Description:
 *		1.
 *
 *	Date:2017.02.22
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
    public class Ctrl_Enemy : BaseControl
    {
        public int IntHeroExpenrence = 5;                                      //英雄的经验数值
        private bool _IsAlive = true;                                          //是否生存
        public int IntMaxHealth = 20;                                          //最大的生命值
        private float _FloCurrentHealth = 0;                                   //当前生命值

        /// <summary>
        /// 属性：是否存活
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return _IsAlive;
            }
        }

        void Start()
        {
            _FloCurrentHealth = IntMaxHealth;
            StartCoroutine("CheckLifeContinue");
        }

        /// <summary>
        /// 检查是否存活
        /// </summary>
        /// <returns></returns>
        IEnumerator CheckLifeContinue()
        {
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
                if (_FloCurrentHealth <= IntMaxHealth * 0.01)
                {
                    _IsAlive = false;
                    //增加英雄经验值
                    Ctrl_HeroProperty.Instance.AddExp(IntHeroExpenrence);
                    //增加杀敌数
                    Ctrl_HeroProperty.Instance.AddKillNumber();
                    //销毁对象
                    Destroy(this.gameObject);
                }
            }
        }

        /// <summary>
        /// 伤害处理
        /// </summary>
        /// <param name="intHurt"></param>
        public void OnHurt(int intHurt)
        {
            int hurtValue = 0;
            hurtValue = Mathf.Abs(intHurt);
            if (hurtValue > 0)
            {
                _FloCurrentHealth -= hurtValue;
            }
            print("_FloCurrentHealth:" + _FloCurrentHealth);
        }

    }
}