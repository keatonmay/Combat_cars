using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Box : MonoBehaviour {

    public bool isDestroyed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isDestroyed = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Update()
    {
        
        if (isDestroyed == true)
        {
            Invoke("restoreBox", 5f);
            isDestroyed = false;
        }
    }

    void restoreBox()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        
    }
}
