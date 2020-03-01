using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public Vars
    public float moveSpeed = 1.0f;
    public float jumpForce = 1.0f;
    // Check if character is grounded
    [HideInInspector]
    public bool isGrounded;

    // Private vars
    private Rigidbody2D rigidBody;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check to not go faster than max speed (moveSpeed)
        if (rigidBody.velocity.x > moveSpeed)
        {
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
            return;
        }
        else if (rigidBody.velocity.x < -moveSpeed)
        {
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);
            return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if rigidbody is assigned/found
        if (rigidBody)
        {
            // Update vertical movement (X axis)
            HorizontalMovement();

            // Jump if "Jump" button is pressed and character is on the ground
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }
        }
    }

    void HorizontalMovement()
    {
        float xInput = Input.GetAxis("Horizontal");
        Vector2 force = new Vector2(0.0f, 0.0f);
        force.x = xInput * moveSpeed;

        rigidBody.AddForce(force);
    }

    private void Jump()
    {
        rigidBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
    }

    // Flip Facing Direction
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
