/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:公共层: 通用对话UI(界面)管理器
 *
 *	Description:
 *		1.
 *
 *	Date:
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

namespace Global
{
    /// <summary>
    /// 对话类型
    /// </summary>
    public enum DialogType
    {
        None,                                                                  //无
        DoubleDialogs,                                                         //双人会话
        Singledialogs                                                          //单人会话
    }

    public class DialogUIMgr : MonoBehaviour
    {
        //public static DialogUIMgr _Instance;                                  //本类对象
        ////UI对象
        //public GameObject goHero;                                              //英雄
        //public GameObject goNPC_Left;                                          //左边的NPC
        //public GameObject goNPC_Right;                                         //右边的NPC
        //public GameObject goSingleDialogArea;                                  //单人对话信息区域
        //public GameObject goDoubleDialogArea;                                  //双人对话信息区域

        ////对话显示控件
        //public Text TxtPersonName;                                             //人名
        //public Text TxtDoubleDialogContent;                                    //双人对话内容
        //public Text TxtSingleDialogConten;                                     //单人对话内容

        ////Sprint资源数组（规定，0下标表示彩色精灵，1下标表示黑白精灵）
        //public Sprite[] SprHero;                                               //英雄精灵资源
        //public Sprite[] SprNPC_Left;
        //public Sprite[] SprNPC_Right;


        //void Awake()
        //{
        //    _Instance = this;
        //}

        ///// <summary>
        ///// 显示下一条对话
        ///// </summary>
        ///// <param name="diaType">对话类型（单人/双人）</param>
        ///// <param name="dialogSectionNum">对话段落编号</param>
        ///// <returns>
        ///// true:  对话结束
        ///// false; 对话继续
        ///// </returns>
        //public bool DisplayNextDialog(DialogType diaType, int dialogSectionNum)
        //{
        //    bool isDialogEnd = false;                                            //对话是否结束
        //    DialogSide diaSide = DialogSide.None;                                //正在说话的一方
        //    string strPersonName;                                              //说话人
        //    string strDialogContent;                                           //对话内容


        //    //切换（选择）对话类型
        //    ChangeDialogType(diaType);

        //    //得到会话信息（数据）
        //    bool boolFlag = DialogDataMgr.GetInstance().GetNextDialogInfoRecoder(dialogSectionNum, out diaSide, out strPersonName, out strDialogContent);
        //    if (boolFlag)
        //    {
        //        //显示对话信息
        //        DisplayDialogInfo(diaType, diaSide, strPersonName, strDialogContent);
        //    }
        //    else
        //    {
        //        //对话结束（没有对话信息了）
        //        isDialogEnd = true;
        //    }

        //    return isDialogEnd;
        //}

        ///// <summary>
        ///// 切换（选择）对话类型
        ///// </summary>
        ///// <param name="diaType">对话类型（单人/双人）</param>
        //private void ChangeDialogType(DialogType diaType)
        //{
        //    switch (diaType)
        //    {
        //        case DialogType.None:
        //            goHero.SetActive(false);                                   //英雄
        //            goNPC_Left.SetActive(false); ;                            //左边的NPC
        //            goNPC_Right.SetActive(false); ;                           //右边的NPC
        //            goSingleDialogArea.SetActive(false); ;                    //单人对话信息区域
        //            goDoubleDialogArea.SetActive(false); ;                    //双人对话信息区域
        //            break;
        //        case DialogType.DoubleDialogs:
        //            goHero.SetActive(true);                                    //英雄
        //            goNPC_Left.SetActive(false); ;                            //左边的NPC
        //            goNPC_Right.SetActive(true); ;                            //右边的NPC
        //            goSingleDialogArea.SetActive(false); ;                    //单人对话信息区域
        //            goDoubleDialogArea.SetActive(true); ;                     //双人对话信息区域
        //            break;
        //        case DialogType.Singledialogs:
        //            goHero.SetActive(false);                                   //英雄
        //            goNPC_Left.SetActive(true); ;                             //左边的NPC
        //            goNPC_Right.SetActive(false); ;                           //右边的NPC
        //            goSingleDialogArea.SetActive(true); ;                     //单人对话信息区域
        //            goDoubleDialogArea.SetActive(false); ;                    //双人对话信息区域
        //            break;
        //        default:
        //            goHero.SetActive(false);                                   //英雄
        //            goNPC_Left.SetActive(false); ;                            //左边的NPC
        //            goNPC_Right.SetActive(false); ;                           //右边的NPC
        //            goSingleDialogArea.SetActive(false); ;                    //单人对话信息区域
        //            goDoubleDialogArea.SetActive(false); ;                    //双人对话信息区域
        //            break;
        //    }
        //}

        ///// <summary>
        ///// 显示对话信息
        ///// </summary>
        ///// <param name="diaType">对话类型（单人/双人）</param>
        ///// <param name="diaSide">对话双方</param>
        ///// <param name="personName">对话人名</param>
        ///// <param name="dialogContent">对方内容</param>
        //private void DisplayDialogInfo(DialogType diaType, DialogSide diaSide, string personName, string dialogContent)
        //{
        //    switch (diaType)
        //    {
        //        case DialogType.None:
        //            break;
        //        case DialogType.DoubleDialogs:
        //            //显示人名，对话信息
        //            if (!string.IsNullOrEmpty(personName) && !string.IsNullOrEmpty(dialogContent))
        //            {
        //                //Modify By 2016.05
        //                if (diaSide == DialogSide.HeroSide)
        //                {
        //                    TxtPersonName.text = GlobalParaMgr.PlayerName;
        //                }
        //                else
        //                {
        //                    TxtPersonName.text = personName;
        //                }
        //                TxtDoubleDialogContent.text = dialogContent;
        //            }
        //            //确定显示精灵
        //            switch (diaSide)
        //            {
        //                case DialogSide.None:
        //                    break;
        //                case DialogSide.HeroSide:
        //                    goHero.GetComponent<Image>().overrideSprite = SprHero[0];//0表示彩色
        //                    goNPC_Right.GetComponent<Image>().overrideSprite = SprNPC_Right[1];//1显示黑白
        //                    break;
        //                case DialogSide.NPCSide:
        //                    goHero.GetComponent<Image>().overrideSprite = SprHero[1];
        //                    goNPC_Right.GetComponent<Image>().overrideSprite = SprNPC_Right[0];
        //                    break;
        //                default:
        //                    break;
        //            }
        //            break;
        //        case DialogType.Singledialogs:
        //            TxtSingleDialogConten.text = dialogContent;
        //            break;
        //        default:
        //            break;
        //    }
        //}

    }
}


