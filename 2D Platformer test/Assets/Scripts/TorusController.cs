using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusController : MonoBehaviour
{

    public Vector3 RotateDirection;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateDirection * Time.deltaTime);
    }

}