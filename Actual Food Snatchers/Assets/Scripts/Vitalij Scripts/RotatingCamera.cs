
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    //Assign a target GameObject in the Inspector to rotate around
    public GameObject target;

    //Assign a number in the Inspector for the amount of degrees
    public float degree;

    //Spped up a camera depending on the way it spins
    public void SpeedUpCam()
    {
        if (degree == -20)
        {
            degree = -60;
        }
        if (degree == 20)
        {
            degree = 60;
        }
    }
    //Revert a camera speed to normal depending on the way it spins
    public void SlowDownCam()
    {
        if (degree == -60)
        {
            degree = -20;
        }
        if (degree == 60)
        {
            degree = 20;
        }
    }

    //Assign a number for camera to spin clockwise
    public void SpinRightCam()
    {
        degree = -20;
    }

    //Assign a number for camera to spin counter-clockwise
    public void SpinLeftCam()
    {
        degree = 20;
    }

    public void Update()
    {
        //Press a button to spin the camera counter-clockwise
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SpinLeftCam();
        }
        //Press a button to spin the camera clockwise
        if (Input.GetKey(KeyCode.LeftShift))
        {
            SpeedUpCam();
        }
        //Release a button to revert camera speed to normal
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SlowDownCam();
        }
        //Press a button to speed up camera
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SpinRightCam();
        }

        // Spin the camera around the target at defined in the Inspector degrees/second.
        transform.RotateAround(target.transform.position, Vector3.up, degree * Time.deltaTime);
    }


}
