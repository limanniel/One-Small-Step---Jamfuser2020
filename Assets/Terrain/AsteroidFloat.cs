using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFloat : MonoBehaviour
{
    float floatAround;
    public float floatStrength = 1;
    public float rotationStrength = 100;
    float passiveRotationStrength;

    // Start is called before the first frame update
    void Start()
    {
        floatAround = transform.position.y;

        passiveRotationStrength = Random.Range(0.1f, 0.2f);
        passiveRotationStrength *= Random.value < 0.5 ? 1 : -1;

        //JumpOff(5.0f, Vector2.right);
    }

    void FixedUpdate()
    {
        Rigidbody2D rigidBody = transform.GetComponent<Rigidbody2D>();

        if (transform.position.y < floatAround)
            rigidBody.AddForce(Vector2.up * floatStrength);
        else
            rigidBody.AddForce(Vector2.down * floatStrength);

        transform.Rotate(0, 0, passiveRotationStrength);
    }

    void JumpOff(float jumpForce, Vector2 dir)
    {
        Rigidbody2D rigidBody = transform.GetComponent<Rigidbody2D>();

        rigidBody.AddForce(-dir * jumpForce);
        transform.Rotate(0, 0, passiveRotationStrength * jumpForce);
    }
}