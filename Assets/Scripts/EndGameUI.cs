using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour {

    public Text Positions;
    public Race_Manager manager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (manager.numberOfCars == 3)
        {
            Positions.text = "1. " + manager.cars[0].gameObject.name + "\n2. " + manager.cars[1].gameObject.name + "\n3. " + manager.cars[2].gameObject.name;
        }

        if (manager.numberOfCars == 4)
        {
            Positions.text = "1. " + manager.cars[0].gameObject.name + "\n2. " + manager.cars[1].gameObject.name + "\n3. " + manager.cars[2].gameObject.name + "\n4. " + manager.cars[3].gameObject.name;
        }
    }
}
