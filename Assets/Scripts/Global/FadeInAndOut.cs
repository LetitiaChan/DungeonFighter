/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:公共层 - 场景的淡入淡出
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

namespace Global
{
    public class FadeInAndOut : MonoBehaviour
    {
        public static FadeInAndOut Instance;
        public float FloFadeInSpeed = 1F;                                      //淡入速度
        public float FloFadeOutSpeed = 0.8F;                                   //淡出速度
        public GameObject goRawImage;                                          //RawImage对象

        private RawImage _RawImage;                                            //RawImage组件
        private bool _IsSceneToClear = true;                                   //屏幕逐渐清晰
        private bool _IsSceneToBlack = false;                                  //屏幕逐渐暗淡

        void Awake()
        {
            Instance = this;

            if (goRawImage)
            {
                _RawImage = goRawImage.GetComponent<RawImage>();
            }
        }

        void Update()
        {
            if (_IsSceneToClear)
            {
                SceneToClear();
            }
            else if (_IsSceneToBlack)
            {
                SceneToBlack();
            }
        }

        /// <summary>
        /// 设置场景的淡入
        /// </summary>
        public void SetScenesToClear()
        {
            _IsSceneToClear = true;
            _IsSceneToBlack = false;
        }

        /// <summary>
        /// 设置场景的淡出
        /// </summary>
        public void SetScenesToBlack()
        {
            _IsSceneToClear = false;
            _IsSceneToBlack = true;
        }

        /// <summary>
        /// 设置场景的淡入
        /// </summary>
        public void SetSceneToClear()
        {
            _IsSceneToClear = true;
            _IsSceneToBlack = false;
        }
        /// <summary>
        /// 设置场景的淡出
        /// </summary>
        public void SetSceneToBlack()
        {
            _IsSceneToClear = false;
            _IsSceneToBlack = true;
        }

        private void SceneToClear()
        {
            FadeToClear();
            if (_RawImage.color.a < 0.15)
            {
                _RawImage.color = Color.clear;
                _RawImage.enabled = false;
                _IsSceneToClear = false;
            }
        }

        private void SceneToBlack()
        {
            _RawImage.enabled = true;
            FadeToBlack();
            if (_RawImage.color.a >= 0.95)
            {
                _RawImage.color = Color.black;
                _IsSceneToBlack = false;
            }
        }

        /// <summary>
        /// 淡入（屏幕逐渐清晰）
        /// </summary>
        private void FadeToClear()
        {
            //运用Color的差值运算
            _RawImage.color = Color.Lerp(_RawImage.color, Color.clear, FloFadeInSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 淡出（屏幕逐渐暗淡）
        /// </summary>
        private void FadeToBlack()
        {
            //运用Color的差值运算
            _RawImage.color = Color.Lerp(_RawImage.color, Color.black, FloFadeOutSpeed * Time.deltaTime);
        }
    }
}