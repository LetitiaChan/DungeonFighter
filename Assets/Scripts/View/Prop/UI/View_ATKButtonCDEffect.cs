/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:视图层：UI攻击虚拟按键CD冷却
 *
 *	Description:
 *		1.
 *
 *	Date:2017.02.27
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

namespace View
{
    public class View_ATKButtonCDEffect : MonoBehaviour
    {
        public Text TxtCountDownNumber;                                        //数字大招倒计时控件
        public float FloCDTime = 2F;                                           //冷却时间
        public Image ImgCircle;                                                //外部圆圈转动特效（精灵）
        public GameObject goWhiteAndBlack;                                     //黑白精灵
        public KeyCode keyCode;                                                //键盘输入

        private float _FloTimerDelta = 0F;                                     //时间累积数值
        private bool _IsStartTimer = false;                                    //开始时间计时吗
        private Button _BtnSelf;                                               //本脚本所挂按钮
        private bool _Enable = false;                                          //是否启用

        void Start()
        {
            _BtnSelf = this.gameObject.GetComponent<Button>();
            TxtCountDownNumber.enabled = false;                                //不显示“控件倒计时"
            EnableSelf();                                                      //默认启用
        }

        void Update()
        {
            if (_Enable)
            {
                //支持键盘输入
                if (Input.GetKeyDown(keyCode))
                {
                    _IsStartTimer = true;
                    TxtCountDownNumber.enabled = true;
                }

                if (_IsStartTimer)
                {
                    goWhiteAndBlack.SetActive(true);
                    _FloTimerDelta += Time.deltaTime;                          //时间数值累加
                    //控件倒计时显示
                    TxtCountDownNumber.text = Mathf.RoundToInt(FloCDTime - _FloTimerDelta).ToString();
                    ImgCircle.fillAmount = _FloTimerDelta / FloCDTime;         //给Circle控件赋值
                    _BtnSelf.interactable = false;                             //按钮禁用
                                                                               //超过CD时限
                    if (_FloTimerDelta > FloCDTime)
                    {
                        TxtCountDownNumber.enabled = false;                    //不显示“控件倒计时"
                        _IsStartTimer = false;
                        ImgCircle.fillAmount = 1;
                        _FloTimerDelta = 0F;
                        goWhiteAndBlack.SetActive(false);                      //禁用黑白精灵
                        _BtnSelf.interactable = true;                          //按钮启用
                    }
                }
            }
        }
        /// <summary>
        /// 响应用户点击
        /// </summary>
        public void ResponseBtnClick()
        {
            _IsStartTimer = true;
            TxtCountDownNumber.enabled = true;                                 //显示“控件倒计时"
        }

        /// <summary>
        /// 启用本控件
        /// </summary>
        public void EnableSelf()
        {
            _Enable = true;
            goWhiteAndBlack.SetActive(false);
            _BtnSelf.interactable = true;
        }
        /// <summary>
        /// 禁用本控件
        /// </summary>
        public void DisableSelf()
        {
            _Enable = false;
            goWhiteAndBlack.SetActive(true);
            _BtnSelf.interactable = false;
        }
    }
}