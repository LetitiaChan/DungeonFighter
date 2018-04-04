using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Global;
using Control;
using DG.Tweening;

namespace View
{
    public class View_BossInfoDisplay : MonoBehaviour
    {
        public GameObject goBossInfo;
        public Slider Sli_HP;
        public Slider Sli_HPGray;
        public Text TxtBossHP_Cur;
        public Text TxtBossHP_Max;

        private float _CurHP = 10000f;
        private float _MaxHP = 10000f;
        private float _speed = 0.1f;

        void Awake()
        {
            Ctrl_BossBruce_Property.eveBossAttr += ChangeHP;
        }

        void Start()
        {
            goBossInfo.SetActive(false);
        }


        public void ChangeHP(KeyValuesUpdate kv)
        {
            if (goBossInfo && kv.Key.Equals("BossHPChange"))
            {
                float[] attr = (float[])kv.Value;
                _CurHP = (attr.Length > 0) ? attr[0] : _CurHP;
                _MaxHP = (attr.Length > 1) ? attr[1] : _MaxHP;
                _MaxHP = _MaxHP <= 0 ? 1 : _MaxHP; //被除数保证大于0

                goBossInfo.SetActive(_CurHP > 0 && Ctrl_LevelTwoScenes.Instance.IsBossBruceDead == false);

                if (TxtBossHP_Cur && TxtBossHP_Max && Sli_HP)
                {
                    TxtBossHP_Cur.text = _CurHP.ToString();
                    TxtBossHP_Max.text = _MaxHP.ToString();

                    Sli_HP.value = _CurHP / _MaxHP * Sli_HP.maxValue;
                    var obj = Sli_HPGray.DOValue(_CurHP / _MaxHP * Sli_HPGray.maxValue, 2f);
                    obj.SetEase(Ease.OutCirc);
                }

            }
        }
    }
}