using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public float distance = 50.0f;
    public LayerMask mask;
    public LineRenderer rope;

    private Vector3 targetPos;
    private RaycastHit2D hit;
    private float ropePull = 4.0f;
    private DistanceJoint2D joint;
    private Rigidbody2D rigidBody;
    private GameObject[] asteroids;
    private float asteroidDistance;
    private float compDistance;

    void Start()
    {
        CircleCollider2D cc2d = GetComponent<CircleCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
        compDistance = 100.0f;

        asteroids = GameObject.FindGameObjectsWithTag("Hooker");
    }

    // Update is called once per frame
    void Update()
    {
        if (joint.distance > 1.0f)
        {
            joint.distance -= Time.deltaTime * ropePull;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //asteroid position

            foreach (GameObject asteroid in asteroids)
            {
                asteroidDistance = Vector2.Distance(asteroid.transform.position, transform.position);// (asteroid.transform.position.x - transform.position.x) * (asteroid.transform.position.x - transform.position.x) + (asteroid.transform.position.y - transform.position.y) * (asteroid.transform.position.y - transform.position.y);
                Debug.Log(asteroidDistance.ToString());

                if (asteroidDistance < compDistance)
                {
                    targetPos = asteroid.transform.position;
                    targetPos.z = 0;
                    compDistance = asteroidDistance;

                }
            }

            asteroidDistance = Vector2.Distance(targetPos, transform.position);//(targetPos.x - transform.position.x) * (targetPos.x - transform.position.x) + (targetPos.y - transform.position.y) * (targetPos.y - transform.position.y);
            // Get the player position as origin, the mouse position for direction, and a distance.
            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, mask);

            //if hit and the object is a rigid body.
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null && asteroidDistance < 5.0f)
            {

                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();

                joint.distance = Vector2.Distance(transform.position, hit.point);

                // Set rope position
                rope.enabled = true;
                rope.SetPosition(0, transform.position);
                rope.SetPosition(1, hit.point);

                rigidBody.AddForce(new Vector2((targetPos.x - transform.position.x), (targetPos.y - transform.position.y) * 2));

            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rope.SetPosition(0, transform.position);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            joint.enabled = false;
            rope.enabled = false;
        }
    }

    //
    /// 
    ////  BRONK GRAPPLING FOR WHEN PLAYER PRESSES SPACE WITHIN VICINITY */FIX/*
    ///
    //

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    Collider2D collider = collision.GetComponent<Collider2D>();
    //    if (collider.tag == "Hooker")
    //    {
    //        GameObject gobject = collision.gameObject;
    //        Debug.Log(gobject.name);

    //        targetPos = gobject.transform.position;
    //        targetPos.z = 0.0f;

    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            Debug.Log("Grapple");
    //            //if hit and the object is a rigid body.
    //            if (collider != null && collider.gameObject.GetComponent<Rigidbody2D>() != null)
    //            {
    //                joint.enabled = true;
    //                joint.connectedBody = collider.gameObject.GetComponent<Rigidbody2D>();

    //                joint.distance = Vector2.Distance(transform.position, gobject.transform.position);

    //                // Set rope position
    //                rope.enabled = true;
    //                rope.SetPosition(0, transform.position);
    //                rope.SetPosition(1, gobject.transform.position);

    //                rigidBody.AddForce(new Vector2((targetPos.x - transform.position.x), (targetPos.y - transform.position.y) * 2));
    //            }
    //        }
    //    }
    //}
}
