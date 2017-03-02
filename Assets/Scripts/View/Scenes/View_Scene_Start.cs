/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:视图层 - 开始游戏
 *
 *	Description:
 *		1.
 *
 *	Date:2017.02.20
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
using Control;

namespace View
{
    public class View_Scene_Start : MonoBehaviour
    {

        public void ClickNewGame()
        {
            print(GetType() + "/ClickNewGame()");
            Ctrl_Scene_Start.Instance.NewGame();
        }

        public void ClickContinueGame()
        {
            print(GetType() + "/ClickContinueGame()");
            Ctrl_Scene_Start.Instance.ContinueGame();
        }
    }
}