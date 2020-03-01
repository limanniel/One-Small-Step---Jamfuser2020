  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public Vars
    public float moveSpeed = 1.0f;
    public float jumpForce = 1.0f;
    // Check if character is grounded
    public bool isGrounded;
    public Transform wallCheck;
    public Transform groundCheck;
    public float wallCheckDistance;
    public LayerMask groundLayer;
    public float wallSlideSpeed;
    public float groundCheckRadius;
    public float movementForceInAir;
    public float wallPushForce;

    // Private vars
    private Rigidbody2D rigidBody;
    private Animator animator;
    private bool facingRight = true;
    [SerializeField]
    private bool isTouchingWall;
    private bool isWallSliding;
    private AudioManager audioMGR;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioMGR = FindObjectOfType<AudioManager>();
        
        if (!audioMGR) { Debug.Log("No audio manager"); }

        //Initialise Vars   Leave as comments to know which values we are using
        //moveSpeed = 5.0f;
        //jumpForce = 3.0f;
        wallCheckDistance = 0.15f;
        wallSlideSpeed = 0.2f;
        groundCheckRadius = 0.06f;
    }

    private void Update()
    {
        if (rigidBody)
        {
            // Jump if "Jump" button is pressed and character is on the ground
            if (Input.GetButtonDown("Jump") && (isGrounded || isWallSliding))
            {
                Jump();
            }

            if (!isWallSliding)
                HorizontalMovement();

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

            CheckIfWallSliding();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if rigidbody is assigned/found
        if (rigidBody)
        {
            
            if (isWallSliding)
            {
                if (rigidBody.velocity.y < -wallSlideSpeed)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, -wallSlideSpeed);
                }
            }

            
            // Update vertical movement (X axis) if not grounded
            //if (isGrounded)
            //    
            //else if (!isGrounded && !isWallSliding && Input.GetAxis("Horizontal") != 0.0f)
            //{
            //    Vector2 force = new Vector2(movementForceInAir * Input.GetAxis("Horizontal"), 0.0f);
            //    rigidBody.AddForce(force);

            //    if (Mathf.Abs(rigidBody.velocity.x) > moveSpeed)
            //    {
            //        rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigidBody.velocity.y);
            //    }
            //}
            //else if (!isGrounded && !isWallSliding && Input.GetAxis("Horizontal") == 0.0f)
            //{
            //    rigidBody.velocity = new Vector2(0.0f, rigidBody.velocity.y);
            //}

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            if (facingRight)
                isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, groundLayer);
            else
                isTouchingWall = Physics2D.Raycast(wallCheck.position, -transform.right, wallCheckDistance, groundLayer);

        }
    }

    void HorizontalMovement()
    {
        float xInput = Input.GetAxis("Horizontal");
        Vector2 force = new Vector2(0.0f, 0.0f);
        force.x = xInput * moveSpeed;

        rigidBody.AddForce(force);

        UpdateWalkingAnimation(xInput);
    }

    private void Jump()
    {
        if (!isWallSliding)
        {
            rigidBody.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
            FindObjectOfType<AudioManager>().Play("Jump");
            audioMGR.Play("Jump", 0.5f, 1.1f);
        }
        else
        {
            isWallSliding = false;
            Flip();
            float abs = 0.0f;
            if (facingRight) abs = 1.0f;
            if (!facingRight) abs = -1.0f;
            rigidBody.AddForce(new Vector2(abs * wallPushForce, jumpForce), ForceMode2D.Impulse);
            audioMGR.Play("Jump", 0.5f, 1.0f);
        }
    }

    private void UpdateWalkingAnimation(float xInput)
    {
        // Update Spritesheet
        animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));

        // Flip Sprite accordingly to facing direction
        if (xInput < 0 && facingRight)
        {
            Flip();
        }
        else if (xInput > 0 && !facingRight)
        {
            Flip();
        }
    }

    // Flip Facing Direction
    private void Flip()
    {
        if (!isWallSliding)
        {
            facingRight = !facingRight;
            Vector3 scale = rigidBody.transform.localScale;
            scale.x *= -1;
            rigidBody.transform.localScale = scale;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void CheckIfWallSliding()
    {
        if (isTouchingWall && !isGrounded && rigidBody.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
            isWallSliding = false;
    }
}
