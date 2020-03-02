using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideParticleScript : MonoBehaviour
{
    public ParticleSystem particleLauncher;

    public float feetPosY;
    public float feetPosX;

    // Start is called before the first frame update
    void Start()
    {
        particleLauncher.Stop();
        var dur = particleLauncher.main;
        dur.duration = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerController>().isWallSliding)
        {
            Vector3 playerFeetPos = FindObjectOfType<PlayerController>().transform.position;
            playerFeetPos.y += feetPosY;
            playerFeetPos.x += feetPosX;
            particleLauncher.transform.position = playerFeetPos;
            if (particleLauncher.isStopped)
            {
                particleLauncher.Play();
            }
            particleLauncher.Emit(1);
        }    
    }
}
