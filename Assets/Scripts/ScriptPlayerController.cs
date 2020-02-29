using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayerController : MonoBehaviour
{
    public float fSpeed;
    public float fMaxJump = 30.0f;
    public float fJump = 15.0f;
    public float fMaxSpeed;
    public bool bIsJumping;

    public Vector2 jumpForce;

    private new Rigidbody2D rigidbody2D;
    private new Animator animation;
    private bool facingLeft;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        rigidbody2D.isKinematic = false;
        facingLeft = false;

        fSpeed = 7.5f;
        rigidbody2D.gravityScale = 10.0f;
        fJump = 4.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        

        if (Input.GetKeyDown(KeyCode.W) && !bIsJumping)
        {
            Jump();
        }

        Vector2 movement = new Vector2(moveX, 0.0f);

        rigidbody2D.velocity = (movement * fSpeed) + jumpForce;
        animation.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));

        if (jumpForce.y != 0)
            jumpForce.y -= Time.deltaTime;

        if (moveX < 0 && !facingLeft)
        {
            reverseImage();
        }
        else if (moveX > 0 && facingLeft)
        {
            reverseImage();
        }

    }

    public void HandleCollision(PlayerCollisionHandler playerCollisionHandler)
    {
        Debug.Log("You're colliding!");
        bIsJumping = false;
        jumpForce.y = 0.0f;
    }

    private void Jump()
    {
        bIsJumping = true;
        jumpForce.y = fJump;
        
    }

    private void reverseImage()
    {
        facingLeft = !facingLeft;
        Vector3 scale = rigidbody2D.transform.localScale;
        scale.x *= -1;
        rigidbody2D.transform.localScale = scale;
    }
}
