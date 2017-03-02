using UnityEngine;
using System.Collections;

public class thunderDestory : MonoBehaviour {


    private float DestoryTime;

	// Use this for initialization
	void Start () {
        DestoryTime = 7.0f;
	}
	
	// Update is called once per frame
	void Update () {

        DestoryTime -= Time.deltaTime;
        if (DestoryTime <= 0)
        {
            Destroy(this.gameObject);
        }


	}
}
