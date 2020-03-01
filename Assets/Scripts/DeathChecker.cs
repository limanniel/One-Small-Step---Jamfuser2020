using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathChecker : MonoBehaviour
{
    [SerializeField]
    public GameObject[] DeathTextCanvas;

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
        if (characterDeath != CHARACTERDEATH.NOTDEAD)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("PauseTest");
            }
        }
    }

    private void Update()
    {
        CharacterChecker();

        for (int i = 1; i < 7; i++)
        {
            if (GetComponent<Transform>().position.y < -500*i)
            {
                DeathTextCanvas[i-1].gameObject.SetActive(true);
                characterDeath = CHARACTERDEATH.KARS;
            }
        }
    }
    
}
