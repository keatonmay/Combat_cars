using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Waypoints : MonoBehaviour {


    public Waypoints nextWaypoint;

    public Waypoints getNextWaypoint()
    {
        return nextWaypoint;
    }
}
