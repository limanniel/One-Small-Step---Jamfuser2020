using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    public GameObject playerCharacter;
    private float radius;
    private float yOffset;
    private bool inBlackhole;
    private float scaleDecrementer;


    // Start is called before the first frame update
    void Start()
    {
        radius = 0.5f;
        yOffset = 1.0f;
        inBlackhole = false;
        scaleDecrementer = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - radius < playerCharacter.transform.position.x && transform.position.x + radius > playerCharacter.transform.position.x &&
            transform.position.y + yOffset - radius < playerCharacter.transform.position.y && transform.position.y + yOffset + radius > playerCharacter.transform.position.y)
        {
            playerCharacter.transform.position = new Vector2(transform.position.x, transform.position.y + yOffset);
            inBlackhole = true;
        }

        if (inBlackhole && (playerCharacter.transform.localScale.x > 0.0f || playerCharacter.transform.localScale.y > 0.0f))
        {
            playerCharacter.transform.localScale -= new Vector3(scaleDecrementer, scaleDecrementer, 0.0f);
        }
                
    }
}
