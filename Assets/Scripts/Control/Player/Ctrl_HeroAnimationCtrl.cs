/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层： 主角动画控制
 *
 *	Description:
 *		1.
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
    public class Ctrl_HeroAnimationCtrl : BaseControl
    {
        public static Ctrl_HeroAnimationCtrl Instance;

        public AnimationClip Ani_Idle;                                         //休闲
        public AnimationClip Ani_Runing;                                       //跑动
        public AnimationClip Ani_NormalAttack1;                                //普通攻击1
        public AnimationClip Ani_NormalAttack2;                                //普通攻击2
        public AnimationClip Ani_NormalAttack3;                                //普通攻击3
        public AnimationClip Ani_MagicTrickA;                                  //大招A
        public AnimationClip Ani_MagicTrickB;                                  //大招B
        public AnimationClip Ani_MagicTrickC;                                  //大招C
        public AnimationClip Ani_MagicTrickD;                                  //大招D

        public AudioClip AcHeroRuning;                                         //定义主角跑动音效剪辑

        //对象缓冲池：主角剑法粒子特效
        public GameObject goHeroNormalPartcalEffec1;                           //左右剑法粒子特效
        public GameObject goHeroNormalPartcalEffec2;                           //剑法的中间劈砍粒子特效


        private Animation _AnimationHandle;                                    //动画句柄
        private HeroActionState _CurrentActionState = HeroActionState.None;    //主角的动画状态
        private bool _IsSinglePlay = true;                                     //单次播放
        private NormalATKComboState _CurATKCombo = NormalATKComboState.NormalATK1;//动画连招

        /// <summary>
        /// 属性：当前主角的动作状态
        /// </summary>
        public HeroActionState CurrentActionState
        {
            get
            {
                return _CurrentActionState;
            }
        }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            _AnimationHandle = this.GetComponent<Animation>();
            //默认动作状态
            _CurrentActionState = HeroActionState.Idle;
            //启动协程，控制主角动画状态
            StartCoroutine("ControlHeroAnimationState");
            //加快普通连招的播放速度
            _AnimationHandle[Ani_NormalAttack1.name].speed = 2.5F;
            _AnimationHandle[Ani_NormalAttack2.name].speed = 2.5F;
            _AnimationHandle[Ani_NormalAttack3.name].speed = 2F;

            //定义主角出现特效
            HeroDisplayParticalEffect();
        }

        /// <summary>
        /// 设置当前（动画）状态
        /// </summary>
        /// <param name="currentActionState"></param>
        public void SetCurrentActionState(HeroActionState currentActionState)
        {
            _CurrentActionState = currentActionState;
        }

        /// <summary>
        /// 主角的动画控制
        /// </summary>
        /// <returns></returns>
        IEnumerator ControlHeroAnimationState()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1F);
                switch (CurrentActionState)
                {
                    case HeroActionState.NormalAttack:
                        /* 攻击连招处理(自动状态转换) */
                        switch (_CurATKCombo)
                        {
                            case NormalATKComboState.NormalATK1:
                                _CurATKCombo = NormalATKComboState.NormalATK2;
                                _AnimationHandle.CrossFade(Ani_NormalAttack1.name);
                                AudioManager.PlayAudioEffectA("BeiJi_DaoJian_3");
                                yield return new WaitForSeconds(Ani_NormalAttack1.length / 2.5F);

                                _CurrentActionState = HeroActionState.Idle;
                                break;
                            case NormalATKComboState.NormalATK2:
                                _CurATKCombo = NormalATKComboState.NormalATK3;
                                _AnimationHandle.CrossFade(Ani_NormalAttack2.name);
                                AudioManager.PlayAudioEffectA("BeiJi_DaoJian_2");
                                yield return new WaitForSeconds(Ani_NormalAttack2.length / 2.5F);

                                _CurrentActionState = HeroActionState.Idle;
                                break;
                            case NormalATKComboState.NormalATK3:
                                _CurATKCombo = NormalATKComboState.NormalATK1;
                                _AnimationHandle.CrossFade(Ani_NormalAttack3.name);
                                AudioManager.PlayAudioEffectA("BeiJi_DaoJian_1");
                                yield return new WaitForSeconds(Ani_NormalAttack3.length / 2F);

                                _CurrentActionState = HeroActionState.Idle;
                                break;
                            default:
                                break;
                        }
                        break;
                    case HeroActionState.MagicTrickA:
                        _AnimationHandle.CrossFade(Ani_MagicTrickA.name);
                        AudioManager.PlayAudioEffectA("SwordHero_MagicA");
                        yield return new WaitForSeconds(Ani_MagicTrickA.length);

                        _CurrentActionState = HeroActionState.Idle;
                        break;
                    case HeroActionState.MagicTrickB:
                        _AnimationHandle.CrossFade(Ani_MagicTrickB.name);
                        AudioManager.PlayAudioEffectA("SwordHero_MagicB");
                        yield return new WaitForSeconds(Ani_MagicTrickB.length);

                        _CurrentActionState = HeroActionState.Idle;
                        break;
                    case HeroActionState.MagicTrickC:
                        _AnimationHandle.CrossFade(Ani_MagicTrickC.name);
                        AudioManager.PlayAudioEffectA("SwordHero_MagicC");
                        yield return new WaitForSeconds(Ani_MagicTrickC.length);

                        _CurrentActionState = HeroActionState.Idle;
                        break;
                    case HeroActionState.MagicTrickD:
                        _AnimationHandle.CrossFade(Ani_MagicTrickD.name);
                        AudioManager.PlayAudioEffectA("SwordHero_MagicD");
                        yield return new WaitForSeconds(Ani_MagicTrickD.length);

                        _CurrentActionState = HeroActionState.Idle;
                        break;
                    case HeroActionState.None:
                        break;
                    case HeroActionState.Idle:
                        _AnimationHandle.CrossFade(Ani_Idle.name);
                        break;
                    case HeroActionState.Runing:
                        _AnimationHandle.CrossFade(Ani_Runing.name);
                        //处理主角跑动音效
                        AudioManager.PlayAudioEffectB(AcHeroRuning);
                        yield return new WaitForSeconds(AcHeroRuning.length);
                        break;
                    default:
                        break;
                }
            }
        }


        /// <summary>
        /// 动画事件_主角大招A
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimationEvent_HeroMagicA()
        {

            StartCoroutine(LoadParticalEffectPublicMethod(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/Hero_MagicA(bruceSkill)", true, transform.position, transform, null, 0));
            yield break;  //(相当于 方法中的 return null)
        }

        /// <summary>
        /// 动画事件_主角大招B
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimationEvent_HeroMagicB()
        {
            StartCoroutine(LoadParticalEffectPublicMethod(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/Hero_MagicB(groundBrokeRed)", true, transform.position + transform.TransformDirection(new Vector3(0F, 0F, 5F)),//特效位置在主角前方5m
                transform, null, 0));
            yield break;  //(相当于 方法中的 return null)

        }

        /// <summary>
        /// 普通剑法粒子特效(主角左右劈砍)
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimationEvent_HeroNormalATK_A()
        {
            /* 传统方式 */
            StartCoroutine(LoadParticalEffectPublicMethod(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/Hero_NormalATK1", true, transform.position + transform.TransformDirection(new Vector3(0F, 0F, 1F)),
                transform, null, 1));
            yield break;

            //定义粒子特效的位置(在主角前方5米的位置)
            goHeroNormalPartcalEffec1.transform.position = transform.position + transform.TransformDirection(new Vector3(0F, 0F, 1F));
            /*  使用对象缓冲池技术 */
            //在缓冲池中，得到一个指定的预设“激活体”。
            PoolManager.PoolsArray["_ParticalSys"].GetGameObjectByPool(goHeroNormalPartcalEffec1, goHeroNormalPartcalEffec1.transform.position, Quaternion.identity);
            yield break;
        }

        /// <summary>
        /// 普通剑法粒子特效(主角中间刺入)
        /// </summary>
        /// <returns></returns>
        public IEnumerator AnimationEvent_HeroNormalATK_B()
        {
            StartCoroutine(LoadParticalEffectPublicMethod(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/Hero_NormalATK2", true, transform.position + transform.TransformDirection(new Vector3(0F, 0F, 1F)),
                transform, null, 1));
            yield break;

            ////定义粒子特效的位置(在主角前方5米的位置)
            //goHeroNormalPartcalEffec2.transform.position = transform.position + transform.TransformDirection(new Vector3(0F, 0F, 1F));
            ///*  使用对象缓冲池技术 */
            ////在缓冲池中，得到一个指定的预设“激活体”。
            //PoolManager.PoolsArray["_ParticalSys"].GetGameObjectByPool(goHeroNormalPartcalEffec2, goHeroNormalPartcalEffec2.transform.position, Quaternion.identity);
            //yield break;

        }

        /// <summary>
        /// 主角登场特效
        /// </summary>
        private void HeroDisplayParticalEffect()
        {
            StartCoroutine(LoadParticalEffectPublicMethod(GlobalParameter.INTERVAL_TIME_0DOT1,
                "ParticleProps/Hero_Display", true, transform.position,
                transform, null, 0));
        }
    }
}