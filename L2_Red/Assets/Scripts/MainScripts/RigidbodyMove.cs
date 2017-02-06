using UnityEngine;
using System.Collections;

public class RigidbodyMove : MonoBehaviour
{

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float maxSpeed = 20, speed = 5;
    private float xAxis, yAxis;


    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //returns the direction the player is facing
        move = transform.TransformDirection(move);
        move = move.normalized;
        //changes speed of character based on input
        move *= speed;

        //checks the difference between the current speed and wanted speed
        Vector3 velocityDifference = (move - rb.velocity);
        //makes sure that the force applied does not go higher than wanted
        velocityDifference.x = Mathf.Clamp(velocityDifference.x, -maxSpeed, maxSpeed);
        //makes sure that the force applied does not go higher than wanted
        velocityDifference.z = Mathf.Clamp(velocityDifference.z, -maxSpeed, maxSpeed);
        //makes sure that gravity will still apply to the character
        velocityDifference.y = 0;
        Debug.Log(velocityDifference);
        //sets the speed of the character and makes sure he does not move faster diagonally
        rb.AddForce(velocityDifference, ForceMode.VelocityChange);
       
       // if (Input.GetAxis("Horizontal") < 0)
       // {
        //    rb.AddForce(transform.forward * speed);
       // }


    }
}
