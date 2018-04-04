using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Control;

namespace View
{
    public class View_DrugButtonCDEffect : MonoBehaviour
    {
        public float CDTimer = 2f;
        public Image imgCircle;

        private float _timerDelta = 0f;
        private bool _isStartTimer = false;
        private Button _btnSelf;
        private bool _enable = false;


        void Start()
        {
            _btnSelf = GetComponent<Button>();
            EnableSelf();
        }

        void Update()
        {
            if (_enable)
            {
                if (_isStartTimer)
                {
                    _timerDelta += Time.deltaTime;
                    imgCircle.fillAmount = 1 - _timerDelta / CDTimer;
                    _btnSelf.interactable = false;

                    if (_timerDelta > CDTimer)
                    {
                        _isStartTimer = false;
                        imgCircle.fillAmount = 0;
                        _timerDelta = 0;
                        _btnSelf.interactable = true;
                    }
                }
            }
        }

        public void ResponseBtnClick()
        {
            if (Ctrl_PlayerUIResponse.Instance.CanDrugRed())
                _isStartTimer = true;
        }


        public void EnableSelf()
        {
            _enable = true;
        }
        public void DisableSelf()
        {
            _enable = false;
        }
    }
}