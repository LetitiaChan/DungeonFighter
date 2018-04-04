using UnityEngine;
using UnityEngine.UI;
using Kernal;

namespace View
{
    /// <summary>
    /// UI攻击虚拟按键CD冷却
    /// </summary>
    public class View_ATKButtonCDEffect : MonoBehaviour
    {
        public Text txtCountDownNumber;
        public float CDTimer = 2f;
        public Image imgCircle;
        public GameObject goWhiteAndBlack;
        public KeyCode keyCode;

        private float _timerDelta = 0f;
        private bool _isStartTimer = false;
        private Button _btnSelf;
        private bool _enable = false;


        void Start()
        {
            _btnSelf = GetComponent<Button>();
            txtCountDownNumber.enabled = false;
            EnableSelf();
        }

        void Update()
        {
            if (_enable)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    _isStartTimer = true;
                    txtCountDownNumber.enabled = true;
                }

                if (_isStartTimer)
                {
                    goWhiteAndBlack.SetActive(true);
                    _timerDelta += Time.deltaTime;
                    txtCountDownNumber.text = Mathf.RoundToInt(CDTimer - _timerDelta).ToString();
                    imgCircle.fillAmount = _timerDelta / CDTimer;
                    _btnSelf.interactable = false;

                    if (_timerDelta > CDTimer)
                    {
                        txtCountDownNumber.enabled = false;
                        _isStartTimer = false;
                        imgCircle.fillAmount = 1;
                        _timerDelta = 0;
                        goWhiteAndBlack.SetActive(false);
                        _btnSelf.interactable = true;
                    }
                }
            }
        }

        public void ResponseBtnClick()
        {
            _isStartTimer = true;
            txtCountDownNumber.enabled = true;
        }


        public void EnableSelf()
        {
            _enable = true;
            goWhiteAndBlack.SetActive(false);
            //_btnSelf.interactable = true;
        }
        public void DisableSelf()
        {
            _enable = false;
            goWhiteAndBlack.SetActive(true);
            //_btnSelf.interactable = false;
        }
    }
}