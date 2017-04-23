using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CharacterMovement : NetworkBehaviour
{
    //Author: Kate Georgiou 25/10/16 ***NEW IMPROVED CHARACTER MOVEMENT*** 
    // Use this for initialization

    [SerializeField]
    CharacterController control;
    //[SerializeField]
    // float playerjumpSpeed;
    private bool canMove = true;
    [SerializeField]
    public float gravity;
    [SerializeField]
    private GameObject fpsCamera;
    private Vector3 direction = Vector3.zero;
    private Vector3 lastPosition = Vector3.zero;
    public GameObject dock;
    private float playerX;
    private float camX;
    [SerializeField]
    private float speed;
    private Vector3 zeroDirection = Vector3.zero;
    //private AudioSource audio;
    public float sensX = 15F, sensY = 15f;
    [SerializeField]
    private AudioClip walk;
    public GameObject lookAtCube;

    UnitInfo UI;

    // Stores the players colour
    public static Color playerColour;
    [SerializeField]
    private Behaviour movement;

    void Start()
    {
        control = GetComponent<CharacterController>();
        UI = GetComponent<UnitInfo>();
        GetComponent<MeshRenderer>().material.color = playerColour;
        movement.enabled = false;
        //GetComponent<AudioSource>().clip = walk;
    }

    void FixedUpdate()
    {
        if (UnitManager.inst.GetSelectedUnit() == this.gameObject)
        {
            canMove = true;

        }

        else canMove = false;
    }

    // Update is called once per frame
    void Update()
    {



        if (UI.GetInfo("energy") > 0)
        {
            if (isLocalPlayer && control.isGrounded && canMove == true)
            {
                
                //  if (Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Horizontal") < -0.1f || Input.GetAxis("Vertical") > 0.1f || Input.GetAxis("Vertical") < -0.1f)
                // {
                direction = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
                //}
                //this.gameObject.transform.rotation = fpsCamera.transform.rotation;    TRYING TO FIX ROATATION ISSUES 
                //direction += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                //direction = new Vector3(0, 0, 0);
               playerX = this.gameObject.transform.localRotation.x;
                //camX = fpsCamera.transform.localRotation.x;
                //playerX = camX;                                                             TRYING TO FIX ROATATION ISSUES 
                //this.gameObject.transform.rotation = new Quaternion(camX, 0, 0, 0);
           
                if (Input.GetKey(KeyCode.W))
                {
                    
                   
                    zeroDirection = transform.TransformDirection(transform.forward * speed * Time.deltaTime);
                    // this.gameObject.transform.Rotate(0, Input.GetAxis("Horizontal") * speed, 0);
                    // controllerBase = this.gameObject.transform.TransformDirection(Vector3.forward);    TRYING TO FIX ROATATION ISSUES 
                
                }
                if (Input.GetKey(KeyCode.S))
                {
                    
                  
                    zeroDirection = -transform.TransformDirection(transform.forward * speed * Time.deltaTime);

                    //  this.gameObject.transform.Rotate(0, Input.GetAxis("Horizontal") * speed, 0);
                    //controllerBase = this.gameObject.transform.TransformDirection(Vector3.forward);     TRYING TO FIX ROATATION ISSUES 
               

                }
                if (Input.GetKey(KeyCode.D))
                {
                   
                
                    zeroDirection = transform.TransformDirection(transform.forward * speed * Time.deltaTime);

                    // this.gameObject.transform.Rotate(0, Input.GetAxis("Horizontal") * speed, 0);      TRYING TO FIX ROATATION ISSUES 
                    //controllerBase = this.gameObject.transform.TransformDirection(Vector3.forward);
                   

                }
                if (Input.GetKey(KeyCode.A))
                {
                    zeroDirection = -transform.TransformDirection(transform.forward * speed * Time.deltaTime);

                    // this.gameObject.transform.Rotate(0, Input.GetAxis("Horizontal") * speed, 0);     TRYING TO FIX ROATATION ISSUES 
                    //controllerBase = this.gameObject.transform.TransformDirection(Vector3.forward);
                  

                }

                zeroDirection *= GetComponent<UnitInfo>().GetInfo("speed");

                //   if (Input.GetButton("Jump"))
                //   {
                //      direction.y = playerjumpSpeed;   REMOVED JUMP FUNCTION
                //  }

                if (Input.GetAxis("Mouse X") != 0)
                {
                    transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y,transform.eulerAngles.z);
                }


            }
            direction.y -= gravity;
            //transform.TransformDirection(direction);
            direction *= GetComponent<UnitInfo>().GetInfo("speed");
            control.Move(direction * Time.deltaTime);

            if (lastPosition != gameObject.transform.position)
            {                

                GetComponent<UnitInfo>().RemoveEnergy();
            }
            lastPosition = gameObject.transform.position;
        }
    }

}
