/***
 *
 *	Project:“地下守护神” Dungeon Fighter
 *
 *	Title:控制层：主角移动控制脚本（通过EasyTouch 插件）
 *
 *	Description:
 *		1.
 *
 *	Date:2017.02.21
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
    public class Ctrl_HeroMovingCtrlByET : BaseControl
    {
        //#if UNITY_ANDROID || UNITY_IPHONE
        public float FloHeroMovingSpeed = 5F;                                  //英雄移动的速度
        public float FloHeroAttackMoveingSpeed = 10F;                          //英雄攻击移动速度

        private CharacterController _cc;
        private float _FloGravity = 1F;                                        //角色控制器模拟重力

        #region 事件注册
        /// <summary>
        /// 游戏对象的启用
        /// </summary>
        void OnEnable()
        {
            EasyJoystick.On_JoystickMove += OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
        }
        /// <summary>
        /// 游戏对象的禁用
        /// </summary>
        public void OnDisable()
        {
            EasyJoystick.On_JoystickMove -= OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
        }
        /// <summary>
        /// 游戏对象的销毁
        /// </summary>
        public void OnDestroy()
        {
            EasyJoystick.On_JoystickMove -= OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
        }
        #endregion

        void Start()
        {
            _cc = GetComponent<CharacterController>();
            //攻击移动
            StartCoroutine("AttackByMove");
        }

        /// <summary>
        /// 攻击移动
        /// </summary>
        /// <returns></returns>
        IEnumerator AttackByMove()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
                if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.NormalAttack)
                {
                    Vector3 vec = transform.forward * FloHeroAttackMoveingSpeed * Time.deltaTime;
                    _cc.Move(vec);
                }
            }
        }

        /// <summary>
        /// 移动摇杆中 
        /// </summary>
        /// <param name="move"></param>
        void OnJoystickMove(MovingJoystick move)
        {
            if (move.joystickName != GlobalParameter.JOYSTICK_NAME)
            {
                return;
            }

            //获取摇杆中心偏移的坐标  
            float joyPositionX = move.joystickAxis.x;
            float joyPositionY = move.joystickAxis.y;

            if (joyPositionY != 0 || joyPositionX != 0)
            {
                //设置角色的朝向（朝向当前坐标+摇杆偏移量)
                if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.MagicTrickB)
                {
                    transform.LookAt(new Vector3(transform.position.x - joyPositionX, transform.position.y, transform.position.z - joyPositionY));
                }
                //移动玩家的位置（按朝向位置移动）  
                Vector3 movement = transform.forward * Time.deltaTime * FloHeroMovingSpeed;
                //增加模拟重力
                movement.y -= _FloGravity;
                //角色控制器
                if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle ||
                    Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Runing)
                {
                    _cc.Move(movement);
                    //播放奔跑动画  
                    if (UnityHelper.GetInstance().GetSmallTime(0.3F))//0.1
                    {
                        Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Runing);
                    }
                }
            }
        }

        /// <summary>
        /// 移动摇杆结束
        /// </summary>
        /// <param name="move"></param>
        void OnJoystickMoveEnd(MovingJoystick move)
        {
            //停止时，角色恢复idle  
            if (move.joystickName == GlobalParameter.JOYSTICK_NAME)
            {
                if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle ||
                    Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Runing)
                {
                    Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Idle);
                }
            }
        }
        //#endif
    }
}