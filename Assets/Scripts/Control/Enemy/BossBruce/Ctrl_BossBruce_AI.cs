using UnityEngine;
using System.Collections;
using Global;
using Kernal;

namespace Control
{
    public class Ctrl_BossBruce_AI : BaseControl
    {

        public float moveSpeed = 2f;
        public float rotateSpeed = 1f;
        public float attackDistance = 5f;
        public float cordonDistance = 10f;
        public float thinkInterval = 1f;

        private GameObject _goHero;
        private Ctrl_BaseEnemyProperty _myProperty;
        private CharacterController _cc;

        void Start()
        {
            StartCoroutine("ThinkProcess");
            StartCoroutine("MovingProcess");

            _goHero = GameObject.FindGameObjectWithTag(Tag.Player);
            _myProperty = GetComponent<Ctrl_BaseEnemyProperty>();
            _cc = GetComponent<CharacterController>();
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