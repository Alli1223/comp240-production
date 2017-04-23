using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
//desc: camera movement for first person controller.
//This script needs to be attached to the camera object, however it is poorly optimised right now and I need to clamp the directions in which the camera can turn (for now just pause the game to exit without it going crazy)
//You can change the sensitivity in the editor.
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
  
    public float sensX = 15F;
    
    public float sensY = 15F;
    public bool allowedToMove = true;
    public static CameraMovement instance;
    public float currentRot;
  //  public float minYRotation = -60f, maxYRotation = 90f;
   

    float rotY = 0F;

    void Awake()
    {
        instance = this;

    }

    void Update()
    {
       // currentRot -= Input.GetAxis("Mouse Y");
      
      //  transform.rotation = Quaternion.identity * Quaternion.AngleAxis(currentRot, transform.up);
       // currentRot = Mathf.Clamp(currentRot, -60, 90);
      


        if (allowedToMove == true)
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensX;



                rotY += Input.GetAxis("Mouse Y") * sensY;


                transform.localEulerAngles = new Vector3(-rotY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensX, 0);
                //cam.transform.localEulerAngles = new Vector3(Mathf.Clamp(cam.transform.localEulerAngles.x, -60, 60), 0, 0);
            }
            else
            {
                rotY += Input.GetAxis("Mouse Y") * sensY;


                transform.localEulerAngles = new Vector3(-rotY, transform.localEulerAngles.y, 0);
            }
          
        }
       

    }
}

