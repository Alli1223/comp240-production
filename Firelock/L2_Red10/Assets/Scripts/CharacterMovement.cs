using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CharacterMovement : NetworkBehaviour
{
    //Author: Kate Georgiou 25/10/16 ***NEW IMPROVED CHARACTER MOVEMENT*** 
    // Use this for initialization

    [SerializeField]
    CharacterController control;
    [SerializeField]
    float playerjumpSpeed;
    private bool canMove = true;
    [SerializeField]
    public float gravity;
    [SerializeField]
    private GameObject fpsCamera;
    private Vector3 direction = Vector3.zero;
    private Vector3 lastPosition = Vector3.zero;
    UnitInfo UI;

    // Stores the players colour
    public static Color playerColour;


    void Start()
    {
        control = GetComponent<CharacterController>();
        UI = GetComponent<UnitInfo>();
        GetComponent<MeshRenderer>().material.color = playerColour;
    }

    void FixedUpdate()
    {
        // if (UnitManager.inst.GetSelectedUnit() == this.gameObject)
        //  {
        canMove = true;

    }
    //  else canMove = false; }

    // Update is called once per frame
    void Update()
    {
        if (UI.GetInfo("energy") > 0)
        {
            if (control.isGrounded /* && canMove == true*/)
            {
                //direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                direction = new Vector3(0, 0, 0);

                if (Input.GetKey(KeyCode.W))
                {
                    direction = transform.TransformDirection(fpsCamera.transform.forward);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    direction = -transform.TransformDirection(fpsCamera.transform.forward);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    direction = transform.TransformDirection(fpsCamera.transform.right);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    direction = -transform.TransformDirection(fpsCamera.transform.right);
                }

                direction *= GetComponent<UnitInfo>().GetInfo("speed");

                if (Input.GetButton("Jump"))
                {
                    direction.y = playerjumpSpeed;
                }


            }
            direction.y -= gravity * Time.deltaTime;
            control.Move(direction * Time.deltaTime);

            if (lastPosition != gameObject.transform.position)
            {
                GetComponent<UnitInfo>().RemoveEnergy();
            }
            lastPosition = gameObject.transform.position;
        }
    }
}
