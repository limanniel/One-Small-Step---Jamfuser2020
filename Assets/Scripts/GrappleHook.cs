using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    
    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    public float distance = 30.0f;
    public LayerMask mask;
    public LineRenderer rope;
    float ropePull = 3.0f;

    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (joint.distance > 1.0f)
            joint.distance -= Time.deltaTime * ropePull;
        else
        {
            rope.enabled = false;
            joint.enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //this.GetComponent<ScriptPlayerController>().GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            // Get the player position as origin, the mouse position for direction, and a distance.
            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, mask);
            
            //if hit and the object is a rigid body.
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();

                joint.distance = Vector2.Distance(transform.position, hit.point);

                // Set rope position
                rope.enabled = true;
                rope.SetPosition(0, transform.position);
                rope.SetPosition(1, hit.point);
            }
        }
        if (Input.GetMouseButton(0))
        {
            rope.SetPosition(0, transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //this.GetComponent<ScriptPlayerController>().GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            joint.enabled = false;
            rope.enabled = false;
        }
    }
}
