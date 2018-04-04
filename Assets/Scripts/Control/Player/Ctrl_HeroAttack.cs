using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Global;
using Kernal;

namespace Control
{
    /// <summary>
    /// 实现主角攻击    攻击动作实现脚本调用：Ctrl_HeroAttackInput -> Ctrl_HeroAttack -> Ctrl_HeroAnimationCtrl
    /// 开发思路：
    ///     1.把附近的所有敌人放入“敌人数组”
    ///         1.1 得到所有敌人，放入“敌人集合”
    ///         1.2 判断“敌人集合”，找出最近敌人
    ///     2.主角在一定范围内，开始自动“注视”最近的敌人
    ///     3.响应输入攻击信号，对于主角“正面”的敌人给与一定伤害处理
    /// </summary>
    public class Ctrl_HeroAttack : BaseControl
    {
        public float minAttackDistance = 5;
        public float heroRotationSpeed = 1f;
        public float realAttackArea = 2f;
        //关于大招攻击参数定义
        public float attackAreaByMagicA = 4f;
        public float attackAreaByMagicB = 8f;
        public float attackAreaByMagicC = 8f;
        public float attackAreaByMagicD = 4f;
        public int attackPowerMultipleByMagicA = 2;
        public int attackPowerMultipleByMagicB = 5;
        public int attackPowerMultipleByMagicC = 10;
        public int attackPowerMultipleByMagicD = 10;


        private List<GameObject> _enemys;
        private Transform _nearestEnemy;
        private float _maxDistance = 10;


        void Awake()
        {
            //事件注册:（多播委托） 主角攻击输入（键盘的事件）
            //#if UNITY_STANDALONE_WIN || UNITY_EDITOR
#if UNITY_STANDALONE_WIN
            Ctrl_HeroAttackInputByKey.evePlayerControl += ResponseNormalAttack;
            Ctrl_HeroAttackInputByKey.evePlayerControl += ResponseMagicTrickA;
            Ctrl_HeroAttackInputByKey.evePlayerControl += ResponseMagicTrickB;
#endif
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
            _enemys = new List<GameObject>();
            StartCoroutine("RecordNearbyEnemysToArray");
            StartCoroutine("HeroRotationEnemy");
        }

        #region 响应攻击输入
        public void ResponseNormalAttack(string controlType)
        {
            if (controlType == "NormalAttack")
            {
                Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.NormalAttack);
                //if (UnityHelper.GetInstance().GetSmallTime(GlobalParameter.INTEVRAL_TIME_0DOT1))
                {
                    AttackEnemyByNormal();
                }
            }
        }

