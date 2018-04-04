using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Control;

namespace Global
{
    /// <summary>
    /// 飘字效果
    /// </summary>
    public class MoveUpLabel : MonoBehaviour
    {
        public float MoveSpeed = 0.02f;
        public float PrefabLength = 2f;
        public float PrefabHeight = 1f;
        public float PositonOffset = 2f;
        private float _HPCurrent;
        private float _HPMax;

        private GameObject _TargetObj;
        private Camera _WorldCamera;
        private Camera _UICamera;
        private Text _UIText;

        private int _CurrentReduceHpNumber = Int32.MinValue;

        public void SetTarget(GameObject go)
        {
            _TargetObj = go;
        }

        public void SetReduceHPNumber(int number)
        {
            _CurrentReduceHpNumber = -Mathf.Abs(number);
        }

        void Start()
        {
            _UIText = GetComponent<Text>();
            _WorldCamera = Camera.main.gameObject.GetComponent<Camera>();
            _UICamera = GameObject.FindGameObjectWithTag(Tag.Tag_UICamera).GetComponent<Camera>();
            if (_TargetObj == null)
            {
                Debug.LogError(GetType() + "Start()/_TargetObj = null!");
                return;
            }
            if (_CurrentReduceHpNumber == Int32.MinValue)
            {
                Debug.LogError(GetType() + "Start()/_CurrentReduceHpNumber is invalid!");
                return;
            }
        }

        void Update()
        {
            if (Time.frameCount % 3 == 0)
            {
                _UIText.text = _CurrentReduceHpNumber.ToString();
                transform.localScale = new Vector3(PrefabLength, PrefabHeight, 0);
                PositonOffset += MoveSpeed; //位置偏移量
                /* 销毁由缓冲池负责(略) RecorvedObjByTime */
            }
        }

        void LateUpdate()
        {
            if (_TargetObj != null)
            {
                if (Time.frameCount % 3 == 0)
                {
                    //三维坐标系与UI坐标系转换
                    Vector3 pos = _WorldCamera.WorldToScreenPoint(_TargetObj.transform.position);
                    pos = _UICamera.ScreenToWorldPoint(pos);
                    transform.position = new Vector3(pos.x, pos.y + PositonOffset, 0);
                }
            }
        }
    }
}