using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    //Assign a target GameObject in the Inspector to rotate around
    public GameObject target;

    //Assign a number in the Inspector for the amount of degrees
    public float degree;

    // Update is called once per frame
    void Update()
    {
       // Spin the camera around the target at defined in the Inspector degrees/second.
        transform.RotateAround(target.transform.position, Vector3.up, degree * Time.deltaTime); 
    }
}
