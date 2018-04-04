using UnityEngine;
using System.Collections;
using Global;
using Kernal;

namespace Control
{
    public class Ctrl_HeroAnimationCtrl : BaseControl
    {
        public static Ctrl_HeroAnimationCtrl Instance;

        public AnimationClip animIdle;
        public AnimationClip animRuning;
        public AnimationClip animDead;
        public AnimationClip animNormalAttack1;
        public AnimationClip animNormalAttack2;
        public AnimationClip animNormalAttack3;
        public AnimationClip animMagicTrickA;
        public AnimationClip animMagicTrickB;
        public AnimationClip animMagicTrickC;
        public AnimationClip animMagicTrickD;

        //对象缓冲池：主角剑法粒子特效
        public GameObject goHeroNormalPartcalEffec1;
        public GameObject goHeroNormalPartcalEffec2;

        //主角音效剪辑
        public AudioClip acHeroRuning;
        public AudioClip acHeroDead;
        public AudioClip AcBeiJi_DaoJian_3;
        public AudioClip AcBeiJi_DaoJian_2;
        public AudioClip AcBeiJi_DaoJian_1;
        public AudioClip AcSwordHero_MagicA;
        public AudioClip AcSwordHero_MagicB;
        public AudioClip AcSwordHero_MagicC;

        public GameObject goMoveUpLabelPrefab;  //飘血文字
        private GameObject goUIPlayerInfo;

        private Animation _animationHandle;
        private HeroActionState _currentActionState = HeroActionState.None;
        private bool _isSinglePlay_Dead = true;
        private NormalATKComboState _curATKCombo = NormalATKComboState.NormalATK1;//动画连招

        public HeroActionState CurrentActionState
        {
            get
            {
                return _currentActionState;
            }
        }

        public GameObject GoUIPlayerInfo
        {
            get
            {
                if (goUIPlayerInfo == null)
                    goUIPlayerInfo = GameObject.FindGameObjectWithTag(Tag.Tag_UIPlayerInfo);
                return goUIPlayerInfo;
            }
        }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            DontDestroyOnLoad(gameObject);

            _animationHandle = GetComponent<Animation>();
            _currentActionState = HeroActionState.Idle;
            StartCoroutine("ControlHeroAnimationState");
            //加快普通连招的播放速度
            _animationHandle[animNormalAttack1.name].speed = 2.5f;
            _animationHandle[animNormalAttack2.name].speed = 2.5f;
            _animationHandle[animNormalAttack3.name].speed = 2f;

            HeroDisplayParticalEffect();
        }


        /// <summary>
        /// 主角的动作状态是否在发大招（ABCD）
        /// </summary>
        /// <returns></returns>
        public bool IsHeroActionMagicTrick()
        {
            return _currentActionState == HeroActionState.MagicTrickA ||
                _currentActionState == HeroActionState.MagicTrickB ||
                _currentActionState == HeroActionState.MagicTrickC ||
                _currentActionState == HeroActionState.MagicTrickD;
        }

