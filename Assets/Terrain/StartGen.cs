using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGen : MonoBehaviour
{
    public int numberOfAsteroids;
    public float minYPosition, maxYPosition;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("asteroid"));

            Vector3 randomPos = new Vector3(Random.Range(-10, 10), Random.Range(minYPosition, maxYPosition), 0);
            obj.transform.SetPositionAndRotation(randomPos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}