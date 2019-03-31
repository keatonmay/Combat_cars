using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_info : MonoBehaviour {

    public int lapCounter;
    public int waypointCounter;
    public float drift;

    // Use this for initialization
    void Start () {
        lapCounter = 0;
        waypointCounter = 0;
        drift = 0;
	}
	
}
