/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层:  英雄的展示
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

using Global;
using Kernal;

namespace Control
{
    public class Ctrl_DisplayHero : BaseControl
    {
        public AnimationClip AniIdle;                                          //动画剪辑 休闲
        public AnimationClip AniRun;
        public AnimationClip AniAttack;
        private Animation _AniCurrent;                                         //当前动画
        private float _FloIntervalTimes = 3F;                                  //播放间隔时间
        private int _IntRandomPlayNum;                                         //随机动作编号

        void Start()
        {
            _AniCurrent = GetComponent<Animation>();
            _FloIntervalTimes = 1F;//初次时间间隔
        }

        /// <summary>
        /// 算法： 间隔3秒钟，随机播放一个人物动作
        /// </summary>
        void Update()
        {
            _FloIntervalTimes -= Time.deltaTime;
            if (_FloIntervalTimes <= 0)
            {
                _FloIntervalTimes = 3F;
                _IntRandomPlayNum = Random.Range(1, 4);
                DisplayHeroPlaying(_IntRandomPlayNum);
            }
        }
        /// <summary>
        /// 展示动作
        /// </summary>
        /// <param name="intPlayNum">动作编号</param>
        internal void DisplayHeroPlaying(int intPlayNum)
        {
            switch (intPlayNum)
            {
                case 1:
                    DisplayIdle();
                    break;
                case 2:
                    DisplayRun();
                    break;
                case 3:
                    DisplayAttack();
                    break;
                default:
                    DisplayIdle();
                    break;
            }
        }
        /// <summary>
        /// 展示休闲动作
        /// </summary>
        internal void DisplayIdle()
        {
            if (_AniCurrent)
            {
                _AniCurrent.CrossFade(AniIdle.name);
            }
        }
        /// <summary>
        /// 展示跑动动作
        /// </summary>
        internal void DisplayRun()
        {
            if (_AniCurrent)
            {
                _AniCurrent.CrossFade(AniRun.name);
            }
        }
        /// <summary>
        /// 展示攻击动作
        /// </summary>
        internal void DisplayAttack()
        {
            if (_AniCurrent)
            {
                _AniCurrent.CrossFade(AniAttack.name);
            }
        }
    }
}