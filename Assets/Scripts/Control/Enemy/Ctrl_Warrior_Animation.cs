/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层：敌人战士动画系统
 *
 *	Description:
 *		1.敌人动画
 *		2.敌人特技处理
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
    public class Ctrl_Warrior_Animation : BaseControl
    {
        private Ctrl_BaseEnemyProperty _MyProperty;                            //本身属性
        private Ctrl_HeroProperty _HeroProperty;                               //英雄属性
        private Animator _Animator;                                            //战士的动画状态机
        private bool _IsSingleTimes = true;                                    //单次开关

        void OnEnable()
        {
            //播放战士动画A部分(休闲、走路、攻击)
            StartCoroutine("PlayWarriorAnimation_A");
            //播放战士动画B部分（受伤、死亡）
            StartCoroutine("PlayWarriorAnimation_B");
            //开启单次模式
            _IsSingleTimes = true;
        }

        void OnDisable()
        {
            //播放战士动画A部分(休闲、走路、攻击)
            StopCoroutine("PlayWarriorAnimation_A");
            //播放战士动画B部分（受伤、死亡）
            StopCoroutine("PlayWarriorAnimation_B");
            //敌人的状态恢复为“站立”状态
            if (_Animator != null)
            {
                _Animator.SetTrigger("RecoverLife");
            }
        }

        void Start()
        {
            _MyProperty = this.gameObject.GetComponent<Ctrl_BaseEnemyProperty>();
            _Animator = this.gameObject.GetComponent<Animator>();
            GameObject goHero = GameObject.FindGameObjectWithTag(Tag.Player);
            if (goHero)
            {
                _HeroProperty = goHero.GetComponent<Ctrl_HeroProperty>();
            }
        }

        /// <summary>
        /// 播放战士动画A（休闲、行走、攻击）
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayWarriorAnimation_A()
        {
            yield return new WaitForEndOfFrame();
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1);
                switch (_MyProperty.CurrentState)
                {
                    case EnemyState.Idle:
                        _Animator.SetFloat("MoveSpeed", 0);
                        _Animator.SetBool("Attack", false);
                        break;
                    case EnemyState.Walking:
                        _Animator.SetBool("Attack", false);
                        _Animator.SetFloat("MoveSpeed", 1);
                        break;
                    case EnemyState.Attack:
                        _Animator.SetFloat("MoveSpeed", 0);
                        _Animator.SetBool("Attack", true);
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 播放战士动画B（受伤、死亡）
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayWarriorAnimation_B()
        {
            yield return new WaitForEndOfFrame();
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
                switch (_MyProperty.CurrentState)
                {
                    case EnemyState.Hurt:
                        _Animator.SetTrigger("Hurt");
                        break;
                    case EnemyState.Dead:
                        if (_IsSingleTimes)
                        {
                            _IsSingleTimes = false;
                            _Animator.SetTrigger("Dead");
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 攻击主角（动画事件）
        /// </summary>
        public void AttackHeroByAnimationEvent()
        {
            if (_HeroProperty)
            {
                print("AttackHeroByAnimationEvent:  " + _MyProperty.ATK);
                _HeroProperty.DecreaseHealthValues(_MyProperty.ATK);
            }
        }

        /// <summary>
        /// 战士受伤动画效果
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimationEvent_WarriorHurt()
        {
            StartCoroutine(LoadParticalEffectPublicMethod(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/Enemy_HurtedEffect", true, transform.position,
                transform, null, 1));
            yield break;
        }
    }
}