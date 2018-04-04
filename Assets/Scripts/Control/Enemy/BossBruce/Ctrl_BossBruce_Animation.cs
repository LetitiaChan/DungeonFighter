using UnityEngine;
using System.Collections;
using Global;
using Kernal;

namespace Control
{
    public class Ctrl_BossBruce_Animation : BaseControl
    {
        public GameObject goEnemyHurtedEffectPrefab;
        private Ctrl_BaseEnemyProperty _myProperty;
        private Ctrl_HeroProperty _heroProperty;
        private Animator _animator;
        private GameObject goHero;
        private bool _isSingleTimes = true;

        void Start()
        {
            StartCoroutine("PlayAnimation_A");
            StartCoroutine("PlayAnimation_B");

            _myProperty = GetComponent<Ctrl_BaseEnemyProperty>();
            _animator = GetComponent<Animator>();
            goHero = GameObject.FindGameObjectWithTag(Tag.Player);
            if (goHero)
            {
                _heroProperty = goHero.GetComponent<Ctrl_HeroProperty>();
            }
        }

        IEnumerator PlayAnimation_A()
        {
            yield return new WaitForEndOfFrame();

            AudioManager.PlayAudioEffectA("BossShout1");

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
                                Ctrl_LevelTwoScenes.Instance.BattleEnd();
                        }
                        break;
                }
            }
        }

        IEnumerator AnimationEvent_BossBruceAttackNormal()
        {
            if (_heroProperty)
                if (Vector3.Distance(transform.position, goHero.transform.position) < GetComponent<Ctrl_BossBruce_AI>().attackDistance)
                    _heroProperty.DecreaseHealthValues(_myProperty.ATK);


            StartCoroutine(LoadParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/Enemy_HurtedEffect", true, transform.position +
                transform.TransformDirection(new Vector3(0, 0, 3f)), transform.rotation,
                transform, "BossATK1", 1));
            yield break;
        }

        IEnumerator AnimationEvent_BossBruceAttackJump()
        {
            if (_heroProperty)
                if (Vector3.Distance(transform.position, goHero.transform.position) < GetComponent<Ctrl_BossBruce_AI>().attackDistance)
                    _heroProperty.DecreaseHealthValues(_myProperty.ATK * 2);

            StartCoroutine(LoadParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/BossBruceSkill", true,
                transform.position + new Vector3(0, 0, -3f),
                transform.rotation,
                transform.parent, "BossATKJump", 2));
            yield break;
        }

        IEnumerator AnimationEvent_BossBruceHurt()
        {
            //StartCoroutine(LoadParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1,
            //    "ParticleProps/Enemy_HurtedEffect", true, transform.position, transform.rotation,
            //    transform, null, 1));
            StartCoroutine(LoadParticalEffect_UsePool(GlobalParameter.INTERVAL_TIME_0DOT1, goEnemyHurtedEffectPrefab,
                transform.position, Quaternion.identity));
            yield break;
        }

        IEnumerator AnimationEvent_BossBruceDead()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1);
            AudioManager.PlayAudioEffectA("BossDeath");
        }
    }
}