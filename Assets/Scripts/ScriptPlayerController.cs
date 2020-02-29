using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayerController : MonoBehaviour
{
    public float fSpeed;
    public float fJump;
    public float fGravity;
    public float fMaxSpeed;
    public float fUpForce;

    private Vector2 netForce;

    [SerializeField]
    public Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        //rigidbody2D.isKinematic = true;
        netForce = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //NetForceCalc();

        //rigidbody2D.velocity = netForce;

        //if (rigidbody2D.velocity.magnitude > fMaxSpeed)
        //    rigidbody2D.velocity = rigidbody2D.velocity.normalized * fMaxSpeed;

    }

    //private void NetForceCalc()
    //{
    //    float moveX = Input.GetAxis("Horizontal");
    //    float moveY = Input.GetAxis("Vertical");
    //    Vector2 movement = new Vector2(moveX * fSpeed, moveY * fSpeed);
    //    Vector2 gravityForce = new Vector2(0.0f, fGravity);
    //    Vector2 upForce = new Vector2(0.0f, -fGravity);
    //    Vector2 jumpForce = new Vector2(0.0f, fJump);

    //    netForce = movement + jumpForce + gravityForce;
    //}

    public void HandleCollision(PlayerCollisionHandler playerCollisionHandler)
    {
        Debug.Log("You're colliding!");
        
    }
}