        public void ResponseMagicTrickA(string controlType)
        {
            if (controlType == "MagicTrickA")
            {
                Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicTrickA);

                StartCoroutine("AttackEnemyByMagicA");
            }
        }

        public void ResponseMagicTrickB(string controlType)
        {
            if (controlType == "MagicTrickB")
            {
                Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicTrickB);

                StartCoroutine("AttackEnemyByMagicB");
            }
        }

        public void ResponseMagicTrickC(string controlType)
        {
            if (controlType == "MagicTrickC")
            {
                Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicTrickC);

                StartCoroutine("AttackEnemyByMagicC");
            }
        }

        public void ResponseMagicTrickD(string controlType)
        {
            if (controlType == "MagicTrickD")
            {
                Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.MagicTrickD);

                StartCoroutine("AttackEnemyByMagicD");
            }
        }
        #endregion

        IEnumerator RecordNearbyEnemysToArray()
        {
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_2);
                GetEnemysToArray();
                GetNearestEnemy();
            }
        }

        private void GetEnemysToArray()
        {
            GameObject[] goEnemys = GameObject.FindGameObjectsWithTag(Tag.Tag_Enemys);
            _enemys.Clear();
            foreach (GameObject goItem in goEnemys)
            {
                Ctrl_BaseEnemyProperty enemy = goItem.GetComponent<Ctrl_BaseEnemyProperty>();
                if (enemy && enemy.CurrentState != EnemyState.Dead)
                {
                    _enemys.Add(goItem);
                }
            }
        }

        private void GetNearestEnemy()
        {
            if (_enemys != null && _enemys.Count >= 1)
            {
                foreach (GameObject goEnemy in _enemys)
                {
                    float floDistance = Vector3.Distance(transform.position, goEnemy.transform.position);
                    if (floDistance < _maxDistance)
                    {
                        _maxDistance = floDistance;
                        _nearestEnemy = goEnemy.transform;
                    }
                }
            }
        }

        IEnumerator HeroRotationEnemy()
        {
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
                if (_nearestEnemy != null && Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle)
                {
                    float floDistance = Vector3.Distance(transform.position, _nearestEnemy.position);
                    if (floDistance < minAttackDistance)
                    {
                        UnityHelper.GetInstance().FaceToGoal(transform, _nearestEnemy, heroRotationSpeed);
                    }
                }
            }
        }

        private void AttackEnemyByNormal()
        {
            AttackEnemy(_enemys, _nearestEnemy, realAttackArea, 1);
        }

        /// <summary>
        /// 使用大招A，攻击敌人
        /// 功能： 主角周边范围，都造成一定的伤害
        /// </summary>
        IEnumerator AttackEnemyByMagicA()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
            AttackEnemy(_enemys, _nearestEnemy, attackAreaByMagicA, attackPowerMultipleByMagicA, false);
        }

        /// <summary>
        /// 使用大招B，攻击敌人
        /// 功能： 主角的正对面方向，造成较大的伤害
        /// </summary>
        IEnumerator AttackEnemyByMagicB()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
            AttackEnemy(_enemys, _nearestEnemy, attackAreaByMagicB, attackPowerMultipleByMagicB);
        }

        /// <summary>
        /// 使用大招C，攻击敌人
        /// 功能： 主角周边范围，都造成一定的伤害
        /// </summary>
        IEnumerator AttackEnemyByMagicC()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
            AttackEnemy(_enemys, _nearestEnemy, attackAreaByMagicC, attackPowerMultipleByMagicC, false);
        }

        /// <summary>
        /// 使用大招D，攻击敌人
        /// 功能： 主角周边范围，都造成一定的伤害
        /// </summary>
        IEnumerator AttackEnemyByMagicD()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
            AttackEnemy(_enemys, _nearestEnemy, attackAreaByMagicD, attackPowerMultipleByMagicD, false);
        }


        /// <summary>
        /// 公共方法：攻击敌人
        /// </summary>
        /// <param name="attackArea">攻击范围</param>
        /// <param name="attackPowerMultiple">攻击力度（倍率）</param>
        /// <param name="isDirection">攻击是否有方向性</param>
        void AttackEnemy(List<GameObject> lisEnemys, Transform traNearestEnemy, float attackArea, int attackPowerMultiple, bool isDirection = true)
        {
            if (lisEnemys == null || lisEnemys.Count <= 0)
            {
                traNearestEnemy = null;
                return;
            }

            //print("RealATK = " + Ctrl_HeroProperty.Instance.GetCurrentATK() + ", Multiple = " + attackPowerMultiple);
            foreach (GameObject enemyItem in lisEnemys)
            {
                if (enemyItem && enemyItem.GetComponent<Ctrl_BaseEnemyProperty>())
                {
                    if (enemyItem.GetComponent<Ctrl_BaseEnemyProperty>().CurrentState != EnemyState.Dead)
                    {
                        var distance = Vector3.Distance(transform.position, enemyItem.transform.position);
                        //攻击具有方向性
                        if (isDirection)
                        {
                            var dir = (enemyItem.transform.position - transform.position).normalized;
                            var direction = Vector3.Dot(dir, transform.forward);
                            //如果主角与敌人在同一个方向，且在有效攻击范围内，则对敌人做伤害处理
                            if (direction > 0 && distance <= attackArea)
                            {
                                var hurtValue = Ctrl_HeroProperty.Instance.GetCurrentATK() * attackPowerMultiple;
                                enemyItem.SendMessage("OnHurt", hurtValue, SendMessageOptions.DontRequireReceiver);
                            }
                        }
                        //攻击无方向性
                        else
                        {
                            if (distance <= attackArea)
                            {
                                var hurtValue = Ctrl_HeroProperty.Instance.GetCurrentATK() * attackPowerMultiple;
                                enemyItem.SendMessage("OnHurt", hurtValue, SendMessageOptions.DontRequireReceiver);
                            }
                        }
                    }
                }
            }
        }
    }
}