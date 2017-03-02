/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层：敌人战士AI系统
 *
 *	Description:
 *		1.思考过程
 *		2.移动处理
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
    public class Ctrl_Warrior_AI : BaseControl
    {
        public float FloMoveSpeed = 1F;                                        //移动的速度
        public float FloRotatSpeed = 1F;                                       //旋转的速度
        public float FloAttackDistance = 1F;                                   //攻击距离
        public float FloCordonDistance = 5F;                                   //警戒距离
        public float FloThinkInterval = 1F;                                    //思考的间隔时间

        private GameObject _GoHero;                                            //主角
        private Transform _MyTransform;                                        //当期战士（敌人）方位
        private Ctrl_BaseEnemyProperty _MyProperty;                            //属性
        private CharacterController _cc;                                       //角色控制器

        void OnEnable()
        {
            //开启“思考”协程
            StartCoroutine("ThinkProcess");
            //开发“移动”协程
            StartCoroutine("MovingProcess");
        }

        void OnDisable()
        {
            //开启“思考”协程
            StopCoroutine("ThinkProcess");
            //开发“移动”协程
            StopCoroutine("MovingProcess");
        }

        void Start()
        {
            _MyTransform = this.gameObject.transform;
            _GoHero = GameObject.FindGameObjectWithTag(Tag.Player);
            _MyProperty = this.gameObject.GetComponent<Ctrl_BaseEnemyProperty>();
            _cc = this.gameObject.GetComponent<CharacterController>();

            /* 确定个体差异性参数  */
            FloMoveSpeed = UnityHelper.GetInstance().GetRandomNum(1, 2);
            //FloAttackDistance = UnityHelper.GetInstance().GetRandomNum(1, 3);
            FloCordonDistance = UnityHelper.GetInstance().GetRandomNum(4, 15);
            FloThinkInterval = UnityHelper.GetInstance().GetRandomNum(1, 3);
        }
        /// <summary>
        /// 思考协程
        /// </summary>
        /// <returns></returns>
        IEnumerator ThinkProcess()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
            while (true)
            {
                yield return new WaitForSeconds(FloThinkInterval);
                if (_MyProperty && _MyProperty.CurrentState != EnemyState.Dead)
                {
                    //得到主角的方位
                    Vector3 VecHero = _GoHero.transform.position;
                    //得到主角与当前（敌人）的距离
                    float floDistance = Vector3.Distance(VecHero, _MyTransform.position);
                    //距离判断
                    if (floDistance < FloAttackDistance)
                    {
                        //攻击状态
                        _MyProperty.CurrentState = EnemyState.Attack;
                    }
                    else if (floDistance < FloCordonDistance)
                    {
                        //警戒（追击）
                        _MyProperty.CurrentState = EnemyState.Walking;
                    }
                    else
                    {
                        //休闲
                        _MyProperty.CurrentState = EnemyState.Idle;
                    }
                }
            }
        }
        /// <summary>
        /// 移动协程
        /// </summary>
        /// <returns></returns>
        IEnumerator MovingProcess()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT02);
                if (_MyProperty && _MyProperty.CurrentState != EnemyState.Dead)
                {
                    //面向主角
                    FaceToHero();
                    //移动
                    switch (_MyProperty.CurrentState)
                    {
                        case EnemyState.Walking:
                            //英雄方位-当前敌人方位
                            Vector3 vec = Vector3.ClampMagnitude((_GoHero.transform.position - _MyTransform.position), FloMoveSpeed * Time.deltaTime);
                            _cc.Move(vec);
                            break;
                        case EnemyState.Hurt:
                            //敌人受伤后退移动
                            Vector3 vect = -transform.forward * FloMoveSpeed / 2 * Time.deltaTime;
                            _cc.Move(vect);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 面向主角
        /// </summary>
        private void FaceToHero()
        {
            //Quaternion.Slerp(
            //                this.transform.rotation,
            //                Quaternion.LookRotation(new Vector3(_GoHero.transform.position.x, 0, _GoHero.transform.position.z) -
            //                    new Vector3(_MyTransform.position.x, 0, _MyTransform.position.z)), FloRotatSpeed);
            UnityHelper.GetInstance().FaceToGoal(_MyTransform, _GoHero.transform, FloRotatSpeed);
        }
    }
}