using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{

    public Transform[] backgrounds; //Array of all the back and foregrounds to parallaxed
    private float[] parallaxScales; //Proportion of camera's movement to move the backgrounds by
    public float smoothing = 1f;         //Smoothing of the parallax. Make sure to set above 0 or parallax wont work

    private Transform cam;          //reference to main cameras transform
    private Vector3 previousCamPos; //position of camera in previous frame

    //called before Start(). Great for references.
    private void Awake()
    {
        //set up camera reference
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        //previous frame had the current frame's camera
        previousCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //looping through backgrounds
       for(int i = 0; i < backgrounds.Length; i++)
        {
            //parallax is opposite of the camera movement because previous frame multiplied by the scale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            //set a target x position which is the current position plus the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //create target position which is backgrounds current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current position and target position
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //set previous camera position to camera's position at the end of the frame
        previousCamPos = cam.position;

    }
}
