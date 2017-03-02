/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层 - 选择登录
 *
 *	Description:
 *		1.
 *
 *	Date:2017.02.21
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
    public class Ctrl_Scene_Login : BaseControl
    {
        public static Ctrl_Scene_Login Instance;
        public AudioClip AucBackground;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            //播放音频
            AudioManager.SetAudioBackgroundVolumns(0.5F);
            AudioManager.SetAudioEffectVolumns(1F);
            AudioManager.PlayBackground(AucBackground);
        }

        /// <summary>
        /// 转入下一场景
        /// </summary>
        public void EnterNextScene()
        {
            base.EnterNextScene(EnumScenes.ScenesLevelOne);
        }

        /// <summary>
        /// 播放英雄音效
        /// </summary>
        public void PlayAudioEffectHero()
        {
            string strHeroEffect = GlobalParaMgr.HeroType == EnumHeroType.MagicHero ? "2_FireBallEffect_MagicHero" : "1_LightRoar_SwordHero";
            AudioManager.PlayAudioEffectA(strHeroEffect);
        }
    }
}