/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:主角攻击
 *
 *	Description:
 *		攻击动作实现脚本调用：Ctrl_HeroAttackInput -> Ctrl_HeroAttack -> Ctrl_HeroAnimationCtrl
 *		
 *		
 *		开发思路：
 *		1.把附近的所有敌人放入“敌人数组”
 *		    1.1 得到所有敌人，放入“敌人集合”
 *		    1.2 判断“敌人集合”，找出最近敌人
 *		2.主角在一定范围内，开始自动“注视”最近的敌人
 *		3.响应输入攻击信号，对于主角“正面”的敌人给与一定伤害处理
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
    public class Ctrl_HeroAttack : BaseControl
    {
        public float FloMinAttackDistance = 5;                                 //最小攻击距离（关注）
        public float FloHeroRotationSpeed = 1F;                                //主角旋转速率
        public float FloRealAttackArea = 2F;                                   //主角实际有效攻击距离
        //关于大招攻击参数定义
        public float FloAttackAreaByMagicA = 4F;                               //大招A的攻击范围
        public float FloAttackAreaByMagicB = 8F;                               //大招B的攻击范围
        public int IntAttackPowerMultipleByMagicA = 5;                         //大招A的攻击力倍率
        public int IntAttackPowerMultipleByMagicB = 20;                        //大招B的攻击力倍率

        private List<GameObject> _ListEnemys;                                  //敌人集合
        private Transform _TraNearestEnemy;                                    //最近敌人方位
        private float _FloMaxDistance = 10;                                    //敌我最大距离（放入“敌人数组”）

        void Awake()
        {
            //事件注册:（多播委托） 主角攻击输入（键盘的事件）
            //#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            Ctrl_HeroAttackInputByKey.evePlayerControl += ResponseNormalAttack;
            Ctrl_HeroAttackInputByKey.evePlayerControl += ResponseMagicTrickA;
            Ctrl_HeroAttackInputByKey.evePlayerControl += ResponseMagicTrickB;
            //#endif
            //主角攻击输入（虚拟按键的事件）
            //#if UNITY_ANDROID || UNITY_IPHONE
            Ctrl_HeroAttackInputByET.evePlayerControl += ResponseNormalAttack;
            Ctrl_HeroAttackInputByET.evePlayerControl += ResponseMagicTrickA;
            Ctrl_HeroAttackInputByET.evePlayerControl += ResponseMagicTrickB;
            Ctrl_HeroAttackInputByET.evePlayerControl += ResponseMagicTrickC;
            Ctrl_HeroAttackInputByET.evePlayerControl += ResponseMagicTrickD;
            //#endif
        }

        void Start()
        {
            _ListEnemys = new List<GameObject>();
            StartCoroutine("RecordNearbyEnemysToArray");
            StartCoroutine("HeroRotationEnemy");
        }

        #region 响应攻击输入
        /// <summary>
        /// 响应普通攻击
        /// </summary>
        /// <param name="controlType"></param>
        public void ResponseNormalAttack(string controlType)
        {
            if (controlType == "NormalAttack")
            {
                //播放攻击动画
                Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.NormalAttack);
                //给特定敌人以伤害处理
                //if (UnityHelper.GetInstance().GetSmallTime(GlobalParameter.INTEVRAL_TIME_0DOT1))
                {
                    AttackEnemyByNormal();
                }
            }
        }

        /// <summary>
        /// 响应大招A
        /// </summary>
        public void ResponseMagicTrickA(string controlType)
        {
            if (controlType == "MagicTrickA")
            {
                //播放攻击动画
                Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicTrickA);

                //给特定敌人以伤害处理
                StartCoroutine("AttackEnemyByMagicA");
            }
        }

        /// <summary>
        /// 响应大招B
        /// </summary>
        public void ResponseMagicTrickB(string controlType)
        {
            if (controlType == "MagicTrickB")
            {
                //播放攻击动画
                Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicTrickB);

                //给特定敌人以伤害处理
            }
        }

        /// <summary>
        /// 响应大招C
        /// </summary>
        public void ResponseMagicTrickC(string controlType)
        {
            if (controlType == "MagicTrickC")
            {
                //播放攻击动画
                Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicTrickC);

                //给特定敌人以伤害处理
            }
        }

        /// <summary>
        /// 响应大招D
        /// </summary>
        public void ResponseMagicTrickD(string controlType)
        {
            if (controlType == "MagicTrickD")
            {
                //播放攻击动画
                Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicTrickD);

                //给特定敌人以伤害处理
            }
        }
        #endregion

        /// <summary>
        /// 把附近敌人放入“敌人数组”
        /// </summary>
        IEnumerator RecordNearbyEnemysToArray()
        {
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_2);
                GetEnemysToArray();
                GetNearestEnemy();
            }
        }

        /// <summary>
        /// 得到所有(活着的)敌人，放入“敌人集合”
        /// </summary>
        private void GetEnemysToArray()
        {
            GameObject[] goEnemys = GameObject.FindGameObjectsWithTag(Tag.Tag_Enemys);
            _ListEnemys.Clear();
            foreach (GameObject goItem in goEnemys)
            {
                //判断敌人是否存活
                //Ctrl_Enemy enemy = goItem.GetComponent<Ctrl_Enemy>();
                //if (enemy && enemy.IsAlive)
                Ctrl_Warrior_Property enemy = goItem.GetComponent<Ctrl_Warrior_Property>();
                if (enemy && enemy.CurrentState != EnemyState.Dead)
                {
                    _ListEnemys.Add(goItem);
                }
            }
        }

        /// <summary>
        /// 判断“敌人集合”，找最近敌人
        /// </summary>
        private void GetNearestEnemy()
        {
            if (_ListEnemys != null && _ListEnemys.Count >= 1)
            {
                foreach (GameObject goEnemy in _ListEnemys)
                {
                    float floDistance = Vector3.Distance(this.gameObject.transform.position, goEnemy.transform.position);
                    if (floDistance < _FloMaxDistance)
                    {
                        _FloMaxDistance = floDistance;
                        _TraNearestEnemy = goEnemy.transform;
                    }
                }
            }
        }

        /// <summary>
        /// 主角“注视”最近的敌人
        /// </summary>
        /// <returns></returns>
        IEnumerator HeroRotationEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
                if (_TraNearestEnemy != null && Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle)
                {
                    float floDistance = Vector3.Distance(this.gameObject.transform.position, _TraNearestEnemy.position);
                    if (floDistance < FloMinAttackDistance)
                    {
                        //this.transform.LookAt(_TraNearestEnemy); //LookAt角度有问题，采用注视旋转
                        //(被替代)
                        //this.transform.rotation = Quaternion.Slerp(
                        //    this.transform.rotation,
                        //    Quaternion.LookRotation(new Vector3(_TraNearestEnemy.position.x, 0, _TraNearestEnemy.position.z) -
                        //        new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)),
                        //    FloHeroRotationSpeed);
                        UnityHelper.GetInstance().FaceToGoal(this.gameObject.transform, _TraNearestEnemy, FloHeroRotationSpeed);
                    }
                }
            }
        }

        /// <summary>
        /// 普攻
        /// </summary>
        private void AttackEnemyByNormal()
        {
            base.AttackEnemy(_ListEnemys, _TraNearestEnemy, FloRealAttackArea, 1, true);
        }

        /// <summary>
        /// 使用大招A，攻击敌人
        /// 功能： 主角周边范围，都造成一定的伤害
        /// </summary>
        IEnumerator AttackEnemyByMagicA()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
            base.AttackEnemy(_ListEnemys, _TraNearestEnemy, FloAttackAreaByMagicA, IntAttackPowerMultipleByMagicA, false);
        }

        /// <summary>
        /// 使用大招B，攻击敌人
        /// 功能： 主角的正对面方向，造成较大的伤害
        /// </summary>
        IEnumerator AttackEnemyByMagicB()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
            base.AttackEnemy(_ListEnemys, _TraNearestEnemy, FloAttackAreaByMagicB, IntAttackPowerMultipleByMagicB);
        }

    }
}