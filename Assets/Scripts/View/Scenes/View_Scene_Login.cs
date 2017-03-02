/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:表示层 - 选择登录
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
using Control;

namespace View
{
	public class View_Scene_Login : MonoBehaviour {
        public GameObject goHeroSword;
        public GameObject goHeroMagic;
        public GameObject goUISword;
        public GameObject goUIMagic;
        public InputField InputUserName;

        void Start () {
            //获取英雄类型（系统默认）
            GlobalParaMgr.HeroType = EnumHeroType.SwordHero;
        }

        /// <summary>
        /// 切换到少年剑侠
        /// </summary>
        public void ChangeToSword()
        {
            //切换模型
            goHeroSword.SetActive(true);
            goHeroMagic.SetActive(false);

            //切换UI
            goUISword.SetActive(true);
            goUIMagic.SetActive(false);

            //英雄类型
            GlobalParaMgr.HeroType = EnumHeroType.SwordHero;

            //播放音效
            Ctrl_Scene_Login.Instance.PlayAudioEffectHero();
        }
        /// <summary>
        /// 切换到魔法师
        /// </summary>
        public void ChangeToMagic()
        {
            //切换模型
            goHeroSword.SetActive(false);
            goHeroMagic.SetActive(true);

            //切换UI
            goUISword.SetActive(false);
            goUIMagic.SetActive(true);

            //英雄类型
            GlobalParaMgr.HeroType = EnumHeroType.MagicHero;

            //播放音效
            Ctrl_Scene_Login.Instance.PlayAudioEffectHero();
        }

        public void SubmitInfo()
        {
            //获得用户名
            GlobalParaMgr.PlayerName = InputUserName.name;
            //跳转场景
            Ctrl_Scene_Login.Instance.EnterNextScene();
        }
	}
}