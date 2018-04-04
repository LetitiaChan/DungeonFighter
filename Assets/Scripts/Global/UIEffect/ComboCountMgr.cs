using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Global
{
    /// <summary>
    /// 连击
    /// </summary>
    public class ComboCountMgr : MonoBehaviour
    {
        public static ComboCountMgr Instance;
        public float PrefabLength = 2f;
        public float PrefabHeight = 1f;

        private int ComboCount = 0;
        private float _CurPrefabLength;
        private float _CurPrefabHeight;
        private float _ComboDelayTime = 3f;
        private Text txtDisplayComboCount;


        public void UpdateComboCount()
        {
            if (GlobalParaMgr.IsBossFightingScene)
            {
                ++ComboCount;
                _ComboDelayTime = 3f;
                _CurPrefabLength = PrefabLength + 1f;
                _CurPrefabHeight = PrefabHeight + 1f;
            }
        }

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            txtDisplayComboCount = GetComponent<Text>();
            _CurPrefabLength = PrefabLength + 1f;
            _CurPrefabHeight = PrefabHeight + 1f;
            transform.localScale = new Vector3(_CurPrefabLength, _CurPrefabHeight, 0);
        }

        void Update()
        {
            //scale变化效果
            if (_CurPrefabLength >= PrefabLength || _CurPrefabHeight >= PrefabHeight)
            {
                _CurPrefabLength -= 0.1f;
                _CurPrefabHeight -= 0.1f;
            }
            transform.localScale = new Vector3(_CurPrefabLength, _CurPrefabHeight, 0);
            txtDisplayComboCount.enabled = ComboCount > 0;
            txtDisplayComboCount.text = "Combo " + ComboCount;
            if (ComboCount > 0)
            {
                _ComboDelayTime -= Time.deltaTime;
                if (_ComboDelayTime <= 0)
                {
                    ComboCount = 0;
                    _CurPrefabLength = PrefabLength + 1f;
                    _CurPrefabHeight = PrefabHeight + 1f;
                }
            }
        }
    }
}