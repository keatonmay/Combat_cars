using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour {

    private Rigidbody2D rb;
    public float steering;
    public float acceleration;
    public float maxSpeed;
    public Waypoints target;
    private float curSpeed;
    Transform targetTransform;
    public Race_Manager manager;
    private int numberOfWaypoints;
    public int weapon;
    public bool hasWeapon = false;
    public GameObject missile;
    public GameObject oil;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        numberOfWaypoints = GameObject.FindGameObjectsWithTag("Waypoint").Length;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Waypoint"))
        {
            var other = GetComponent<Car_info>();
            target = target.getNextWaypoint();
            other.waypointCounter++;
            if (other.waypointCounter == numberOfWaypoints + 1)
            {
                other.waypointCounter = 1;
                other.lapCounter++;
            }

        }
        if (collision.CompareTag("WeaponBox"))
        {
            weapon = Random.Range(0, 2);
            hasWeapon = true;
        }

        if (collision.CompareTag("Weapon"))
        {
            rb.velocity = new Vector2(0, 0);
            Destroy(collision.gameObject);
        }
    }

    void rotateToTarget()
    {
        // rotation
        Vector2 vectorToTarget = targetTransform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        angle -= 90;
        
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * steering);
    }

    void FixedUpdate () {

        // rotation
        targetTransform = target.transform;
        rotateToTarget();
        var carinfo = GetComponent<Car_info>();


        // forward motion and drift (same as player car), always accelerate
        Vector2 speed = transform.up * (1 * acceleration);
        rb.AddForce(speed);

        float drift = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left)) * 2.0f;

        Vector2 relativeForce = Vector2.right * drift;
        Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeForce), Color.green);
        rb.AddForce(rb.GetRelativeVector(relativeForce));

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        carinfo.drift = drift;

        curSpeed = rb.velocity.magnitude;


        RaycastHit2D hit = CheckRaycast();

        if (hasWeapon == true && weapon == 0)
        {
          if (hit.collider.tag == "Car")
            {
                GameObject go = (GameObject)Instantiate(missile, transform.position + (transform.up * 5), transform.rotation);
                hasWeapon = false;
            }
        }
        if (hasWeapon == true && weapon == 1)
        {

            Invoke("dropOil", Random.Range(0, 10));
            hasWeapon = false;
        }
    }

    private bool RaycastCheckUpdate()
    {
        RaycastHit2D hit = CheckRaycast();

        if (hit.collider)
        {
            Debug.Log("Hit" + hit.collider.name);
            return true;
        }
        else
        {
            return false;
        }
    }

    private RaycastHit2D CheckRaycast ()
    {
        float Reach = 20f;
        Vector2 up = transform.TransformDirection(Vector2.up);
        Debug.DrawRay(transform.position + Vector3.up * 3, up * Reach);

        return Physics2D.Raycast(transform.position + Vector3.up * 3, up * Reach);
    }
    void shootMissile()
    {
        GameObject go = (GameObject)Instantiate(missile, transform.position + (transform.up * 5), transform.rotation);
       
    }

    void dropOil()
    {
        GameObject go = (GameObject)Instantiate(oil, transform.position - (transform.up * 5), transform.rotation);
        
    }
}
