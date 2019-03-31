using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Race_Manager : MonoBehaviour {

    public GameObject[] cars;
    public int numberOfCars;
    public int numberOfLaps;
    public string count;

	// Use this for initialization
	void Start () {
        StartCoroutine(countdown());
        cars = GameObject.FindGameObjectsWithTag("Car");
        numberOfCars = cars.Length;

        for (int i = 0; i < numberOfCars; i++)
        {
            cars[i].GetComponent<Rigidbody2D>().isKinematic = true;
        }
        GameObject.Find("EndGameScreen").GetComponent<Canvas>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        getPositions();
        racefinish();
	}

    IEnumerator countdown()
    {
        count = "3";
        yield return new WaitForSeconds(1);

        count = "2";
        yield return new WaitForSeconds(1);

        count = "1";
        yield return new WaitForSeconds(1);

        for (int i = 0; i < numberOfCars; i++)
        {
            cars[i].GetComponent<Rigidbody2D>().isKinematic = false;
        }

        count = "Go!";
        yield return new WaitForSeconds(1);

        count = "";

    }

    void racefinish()
    {
        var other = cars[0].GetComponent<Car_info>();
        
        if (other.lapCounter == numberOfLaps)
        {
            endState();
        }
        

    }


    // return the car in front of the calling object. this will be used to find a target for some weapons.
    public GameObject GetNextPosition(string objName)
    {
        for (int i = 0; i < numberOfCars; i++)
        {
            if (cars[i].gameObject.name == objName && i == 0)
            {
                return null;
            }
            else if (cars[i].gameObject.name == objName)
            {
                return cars[i - 1];
            }
        }

        return null;
    }


    // this function arranges the car game objects from first to last place. index 0 is in 1st place
    void getPositions()
    {
        for(int i = 0; i < numberOfCars-1; i++)
        {
            var other = cars[i].GetComponent<Car_info>();
            var comp = cars[i + 1].GetComponent<Car_info>();

            GameObject temp;

            if (other.lapCounter < comp.lapCounter)
            {
                temp = cars[i];
                cars[i] = cars[i + 1];
                cars[i + 1] = temp;
            }
            else if (other.lapCounter == comp.lapCounter)
            {
                if (other.waypointCounter < comp.waypointCounter)
                {
                    temp = cars[i];
                    cars[i] = cars[i + 1];
                    cars[i + 1] = temp;
                }
                
            }
        }
    }

    void endState()
    {

        for (int i = 0; i < numberOfCars; i++)
        {
            cars[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            cars[i].GetComponent<Rigidbody2D>().angularVelocity = 0f;

        }
        GameObject.Find("EndGameScreen").GetComponent<Canvas>().enabled = true;
    }
}
