/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层： 主角移动，通过键盘方式
 *
 *	Description:
 *		1.
 *
 *	Date:2017.02.22
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
    public class Ctrl_HeroMovingCtrlByKey : BaseControl
    {
        //#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        public float FloHeroMovingSpeed = 5F;                                  //英雄移动的速度
        private CharacterController _cc;                                       //角色控制器
        private float _FloGravity = 1F;                                        //角色控制器模拟重力

        void Start()
        {
            _cc = GetComponent<CharacterController>();
        }

        void Update()
        {
            //if(Time.frameCount % 3 == 0){ } 提高脚本效率,每3帧执行一次
            ControlMoving();
        }

        /// <summary>
        /// 控制主角移动
        /// </summary>
        void ControlMoving()
        {
            //点击键盘按键，获取水平与垂直偏移量
            float floMovingXPos = Input.GetAxis("Horizontal");//从-1到1偏移量
            float floMovingYPos = Input.GetAxis("Vertical");

            if (floMovingXPos != 0 || floMovingYPos != 0)
            {
                //设置角色的朝向（朝向当前坐标+摇杆偏移量）
                if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.MagicTrickB)
                {
                    transform.LookAt(new Vector3(transform.position.x - floMovingXPos, transform.position.y, transform.position.z - floMovingYPos));
                }
                //移动玩家的位置（按朝向位置移动）  
                Vector3 movement = transform.forward * Time.deltaTime * FloHeroMovingSpeed;
                //增加模拟重力
                movement.y -= _FloGravity;
                if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle ||
                    Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Runing)
                {
                    _cc.Move(movement);

                    //播放奔跑动画
                    if (UnityHelper.GetInstance().GetSmallTime(0.3F))//0.2
                    {
                        Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Runing);
                    }
                }
            }
            //else
            //{
            //    //if (UnityHelper.GetInstance().GetSmallTime(0.2F))
            //    //{
            //    //Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Idle);
            //    //}
            //}
        }
        //#endif
    }//Class_end
}