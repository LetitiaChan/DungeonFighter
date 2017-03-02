using UnityEngine;
using System.Collections;

public class elite : MonoBehaviour {


    private float speed;

	// Use this for initialization
	void Start () {
        speed = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
        //speed *= Time.deltaTime*20.0f;
        transform.Rotate(0,2.0f,0);
	
	}
}
