using UnityEngine;
using System.Collections;

public class fireexp : MonoBehaviour
{

    private float destorytime;

    void Start()
    {
        destorytime = 4;
    }

    void Update()
    {

        destorytime -= Time.deltaTime;

        if (destorytime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
