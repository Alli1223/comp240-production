using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

    //AUTHOR: KATE GEORGIOU 13/10/16 DESC: ROTATES THE CAMERA WHILE IN TACTICAL VIEW.
    //ATTACH TO THE CAMERA THAT YOU WANT TO BE USED IN THE TACTICAL VIEW.

    private Camera cam;
    
    public float speed; //public variables used in other classes (refrenced by being a singleton)
    public static CameraRotate inst; //make this class an singleton
    public bool movementAllowed = true; //public variables used in other classes (refrenced by being a singleton)

    void Start()
    {
        inst = this; //initialises the singleton.
        cam = this.gameObject.GetComponent<Camera>(); // cam = this camera component.
    }
    void Update()
    {
      
        if (Input.GetKey(KeyCode.Q) && movementAllowed == true) //if u press Q and movement is allowed....
        {
            this.gameObject.transform.Rotate(Vector3.forward * speed * Time.deltaTime); //...rotate this gameobject.
        }
        if (Input.GetKey(KeyCode.E) && movementAllowed == true) //if u press E and movement is allowed....
        {
            this.gameObject.transform.Rotate(Vector3.forward * -speed * Time.deltaTime); //...rotate this gameobject in the other direction.
        }
    }
}