        public void HeroDisplayParticalEffect()
        {
            StartCoroutine(LoadParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/Hero_Display", true, transform.position, transform.rotation,
                transform, null, 0));
        }

        public void SetCurrentActionState(HeroActionState currentActionState)
        {
            _currentActionState = currentActionState;
        }

        IEnumerator ControlHeroAnimationState()
        {
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT1);
                switch (CurrentActionState)
                {
                    case HeroActionState.NormalAttack:
                        /* 攻击连招处理(自动状态转换) */
                        switch (_curATKCombo)
                        {
                            case NormalATKComboState.NormalATK1:
                                _curATKCombo = NormalATKComboState.NormalATK2;
                                _animationHandle.CrossFade(animNormalAttack1.name);
                                AudioManager.PlayAudioEffectA(AcBeiJi_DaoJian_3);
                                yield return new WaitForSeconds(animNormalAttack1.length / 2.5f);

                                _currentActionState = HeroActionState.Idle;
                                break;
                            case NormalATKComboState.NormalATK2:
                                _curATKCombo = NormalATKComboState.NormalATK3;
                                _animationHandle.CrossFade(animNormalAttack2.name);
                                AudioManager.PlayAudioEffectA(AcBeiJi_DaoJian_2);
                                yield return new WaitForSeconds(animNormalAttack2.length / 2.5f);

                                _currentActionState = HeroActionState.Idle;
                                break;
                            case NormalATKComboState.NormalATK3:
                                _curATKCombo = NormalATKComboState.NormalATK1;
                                _animationHandle.CrossFade(animNormalAttack3.name);
                                AudioManager.PlayAudioEffectA(AcBeiJi_DaoJian_1);
                                yield return new WaitForSeconds(animNormalAttack3.length / 2f);

                                _currentActionState = HeroActionState.Idle;
                                break;
                            default:
                                break;
                        }
                        break;
                    case HeroActionState.MagicTrickA:
                        _animationHandle.CrossFade(animMagicTrickA.name);
                        AudioManager.PlayAudioEffectA(AcSwordHero_MagicA);
                        yield return new WaitForSeconds(animMagicTrickA.length);

                        if (_currentActionState == HeroActionState.MagicTrickA)
                            _currentActionState = HeroActionState.Idle;
                        break;
                    case HeroActionState.MagicTrickB:
                        _animationHandle.CrossFade(animMagicTrickB.name);
                        AudioManager.PlayAudioEffectA(AcSwordHero_MagicB);
                        yield return new WaitForSeconds(animMagicTrickB.length);

                        if (_currentActionState == HeroActionState.MagicTrickB)
                            _currentActionState = HeroActionState.Idle;
                        break;
                    case HeroActionState.MagicTrickC:
                        _animationHandle.CrossFade(animMagicTrickC.name);
                        AudioManager.PlayAudioEffectA(AcSwordHero_MagicC);
                        yield return new WaitForSeconds(animMagicTrickC.length);

                        if (_currentActionState == HeroActionState.MagicTrickC)
                            _currentActionState = HeroActionState.Idle;
                        break;
                    case HeroActionState.MagicTrickD:
                        _animationHandle.CrossFade(animMagicTrickD.name);
                        //AudioManager.PlayAudioEffectA("SwordHero_MagicD");
                        yield return new WaitForSeconds(animMagicTrickD.length);

                        if (_currentActionState == HeroActionState.MagicTrickD)
                            _currentActionState = HeroActionState.Idle;
                        break;
                    case HeroActionState.None:
                        _animationHandle.CrossFade(animIdle.name);
                        break;
                    case HeroActionState.Idle:
                        _animationHandle.CrossFade(animIdle.name);
                        break;
                    case HeroActionState.Runing:
                        _animationHandle.CrossFade(animRuning.name);
                        AudioManager.PlayAudioEffectB(acHeroRuning);
                        yield return new WaitForSeconds(acHeroRuning.length);
                        break;
                    case HeroActionState.Hurt:
                        break;
                    case HeroActionState.Dead:
                        if (_isSinglePlay_Dead)
                        {
                            _isSinglePlay_Dead = false;
                            _animationHandle.CrossFade(animDead.name);
                            AudioManager.PlayAudioEffectB(acHeroDead);
                            yield return new WaitForSeconds(animDead.length);
                        }
                        break;
                }
            }
        }

        public void CancelRunningState()
        {
            if (CurrentActionState == HeroActionState.Idle || CurrentActionState == HeroActionState.Runing)
            {
                SetCurrentActionState(HeroActionState.Idle);
            }
        }

        #region 动画事件 AnimationEvent
        public IEnumerator AnimationEvent_HeroMagicA()
        {
            StartCoroutine(LoadParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/Hero_MagicA(bruceSkill)", true,
                transform.position + transform.TransformDirection(new Vector3(0F, 0F, 1f)), transform.rotation,
                transform.parent, null, 0));
            yield break;  //(相当于 方法中的 return null)
        }

        public IEnumerator AnimationEvent_HeroMagicB()
        {
            StartCoroutine(LoadParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/Hero_MagicA(bruceSkill)", true,
                transform.position + transform.TransformDirection(new Vector3(0F, 0F, 2F)), transform.rotation,
                transform.parent, null, 0));
            yield break;

        }

        public IEnumerator AnimationEvent_HeroMagicC()
        {
            StartCoroutine(LoadParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/Hero_MagicC(groundBrokeRed)", true,
                transform.position + transform.TransformDirection(new Vector3(0F, 0F, 3F)), transform.rotation,
                transform.parent, null, 0));
            yield break;
        }

        /// <summary>
        /// 普通剑法粒子特效(主角左右劈砍)
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimationEvent_HeroNormalATK_A()
        {
            ///* 传统方式 */
            //StartCoroutine(LoadParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1,
            //    "ParticleProps/Hero_NormalATK1", true,
            //    transform.position + transform.TransformDirection(new Vector3(0F, 0F, 1F)),transform.rotation,
            //    transform, null, 1));
            //yield break;


            /*  使用对象缓冲池 */
            goHeroNormalPartcalEffec1.transform.position = transform.position + transform.TransformDirection(new Vector3(0F, 0F, 1F));
            //PoolManager.PoolsArray["_ParticalSys"].GetGameObjectByPool(goHeroNormalPartcalEffec1, goHeroNormalPartcalEffec1.transform.position, Quaternion.identity);
            PoolManager.PoolsArray["_ParticalSys"].GetGameObjectByPool(goHeroNormalPartcalEffec1, goHeroNormalPartcalEffec1.transform.position, transform.rotation);
            yield break;
        }

        /// <summary>
        /// 普通剑法粒子特效(主角中间刺入)
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimationEvent_HeroNormalATK_B()
        {
            ///* 传统方式 */
            //StartCoroutine(LoadParticalEffect(GlobalParameter.INTERVAL_TIME_0DOT1,
            //    "ParticleProps/Hero_NormalATK2", true,
            //    transform.position + transform.TransformDirection(new Vector3(0F, 0F, 1F)),transform.rotation,
            //    transform, null, 1));
            //yield break;


            /*  使用对象缓冲池 */
            goHeroNormalPartcalEffec2.transform.position = transform.position + transform.TransformDirection(new Vector3(0F, 0F, 1F));
            //PoolManager.PoolsArray["_ParticalSys"].GetGameObjectByPool(goHeroNormalPartcalEffec2, goHeroNormalPartcalEffec2.transform.position, Quaternion.identity);
            PoolManager.PoolsArray["_ParticalSys"].GetGameObjectByPool(goHeroNormalPartcalEffec2, goHeroNormalPartcalEffec2.transform.position + transform.TransformDirection(new Vector3(0f, 0f, 2f)), transform.rotation);
            yield break;

        }
        #endregion

        public void Animation_HeroHurtEffect(int reduceHP)
        {
            StartCoroutine(LoadParticalEffectInPool_MoveUpLabel(GlobalParameter.INTERVAL_TIME_0DOT1, goMoveUpLabelPrefab,
                gameObject.transform.position + transform.TransformDirection(new Vector3(0, 10f, 0)), Quaternion.identity, gameObject,
                reduceHP, GoUIPlayerInfo.transform));
        }

        public void RecoverLife()
        {
            _isSinglePlay_Dead = true;
            SetCurrentActionState(HeroActionState.Idle);
            HeroDisplayParticalEffect();
        }
    }
}