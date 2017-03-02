/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:视图层 - 过渡场景
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

namespace View
{
    public class View_Scene_Loading : MonoBehaviour
    {
        public Slider SliLoadingProgress;
        private AsyncOperation _AsyOper;
        private float _FloProgressValue;

        void Start()
        {
            StartCoroutine("LoadingSceneProgress");
        }

        void Update()
        {
            if (_FloProgressValue <= 1)
                _FloProgressValue += 0.01F;
            SliLoadingProgress.value = _FloProgressValue;
        }

        IEnumerator LoadingSceneProgress()
        {
            string str = ConvertEnumToStr.GetInstance().GetStrByEnumScenes(EnumScenes.ScenesLogin);
            _AsyOper = Application.LoadLevelAsync(str);
            _FloProgressValue = _AsyOper.progress;
            yield return _AsyOper;
        }
    }
}