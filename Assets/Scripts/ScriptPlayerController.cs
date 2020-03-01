using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptPlayerController : MonoBehaviour
{
    [SerializeField]
    public GameObject DeathText1;

    [SerializeField]
    public GameObject DeathText2;

    [SerializeField]
    public GameObject DeathText3;

    [SerializeField]
    public GameObject DeathText4;

    [SerializeField]
    public GameObject DeathText5;

    [SerializeField]
    public GameObject DeathText6;

    [Range(0, 30f)]
    public float fSpeed;
    [Range(0, 15)]
    public float fMaxSpeed;

    public float fMaxJump;

    public float fJumpTimer;
    public bool bIsJumping;
    public LayerMask ground;

    public CameraShake cameraShake;

    private new Rigidbody2D rigidbody2D;
    private new Animator animation;
    private bool facingLeft;

    private Vector2 movement;

    public Vector2 wallJumpDir;
    public Vector2 wallFacingDir;
    public float fWallJumpForce;
    public float fWallFacingForce;
    public Transform wallRaycast;
    public float fWallRaycastDistance;
    private RaycastHit2D wallCheckHit;
    public bool bWallSliding;
    private float fMaxWallSlideVelocity;
    public bool bIsInAir;
    enum CHARACTERDEATH
    {
        NOTDEAD = 0,
        ACIDLASER,
        KARS,
        BLACKHOLE
    }
    CHARACTERDEATH characterDeath = CHARACTERDEATH.NOTDEAD;

   
    // Start is called before the first frame update
    void Start()
    {
        
        rigidbody2D = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        rigidbody2D.isKinematic = false;
        facingLeft = false;
        //fFallMultiplier = 2.5f;
        //fLowJumpMultiplier = 2.0f;
        wallJumpDir.Normalize();
        wallFacingDir.Normalize();
        fMaxWallSlideVelocity = 0.0f;
        bIsInAir = false;
        fSpeed = 5f;
        fMaxSpeed = 5f;
        fMaxJump = 1.0f;
        rigidbody2D.gravityScale = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterChecker();

        float moveX = Input.GetAxis("Horizontal");
        movement = new Vector2(moveX, 0.0f);

        if (!bIsInAir) rigidbody2D.AddForce(movement * fSpeed * 5);
        rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, fMaxSpeed);
        animation.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            bIsJumping = true;
            StartCoroutine(cameraShake.Shake(0.6f, 0.015f));
        }

        if (wallCheckHit && rigidbody2D.velocity.y <= 0 && !IsGrounded())
        {
            bWallSliding = true;
        }
        else
            bWallSliding = false;

        if (moveX < 0 && !facingLeft)
        {
            reverseImage();
        }
        else if (moveX > 0 && facingLeft)
        {
            reverseImage();
        }

        if (transform.position.y < -50)
        {
            DeathText1.SetActive(true);
            if (transform.position.y < -100)
            {
                DeathText2.SetActive(true);
                if (transform.position.y < -150)
                {
                    DeathText3.SetActive(true);
                    if (transform.position.y < -200)
                    {
                        DeathText4.SetActive(true);
                        if (transform.position.y < -250)
                        {
                            DeathText5.SetActive(true);
                            if (transform.position.y < -300)
                            {
                                DeathText6.SetActive(true);
                            }
                        }
                    }
                }

                if (IsGrounded()) bIsInAir = false;

            }
        }

    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GonDie")
        {
            characterDeath = CHARACTERDEATH.ACIDLASER;
        }
    }

    private void CharacterChecker()
    {
        if(characterDeath != CHARACTERDEATH.NOTDEAD)
        {
            if(Input.anyKey)
            {
                SceneManager.LoadScene("PauseTest");
            }
        }
    }

    private void FixedUpdate()
    {
        if (bIsJumping && Input.GetButton("Jump"))
        {
            Jump();
        }
        else
        {
            bIsJumping = false;
            fJumpTimer = 0.0f;
        }

        wallCheckHit = Physics2D.Raycast(wallRaycast.position, wallRaycast.right, fWallRaycastDistance, ground);

        if (wallCheckHit)
        {
            //Debug.Log("Wall Touch");
        }

        if (bWallSliding)
        {
            bIsJumping = true;
            if (rigidbody2D.velocity.y < -fMaxWallSlideVelocity)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -fMaxWallSlideVelocity);
            }
        }

    }

    private void Jump()
    {
        if (fJumpTimer < fMaxJump)
        {
            fJumpTimer += Time.deltaTime * 8;
            if (fJumpTimer > 0.6)

                if (!bWallSliding)
                {
                    bIsInAir = false;
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, fJumpTimer * 5.0f);
                }

                else
                {
                    rigidbody2D.velocity = new Vector2(-150.0f + (fSpeed * 3), fJumpTimer * 5.0f);
                    bIsInAir = true;
                    reverseImage();

                }
        }
    }

    public void HandleCollision(PlayerCollisionHandler playerCollisionHandler)
    {
        //Debug.Log("You're colliding!");
    }

    private bool IsGrounded()
    {
        Vector2 pos = transform.position;
        Vector2 dir = Vector2.down;
        float dist = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(pos, dir, dist, ground);
        if (hit.collider != null)
            return true;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallRaycast.position, wallRaycast.right);
        //Gizmos.DrawLine(wallRaycast.position, wallRaycast.position + fWallRaycastDistance);
    }
    private void reverseImage()
    {
        facingLeft = !facingLeft;
        Vector3 scale = rigidbody2D.transform.localScale;
        scale.x *= -1;
        rigidbody2D.transform.localScale = scale;
    }
}
