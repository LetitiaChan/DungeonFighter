using UnityEngine;
using System.Collections;

public class arrowControl : MonoBehaviour {


    public float _speed=6;
    private float DestoryTime;
	// Use this for initialization
	void Start () {
        DestoryTime = 10;
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        DestoryTime -= Time.deltaTime;
        if (DestoryTime <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    int ArrowDamage;

    public void SetDamage(int a)
    {
        ArrowDamage = a;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Player"))
        {
            //OnDamage
            other.SendMessage("PlayerOnDamage", -ArrowDamage, SendMessageOptions.DontRequireReceiver);
            other.SendMessage("SetHitWound",SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
        }



    }
    


}
