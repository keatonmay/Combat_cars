using UnityEngine;

public class Car_Controller : MonoBehaviour {

    public float maxSpeed;
    public float acceleration;
    public float steering;
    public int  lapCounter;
    public Race_Manager manager;
    public GameObject missile;
    public GameObject oil;


    private Rigidbody2D rb;
    private float curSpeed;
    public Waypoints target;
    private int numberOfWaypoints;
    public int weapon;
    public bool hasWeapon = false;

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
            lapCounter = other.lapCounter;
            
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

    // Update is called once per frame
    void FixedUpdate () {        

        float h;
        float v;
        var carinfo = GetComponent<Car_info>();

        if (this.gameObject.name == "Player1")
        {
            h = -Input.GetAxis("P_1Horizontal");
            v = Input.GetAxis("P_1Vertical");
        }
        else
        {
            h = -Input.GetAxis("P_2Horizontal");
            v = Input.GetAxis("P_2Vertical");
        }
        

        Vector2 speed = transform.up * (v * acceleration);
        rb.AddForce(speed);

        float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));

        if (direction >= 0.0f)
        {
            rb.rotation += h * steering * (rb.velocity.magnitude / maxSpeed);
        }
        else
        {
            rb.rotation -= h * steering * (rb.velocity.magnitude / maxSpeed);
        }

        float drift = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left)) * 2.0f;

        Vector2 relativeForce = Vector2.right * drift;
        Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeForce), Color.green);
        rb.AddForce(rb.GetRelativeVector(relativeForce));

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

       

        if (this.gameObject.name == "Player1")
        {
            if (Input.GetAxis("Fire1") > 0)
            {
                if (weapon == 0 && hasWeapon == true)
                {
                    GameObject go = (GameObject)Instantiate(missile, transform.position + (transform.up * 5), transform.rotation);
                    hasWeapon = false;
                }
                else if (weapon == 1 && hasWeapon == true)
                {
                    GameObject go = (GameObject)Instantiate(oil, transform.position - (transform.up * 5), transform.rotation);
                    hasWeapon = false;
                }
            }
        }

        
        
        else
        {
            if (Input.GetAxis("Fire2") > 0)
            {
                if (weapon == 0 && hasWeapon == true)
                {
                    GameObject go = (GameObject)Instantiate(missile, transform.position + (transform.up * 5), transform.rotation);
                    hasWeapon = false;
                }
                else if (weapon == 1 && hasWeapon == true)
                {
                    GameObject go = (GameObject)Instantiate(oil, transform.position - (transform.up * 5), transform.rotation);
                    hasWeapon = false;
                }
            }
        }

        carinfo.drift = drift;

        curSpeed = rb.velocity.magnitude;
	}
}
