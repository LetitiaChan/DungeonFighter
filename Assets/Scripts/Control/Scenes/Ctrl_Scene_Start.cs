/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层 - 开始游戏
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
using Global;
using Kernal;

namespace Control
{
    //public class Ctrl_Scene_Start : MonoBehaviour
    public class Ctrl_Scene_Start : BaseControl
    {
        public static Ctrl_Scene_Start Instance;
        public AudioClip AucBackground;                     //背景音乐音频剪辑

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            AudioManager.SetAudioBackgroundVolumns(0.5F);
            AudioManager.SetAudioBackgroundVolumns(1F);
            AudioManager.PlayBackground(AucBackground);  //针对大音频的方式
        }

        internal void NewGame()
        {
            //print(GetType() + "/NewGame()");
            StartCoroutine("EnterNextScenes");
        }

        internal void ContinueGame()
        {
            //print(GetType() + "/ContinueGame()");
            FadeInAndOut.Instance.SetSceneToBlack();
        }

        IEnumerator EnterNextScenes()
        {
            FadeInAndOut.Instance.SetSceneToBlack();
            yield return new WaitForSeconds(3F);

            base.EnterNextScene(EnumScenes.ScenesLoading);
        }
    }
}