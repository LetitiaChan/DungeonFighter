using UnityEngine;
using System.Collections;
using Global;
using Kernal;

namespace Control
{
    public class Arrow : BaseControl
    {
        public float arrowSpeed = 1f;
        public float arrowATK = 40;
        private Ctrl_HeroProperty _HeroProperty;

        void Start()
        {
            GameObject goHero = GameObject.FindGameObjectWithTag(Tag.Player);
            if (goHero)
            {
                _HeroProperty = goHero.GetComponent<Ctrl_HeroProperty>();
            }
        }

        void Update()
        {
            if (Time.frameCount % 2 == 0)
            {
                this.gameObject.transform.Translate(Vector3.forward * arrowSpeed * Time.deltaTime);
            }
        }

        void OnTriggerEnter(Collider con)
        {
            if (con.gameObject.tag == Tag.Player)
            {
                _HeroProperty.DecreaseHealthValues(arrowATK);
                //Destroy(this.gameObject, 0.5f);
                PoolManager.PoolsArray["_ParticalSys"].RecoverGameObjectToPools(this.gameObject);
            }
        }

    }
}