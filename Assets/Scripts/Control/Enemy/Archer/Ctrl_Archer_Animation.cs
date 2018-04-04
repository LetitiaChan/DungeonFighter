using UnityEngine;
using System.Collections;
using Kernal;
using Global;

namespace Control
{
    public class Ctrl_Archer_Animation : BaseControl
    {
        public GameObject goArrowPropByArcher;
        public GameObject goEnemyHurtedEffectPrefab;
        public GameObject goArrowPrefab;

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
            var goHero = GameObject.FindGameObjectWithTag(Tag.Player);
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
                                Ctrl_LevelTwoScenes.Instance.KillNum_archer += 1;
                        }
                        break;
                }
            }
        }

        public IEnumerator AnimationEvent_ArcherAttack()
        {
            //StartCoroutine(LoadParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1,
            //    "Prefabs/Props/Arrow", true, goArrowPropByArcher.transform.position, goArrowPropByArcher.transform.rotation, 
            //    transform.parent, null, 15));
            StartCoroutine(LoadParticalEffect_UsePool(GlobalParameter.INTERVAL_TIME_0DOT1, goArrowPrefab,
                goArrowPropByArcher.transform.position, goArrowPropByArcher.transform.rotation));
            yield break;
        }

        public IEnumerator AnimationEvent_ArcherHurt()
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
