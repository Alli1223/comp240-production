using UnityEngine;
using System.Collections;

public class CameraRotate : MonoBehaviour {

    //AUTHOR: KATE GEORGIOU 13/10/16 DESC: ROTATES THE CAMERA WHILE IN TACTICAL VIEW.
    //ATTACH TO THE CAMERA THAT YOU WANT TO BE USED IN THE TACTICAL VIEW.

    private Camera cam;
    
    public float speed;
    public static CameraRotate inst;
    public bool movementAllowed = true;

    void Start()
    {
        inst = this;
        cam = this.gameObject.GetComponent<Camera>();
    }
    void Update()
    {
        //map to buttons
        if (Input.GetKey(KeyCode.Q) && movementAllowed == true)
        {
            this.gameObject.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E) && movementAllowed == true)
        {
            this.gameObject.transform.Rotate(Vector3.forward * -speed * Time.deltaTime);
        }
    }
}
