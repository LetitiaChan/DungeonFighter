using UnityEngine;
using System.Collections;
using Global;
using Kernal;

namespace Control
{
    /// <summary>
    /// 主角移动控制脚本（通过EasyTouch插件）
    /// </summary>
    public class Ctrl_HeroMovingCtrlByET : BaseControl
    {
        //#if UNITY_ANDROID || UNITY_IPHONE
        public float heroMovingSpeed = 5f;
        public float heroAttackMoveingSpeed = 10f;

        private CharacterController _cc;
        private float _gravity = 1f;

        #region 事件注册
        void OnEnable()
        {
            EasyJoystick.On_JoystickMove += OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
        }
        public void OnDisable()
        {
            CancelRunningState();
            EasyJoystick.On_JoystickMove -= OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
        }
        public void OnDestroy()
        {
            CancelRunningState();
            EasyJoystick.On_JoystickMove -= OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
        }
        #endregion

        void Start()
        {
            _cc = GetComponent<CharacterController>();
            StartCoroutine("AttackByMove");
        }

        IEnumerator AttackByMove()
        {
            yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
            while (true)
            {
                yield return new WaitForSeconds(GlobalParameter.INTERVAL_TIME_0DOT5);
                if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.NormalAttack)
                {
                    Vector3 vec = transform.forward * heroAttackMoveingSpeed * Time.deltaTime;
                    _cc.Move(vec);
                }
            }
        }

        void OnJoystickMove(MovingJoystick move)
        {
            if (move.joystickName != GlobalParameter.JOYSTICK_NAME) return;

            float joyPositionX = move.joystickAxis.x;
            float joyPositionY = move.joystickAxis.y;

            if (joyPositionY != 0 || joyPositionX != 0)
            {
                /*发大招、死亡 锁定方向*/
                if (!Ctrl_HeroAnimationCtrl.Instance.IsHeroActionMagicTrick() && Ctrl_HeroAnimationCtrl.Instance.CurrentActionState != HeroActionState.Dead)
                {
                    //设置角色的朝向（朝向当前坐标+摇杆偏移量)
                    transform.LookAt(new Vector3(transform.position.x - joyPositionX, transform.position.y, transform.position.z - joyPositionY));
                }
                Vector3 movement = transform.forward * Time.deltaTime * heroMovingSpeed;
                movement.y -= _gravity;

                if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle ||
                    Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Runing)
                {
                    _cc.Move(movement);
                    if (UnityHelper.GetInstance().isTimeOutSmall(GlobalParameter.INTERVAL_TIME_0DOT1))
                    {
                        Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Runing);
                    }
                }
            }
        }

        void OnJoystickMoveEnd(MovingJoystick move)
        {
            if (move.joystickName == GlobalParameter.JOYSTICK_NAME)
            {
                CancelRunningState();
            }
        }

        void CancelRunningState()
        {
            if (Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Idle ||
                Ctrl_HeroAnimationCtrl.Instance.CurrentActionState == HeroActionState.Runing)
            {
                Ctrl_HeroAnimationCtrl.Instance.SetCurrentActionState(HeroActionState.Idle);
            }
        }
        //#endif
    }
}