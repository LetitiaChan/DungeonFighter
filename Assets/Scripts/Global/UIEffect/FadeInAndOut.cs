using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    //淡入淡出
    public class FadeInAndOut : MonoBehaviour
    {
        public static FadeInAndOut Instance;

        #region Unity Inspector Fields
        public float colorChangeSpeed = 1f;
        public GameObject goRawImage;
        #endregion

        private RawImage _rawImage;
        private bool _isChangeToClear = true;
        private bool _isChangeToBlack = false;


        void Awake()
        {
            Instance = this;

            if (goRawImage)
            {
                _rawImage = goRawImage.GetComponent<RawImage>();
            }
        }
        void Update()
        {
            if (_isChangeToClear)
            {
                FadeToClear();
            }
            else if (_isChangeToBlack)
            {
                FadeToBlack();
            }
        }

        public void SetSceneToClear()
        {
            _isChangeToClear = true;
            _isChangeToBlack = false;
        }
        public void SetSceneToBlack()
        {
            _isChangeToClear = false;
            _isChangeToBlack = true;
        }


        private void FadeToClear()
        {
            _rawImage.color = Color.Lerp(_rawImage.color, Color.clear, colorChangeSpeed * Time.deltaTime);

            if (_rawImage.color.a <= 0.05)
            {
                _rawImage.color = Color.clear;
                _rawImage.enabled = false;
                _isChangeToClear = false;
            }
        }

        private void FadeToBlack()
        {
            _rawImage.enabled = true;

            _rawImage.color = Color.Lerp(_rawImage.color, Color.black, colorChangeSpeed * Time.deltaTime);

            if (_rawImage.color.a >= 0.95)
            {
                _rawImage.color = Color.black;
                _isChangeToBlack = false;
            }
        }

    }
}
