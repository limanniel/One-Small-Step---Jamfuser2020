using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public static bool startBool = true;
    public GameObject startGame;

    private void Update()
    {
        if(startBool == true)
        {
            startGame.SetActive(true);
        }

        if (Input.anyKeyDown)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        startGame.SetActive(false);
        Time.timeScale = 1f;
        startBool = false;
    }

}