/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:视图层：相应玩家信息处理
 *
 *	Description:
 *		1.专门响应玩家的“点击”处理。
 *
 *	Date:2017.02.26
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

using Kernal;
using Global;
using Control;

namespace View
{
    public class View_PlayerInfoResponse : MonoBehaviour
    {
        public static View_PlayerInfoResponse Instance;                        //本脚本的实例
        public GameObject goPlayerDetailInfoPanel;                             //玩家详细信息面板（即：玩家角色）
        //public GameObject goET;                                                //EasyTouch 摇杆
        //public GameObject goHeroInfo;                                          //英雄信息
        //定义攻击虚拟按键
        public GameObject GoNormalATK;                                         //普通攻击UI按键
        public GameObject GoMagicA;                                            //大招A
        public GameObject GoMagicB;
        public GameObject GoMagicC;
        public GameObject GoMagicD;
        public GameObject GoAddingHP;                                          //加血按键

        void Awake()
        {
            Instance = this;
        }

        //void Start()
        //{
        //    //DisplayET();
        //}

        /// <summary>
        /// 显示与隐藏“玩家详细信息面板”
        /// </summary>
        public void ShowOrHidePlayerDetailPanel()
        {
            goPlayerDetailInfoPanel.SetActive(!goPlayerDetailInfoPanel.activeSelf);
        }

        /// <summary>
        /// 显示玩家角色
        /// </summary>
        public void DisplayPlayerRoles()
        {
            //预处理
            if (goPlayerDetailInfoPanel != null)
            {
                BeforeOpenWindow(goPlayerDetailInfoPanel);
            }
            goPlayerDetailInfoPanel.SetActive(true);
        }

        /// <summary>
        /// 隐藏玩家角色
        /// </summary>
        public void HidePlayerRoles()
        {
            //预处理
            if (goPlayerDetailInfoPanel != null)
            {
                BeforeCloseWindow();
            }
            goPlayerDetailInfoPanel.SetActive(false);
        }

        /// <summary>
        /// 显示ET
        /// </summary>
        public void DisplayET()
        {
            //goET.SetActive(true);
        }

        /// <summary>
        /// 隐藏ET
        /// </summary>
        public void HideET()
        {
            //goET.SetActive(false);
        }

        /// <summary>
        /// 显示所有UI按键
        /// </summary>
        public void DisplayAllUIKey()
        {
            GoNormalATK.SetActive(true);
            GoMagicA.SetActive(true);
            GoMagicB.SetActive(true);
            GoMagicC.SetActive(true);
            GoMagicD.SetActive(true);
            GoAddingHP.SetActive(true);
        }

        /// <summary>
        /// 隐藏所有UI按键
        /// </summary>
        public void HideAllUIKey()
        {
            GoNormalATK.SetActive(false);
            GoMagicA.SetActive(false);
            GoMagicB.SetActive(false);
            GoMagicC.SetActive(false);
            GoMagicD.SetActive(false);
            GoAddingHP.SetActive(false);
        }

        /// <summary>
        /// 显示主要攻击按键
        /// </summary>
        public void DisplayMainATK()
        {
            GoNormalATK.SetActive(true);
            GoMagicA.SetActive(false);
            GoMagicB.SetActive(false);
            GoMagicC.SetActive(false);
            GoMagicD.SetActive(false);
            GoAddingHP.SetActive(false);
        }

        /// <summary>
        /// 显示英雄UI信息
        /// </summary>
        public void DisplayHeroUIInfo()
        {
            //goHeroInfo.SetActive(true);
        }

        /// <summary>
        /// 隐藏英雄UI信息
        /// </summary>
        public void HideHeroUIInfo()
        {
            //goHeroInfo.SetActive(false);
        }

        /// <summary>
        /// 退出游戏系统
        /// </summary>
        public void ExitGame()
        {
            Application.Quit();
        }

        /// <summary>
        /// 打开窗体之前的预处理
        /// </summary>
        /// <param name="goNeedDisplayPanel"></param>
        private void BeforeOpenWindow(GameObject goNeedDisplayPanel)
        {
            //禁用ET
            HideET();
            //窗体的模态化
            this.gameObject.GetComponent<UIMaskMgr>().SetMaskWindow(goNeedDisplayPanel);
        }

        /// <summary>
        /// 关闭窗体之前的预处理
        /// </summary>
        private void BeforeCloseWindow()
        {
            //开启ET
            DisplayET();
            //取消窗体的模态化
            this.gameObject.GetComponent<UIMaskMgr>().CancleMaskWindow();
        }


        //#if UNITY_ANDROID || UNITY_IPHONE
        #region 相应玩家虚拟按键点击
        //public void ResponseNormalATK()
        //{
        //    //print("[视图层]相应攻击");
        //    Ctrl_HeroAttackInputByET.Instance.ResponseATKByNormal();
        //}

        public void ResponseATKByMagicA()
        {
            Ctrl_HeroAttackInputByET.Instance.ResponseATKByMagicA();
        }

        public void ResponseATKByMagicB()
        {
            Ctrl_HeroAttackInputByET.Instance.ResponseATKByMagicB();
        }

        public void ResponseATKByMagicC()
        {
            Ctrl_HeroAttackInputByET.Instance.ResponseATKByMagicC();
        }

        public void ResponseATKByMagicD()
        {
            Ctrl_HeroAttackInputByET.Instance.ResponseATKByMagicD();
        }

        #endregion
        //#endif

    }
}