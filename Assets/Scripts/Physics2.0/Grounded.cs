using UnityEngine;

public class Grounded : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Find parent that the script is attached to
        player = gameObject.transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            player.GetComponent<PlayerController>().isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            player.GetComponent<PlayerController>().isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            player.GetComponent<PlayerController>().isGrounded = false;
        }
    }
}
