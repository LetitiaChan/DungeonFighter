/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:公共层: UI遮罩管理器 
 *
 *	Description:
 *		1.实现弹出“模态窗体”。
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

using Kernal;

namespace Global
{
    public class UIMaskMgr : MonoBehaviour
    {
        public GameObject GoTopPlane;                                          //顶层面板
        public GameObject goMaskPlane;                                         //遮罩面板
        private Camera _UICamear;                                              //UI摄像机
        private float _OriginalUICameraDepth;                                  //原始UI摄像机的层深



        void Start()
        {
            //得到UI摄像机的原始“层深”
            _UICamear = this.gameObject.transform.parent.FindChild("UICamera").GetComponent<Camera>();
            if (_UICamear != null)
            {
                _OriginalUICameraDepth = _UICamear.depth;
            }
            else
            {
                Debug.LogError(GetType() + "/Start()/_UICamera is Null ,please Check!");
            }
        }

        /// <summary>
        /// 设置遮罩状态
        /// </summary>
        /// <param name="goDisplayPlane">需要显示的窗体</param>
        public void SetMaskWindow(GameObject goDisplayPlane)
        {
            //顶层窗体下移。
            GoTopPlane.transform.SetAsLastSibling();
            //启用遮罩窗体
            goMaskPlane.SetActive(true);
            //遮罩窗体下移
            goMaskPlane.transform.SetAsLastSibling();
            //显示窗体下移
            goDisplayPlane.transform.SetAsLastSibling();
            //增加当前UI摄像机的“层深”
            if (_UICamear != null)
            {
                _UICamear.depth = _UICamear.depth + 20;
            }
        }

        /// <summary>
        /// 取消遮罩窗体
        /// </summary>
        public void CancleMaskWindow()
        {
            //顶层窗体上移
            GoTopPlane.transform.SetAsFirstSibling();
            //禁用遮罩窗体
            goMaskPlane.SetActive(false);
            //回复UI摄像机的原来的“层深”
            _UICamear.depth = _OriginalUICameraDepth;
        }
    }//Class_end
}


