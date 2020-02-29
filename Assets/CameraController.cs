using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject targetObject;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - targetObject.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = targetObject.transform.position + offset;
    }

}
