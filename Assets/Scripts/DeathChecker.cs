using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathChecker : MonoBehaviour
{
    [SerializeField]
    public GameObject[] DeathTextCanvas;
    [SerializeField]
    private Sprite[]    DeathSprite;
    private Animator    animator;
    int                 enumNumber;
     public ParticleSystem particleEffects;

    enum CHARACTERDEATH
    {
        NOTDEAD = 0,
        ACIDLASER,
        KARS,
        BLACKHOLE
    }
    
    CHARACTERDEATH characterDeath = CHARACTERDEATH.NOTDEAD;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //particleEffects.Pause();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GonDie")
        {
            characterDeath = CHARACTERDEATH.ACIDLASER;
            enumNumber = (int)characterDeath;
            animator.SetInteger("currentLifeState", enumNumber);
            //particleEffects.transform.position = transform.position;
           // particleEffects.Play();
        }
        
    }

    private void CharacterChecker()
    {
        if (characterDeath != CHARACTERDEATH.NOTDEAD)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("Level1");
            }
        }
    }

    private void Update()
    {
        CharacterChecker();
        enumNumber = (int)characterDeath;

        for (int i = 1; i < 7; i++)
        {
            if (GetComponent<Transform>().position.y < -500 * i)
            {
                DeathTextCanvas[i - 1].gameObject.SetActive(true);
                characterDeath = CHARACTERDEATH.KARS;
                enumNumber = (int)characterDeath;
            }
        }
    }
}
