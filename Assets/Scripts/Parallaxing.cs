using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{

    public Transform[] backgrounds;     //Array (list) of all the back- and foregrounds to be parallaxed
    private float[] parallaxScales;     //The proportion of the camera's movement to mover the background by
    public float smoothing=1f;          //How smooth the parallax is going to be. Make sure  to set this above 0

    private Transform cam;              //reference to main camera's transform
    private Vector3 previousCamPos;     //the position of the camera in the previous frame

    // Is called before Start() --> Great for references
    void Awake()
    {
        //set up camera the reference
        cam = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        //The previous frame had the current frame's camera position
        previousCamPos = cam.position;
        //Make sure the pallaxScales Array is as long as the backgrounds Array
        parallaxScales = new float[backgrounds.Length];

        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z*-1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //for each background
        for(int i = 0; i < backgrounds.Length; i++)
        {
            //the parallax is the oposite of  the camera movemtn because of the previous frame multiplied by the parallaxscale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            //set a target x position which is the current position + the parallax 
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // create a target postion which is the background's current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade between current postion and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        //set the previousCamPos to the camera's postion at the end of the frame
        previousCamPos = cam.position;
    }
}
