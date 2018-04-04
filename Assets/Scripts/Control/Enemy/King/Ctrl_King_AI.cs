using UnityEngine;
using System.Collections;
using Kernal;
using Global;

namespace Control
{
    public class Ctrl_King_AI : BaseControl
    {
        public float moveSpeed = 1f;
        public float rotateSpeed = 1f;
        public float attackDistance = 1f;
        public float cordonDistance = 5f;
        public float thinkInterval = 3f;

        private GameObject _goHero;
        private Ctrl_BaseEnemyProperty _myProperty;
        private CharacterController _cc;

        /*由于加入对象缓冲池，需要在OnEnable OnDisable里开启停止*/
        void OnEnable()
        {
            StartCoroutine("ThinkProcess");
            StartCoroutine("MovingProcess");
        }
        void OnDisable()
        {
            StopCoroutine("ThinkProcess");
            StopCoroutine("MovingProcess");
        }

        void Start()
        {
            _goHero = GameObject.FindGameObjectWithTag(Tag.Player);
            _myProperty = GetComponent<Ctrl_BaseEnemyProperty>();
            _cc = GetComponent<CharacterController>();

            /* 确定个体差异性参数  */
            moveSpeed = UnityHelper.GetInstance().GetRandomNumByRange(1, 2);
            attackDistance = UnityHelper.GetInstance().GetRandomNumByRange(1, 2);
            cordonDistance = UnityHelper.GetInstance().GetRandomNumByRange(4, 8);
            thinkInterval = UnityHelper.GetInstance().GetRandomNumByRange(2, 4);
        }

        IEnumerator ThinkProcess()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
            while (true)
            {
                yield return new WaitForSeconds(thinkInterval);

                if (_myProperty && _myProperty.CurrentState != EnemyState.Dead)
                {
                    if (Ctrl_HeroAnimationCtrl.Instance && Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.Dead)
                    {
                        Vector3 VecHero = _goHero.transform.position;
                        var distance = Vector3.Distance(VecHero, transform.position);
                        if (distance < attackDistance)
                        {
                            _myProperty.CurrentState = EnemyState.Attack;
                        }
                        else if (distance < cordonDistance)//警戒（追击）
                        {
                            _myProperty.CurrentState = EnemyState.Walking;
                        }
                        else
                        {
                            _myProperty.CurrentState = EnemyState.Idle;
                        }
                    }
                    else
                    {
                        _myProperty.CurrentState = EnemyState.Idle;
                    }
                }
            }
        }

        IEnumerator MovingProcess()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);

            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT02);

                if (_myProperty && _myProperty.CurrentState != EnemyState.Dead)
                {
                    FaceToHero();
                    switch (_myProperty.CurrentState)
                    {
                        case EnemyState.Walking:
                            var vec = Vector3.ClampMagnitude((_goHero.transform.position - transform.position), moveSpeed * Time.deltaTime);
                            _cc.Move(vec);
                            break;
                        case EnemyState.Hurt:
                            var vect = -base.transform.forward * moveSpeed / 2 * Time.deltaTime;
                            _cc.Move(vect); //敌人受伤后退效果
                            break;
                    }
                }
            }
        }

        private void FaceToHero()
        {
            UnityHelper.GetInstance().FaceToGoal(transform, _goHero.transform, rotateSpeed);
        }
    }
}
