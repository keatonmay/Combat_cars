using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour {

    public GameObject smoke;
    public Car_info info;

    // Update is called once per frame
    void Update () {
        float drift = info.drift;
 

        if (drift > 20 || drift < -20)
        {

            GameObject go = (GameObject)Instantiate(smoke, transform.position, transform.rotation);
            Destroy(go, 0.5f);


        }
        
    }
}
