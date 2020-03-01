using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public float distance = 20.0f;
    public LayerMask mask;
    public LineRenderer rope;

    private Vector3 targetPos;
    private RaycastHit2D hit;
    private float ropePull = 4.0f;
    private DistanceJoint2D joint;
    private Rigidbody2D rigidBody;
    private AudioManager audioMGR;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
        audioMGR = FindObjectOfType<AudioManager>();

        if (!audioMGR) { Debug.Log("No audio manager"); }
    }

    // Update is called once per frame
    void Update()
    {
        if (joint.distance > 2.0f)
        {
            joint.distance -= Time.deltaTime * ropePull;
        }

        if (Input.GetMouseButtonDown(0))
        {
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

                rigidBody.velocity = new Vector2((targetPos.x - transform.position.x), (targetPos.y - transform.position.y) *2);
            }
        }
        if (Input.GetMouseButton(0))
        {
            rope.SetPosition(0, transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
            rope.enabled = false;
        }
    }
}
