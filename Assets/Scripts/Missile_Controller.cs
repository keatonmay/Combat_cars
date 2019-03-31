using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Controller : MonoBehaviour {

    Rigidbody2D rb;
    private float movespeed = 60;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

        var locVel = transform.InverseTransformDirection(rb.velocity);
        locVel.y = movespeed;
        rb.velocity = transform.TransformDirection(locVel);
    }
	
	// Update is called once per frame
	void Update () {



    }


}
