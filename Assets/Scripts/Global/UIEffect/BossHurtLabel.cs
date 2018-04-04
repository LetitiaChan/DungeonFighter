using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Global
{
    public class BossHurtLabel : MonoBehaviour
    {
        private int _CurrentReduceHpNumber = int.MinValue;

        void OnEnable()
        {
            Play();
        }
        void Start()
        {
            Play();
        }
        void OnDisable()
        {
            transform.GetComponent<Text>().text = "";
            transform.localScale = new Vector3(0.5f, 0.3f, 0.5f);
            transform.localPosition = new Vector3(220, 20, 0);
        }

        public void SetReduceHPNumber(int number)
        {
            _CurrentReduceHpNumber = -Mathf.Abs(number);
            transform.GetComponent<Text>().text = _CurrentReduceHpNumber.ToString();
        }

        private void Play()
        {
            if (_CurrentReduceHpNumber == int.MinValue)
            {
                //Debug.Log(GetType() + "Start()/_CurrentReduceHpNumber is invalid!");
                return;
            }
            transform.GetComponent<Text>().text = _CurrentReduceHpNumber.ToString();

            transform.DOScale(new Vector3(1, 0.6f, 1), 1f);
            var tweener = transform.DOLocalMove(transform.localPosition + new Vector3(90, 100, 0), 1f);
            tweener.SetEase(Ease.OutCirc);
        }
    }
}