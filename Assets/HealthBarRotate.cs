using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarRotate : MonoBehaviour
{
    Transform cameraTransform;
    void Start()
    {
        cameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = cameraTransform.rotation;
    }
}
