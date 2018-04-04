using UnityEngine;

namespace Control
{
    /// <summary>
    /// 选角创角时的英雄展示
    /// </summary>
    public class Ctrl_DisplayHero : BaseControl
    {
        public AnimationClip animIdle;
        public AnimationClip animRun;
        public AnimationClip animAttack;

        private Animation _aniCurrent;
        private float _intervalTimes = 3f;
        private int _randomPlayNum;

        void Start()
        {
            _aniCurrent = GetComponent<Animation>();
            _intervalTimes = 1f;
        }

        /// <summary>
        /// 思路：间隔3秒钟，随机播放一个人物动作
        /// </summary>
        void Update()
        {
            _intervalTimes -= Time.deltaTime;
            if (_intervalTimes <= 0)
            {
                _intervalTimes = 3f;
                _randomPlayNum = Random.Range(1, 4);
                DisplayHeroPlaying(_randomPlayNum);
            }
        }


        internal void DisplayHeroPlaying(int playNum)
        {
            switch (playNum)
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
        internal void DisplayIdle()
        {
            if (_aniCurrent)
            {
                _aniCurrent.CrossFade(animIdle.name);
            }
        }
        internal void DisplayRun()
        {
            if (_aniCurrent)
            {
                _aniCurrent.CrossFade(animRun.name);
            }
        }
        internal void DisplayAttack()
        {
            if (_aniCurrent)
            {
                _aniCurrent.CrossFade(animAttack.name);
            }
        }
    }
}