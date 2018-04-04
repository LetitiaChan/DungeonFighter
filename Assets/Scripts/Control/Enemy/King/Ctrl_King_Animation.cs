using UnityEngine;
using System.Collections;
using Kernal;
using Global;

namespace Control
{
    public class Ctrl_King_Animation : BaseControl
    {
        public GameObject goEnemyHurtedEffectPrefab;

        private GameObject goHero;
        private Ctrl_BaseEnemyProperty _myProperty;
        private Ctrl_HeroProperty _heroProperty;
        private Animator _animator;
        private bool _isSingleTimes = true;

        /*由于加入对象缓冲池，需要在OnEnable OnDisable里开启停止*/
        void OnEnable()
        {
            StartCoroutine("PlayAnimation_A");
            StartCoroutine("PlayAnimation_B");
            _isSingleTimes = true;
        }
        void OnDisable()
        {
            StopCoroutine("PlayAnimation_A");
            StopCoroutine("PlayAnimation_B");

            if (_animator != null)
            {
                _animator.SetTrigger("RecoverLife");//敌人的状态恢复为“站立”状态
            }
        }

        void Start()
        {
            _myProperty = GetComponent<Ctrl_BaseEnemyProperty>();
            _animator = GetComponent<Animator>();
            goHero = GameObject.FindGameObjectWithTag(Tag.Player);
            if (goHero)
            {
                _heroProperty = goHero.GetComponent<Ctrl_HeroProperty>();
            }
        }

        /// <summary>
        /// 播放动画A（休闲、行走、攻击）
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayAnimation_A()
        {
            yield return new WaitForEndOfFrame();

            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1);

                switch (_myProperty.CurrentState)
                {
                    case EnemyState.Idle:
                        _animator.SetFloat("MoveSpeed", 0);
                        _animator.SetBool("Attack", false);
                        break;
                    case EnemyState.Walking:
                        _animator.SetBool("Attack", false);
                        _animator.SetFloat("MoveSpeed", 1);
                        break;
                    case EnemyState.Attack:
                        _animator.SetFloat("MoveSpeed", 0);
                        _animator.SetBool("Attack", true);
                        break;
                }
            }
        }
        /// <summary>
        /// 播放动画B（受伤、死亡）
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayAnimation_B()
        {
            yield return new WaitForEndOfFrame();

            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);

                switch (_myProperty.CurrentState)
                {
                    case EnemyState.Hurt:
                        //Log.Write(GetType() + "/PlayAnimation_B()/King敌人受伤");
                        _animator.SetTrigger("Hurt");
                        yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_1);
                        if (_myProperty.CurrentState == EnemyState.Hurt)
                            _myProperty.CurrentState = EnemyState.Attack;
                        break;
                    case EnemyState.Dead:
                        if (_isSingleTimes)
                        {
                            _isSingleTimes = false;
                            _animator.SetTrigger("Dead");
                            if (Ctrl_LevelTwoScenes.Instance)
                                Ctrl_LevelTwoScenes.Instance.KillNum_king += 1;
                        }
                        break;
                }
            }
        }

        public IEnumerator AnimationEvent_KingAttack()
        {
            if (_heroProperty)
                if (Vector3.Distance(transform.position, goHero.transform.position) < GetComponent<Ctrl_King_AI>().attackDistance)
                    _heroProperty.DecreaseHealthValues(_myProperty.ATK);
            yield break;
        }

        public IEnumerator AnimationEvent_KingHurt()
        {
            //StartCoroutine(LoadParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1,
            //    "ParticleProps/Enemy_HurtedEffect", true, transform.position, transform.rotation,
            //    transform, null, 1));
            StartCoroutine(LoadParticalEffect_UsePool(GlobalParameter.INTERVAL_TIME_0DOT1, goEnemyHurtedEffectPrefab,
                transform.position, Quaternion.identity));
            yield break;
        }

    }
}
