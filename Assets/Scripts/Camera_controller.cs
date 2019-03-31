using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    


    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update () {

        transform.position = player.transform.position + offset;
    }
}
