using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiraEasterEgg : MonoBehaviour
{
    private GameObject kira;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private GameObject KillerQueen;

    [SerializeField]
    private GameObject[] particleEffects;

    [SerializeField]
    private GameObject[] copyPasta;
    float timeLeft = 64f;
    bool collisionTrue = false;

    void Start()
    {
        kira = gameObject.transform.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        target.GetComponent<PlayerController>().stopMovement = true;
        target.GetComponent<PlayerController>().canJump = false;

        collisionTrue = true;
    }


    private void Update()
    {
        if(collisionTrue)
        {
            for (int i = 0; i < 12; i++)
            {
                copyPasta[i].SetActive(true);
            }
        }
    }
}
