using UnityEngine;
using System.Collections;


public enum CSPointType { 

    MainTown,
    None

}

public class csPoint : MonoBehaviour {



    public Transform myTransform;
    public GameObject Playerobj;

    public CSPointType csPointType;

    public bool incsPoint;
    //public float distance;
    public float DelayTime;

    // Use this for initialization
    void Start()
    {
        myTransform = this.transform;
        Playerobj = GameObject.FindGameObjectWithTag("Player");

        incsPoint = false;
        DelayTime = 1.5f;



    }

    // Update is called once per frame
    void Update()
    {

        //distance = Vector3.Distance(Playerobj.transform.position, myTransform.position - new Vector3(0,-2,0));

        //if (distance<= 3f)
        //{
        //    incsPoint = true;
        //}
        //else
        //{
        //    incsPoint = false;
        //}

        if (incsPoint)
        {
            DelayTime -= Time.deltaTime;




            //Application.LoadLevel("MainTown");
        }

        if (DelayTime <= 0)
        {
            CSMoveToScene();
        }
        if (DelayTime <= -2)
        {
            DelayTime = 1.5f;
        }



    }

    void OnGUI()
    {

       // GUI.Label(new Rect(111, 111, 111, 111), distance.ToString());
    
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals("Player"))
        {

            incsPoint = true;

        }



    }


    /// <summary>
    /// 
    /// </summary>
    public void CSMoveToScene()
    {
        switch (csPointType)
        {
            case CSPointType.MainTown:

                Application.LoadLevel("MainTown");



                break;
            case CSPointType.None:
                break;
            default:
                break;
        }



    
    }


}
