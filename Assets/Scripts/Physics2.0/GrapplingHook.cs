using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public float hitMaxDistance = 20.0f;
    public float pullStep = 0.05f;
    public LayerMask layerMask;
    public LineRenderer line;

    private DistanceJoint2D joint;
    private RaycastHit2D hit;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (joint.distance > 1.0f)
        {
            joint.distance -= pullStep * Time.deltaTime;
        }
        else
        {
            line.enabled = false;
            joint.enabled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0.0f;
            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, hitMaxDistance, layerMask);

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                joint.distance = Vector2.Distance(transform.position, hit.point);

                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);
            }
        }

        if (Input.GetMouseButton(0))
        {
            line.SetPosition(0, transform.position);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
            line.enabled = false;
        }
    }
}
