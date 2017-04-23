//---------------------------------------------------------------------------
// Author : Ross Perry 
// Date : 17 / 10 / 2016  
// Description : Gives functionality for the player to shoot over the network
// Features function to shoot
//---------------------------------------------------------------------------


using UnityEngine;
using UnityEngine.Networking; //Namespace to give access to NetworkBehavior
using System.Collections;

public class Shooting : NetworkBehaviour //Adds access to network functionality e.g. "IsLocalPlayer"
{
    //Handles defining of the bullet related variables
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform bulletOrigin;

    private Camera firstPerson;

    [SerializeField]
    private float bulletForce = 30f;
    private float destroyTimer = 6.5f;

    void Start()
    {
        firstPerson = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Handles calling the function to shoot 
        {
            CmdShooting();
        }
       
    }

    [Command] //This attribute specifies that a function will be called on the server from the client
    public void CmdShooting()
    { 
        var bulletClone = (GameObject)Instantiate(bullet, bulletOrigin.transform.position, transform.rotation); //Manages the spawning of the clone
        bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce; //Functionality to make the clone move forward

        NetworkServer.Spawn(bulletClone.gameObject); //Spawns a network aware (able to be seen across the network) prefab
        if (bulletClone.gameObject != null) //Handles the check to only destroy the cloned bullets which have been spawned
        {
            Destroy(bulletClone.gameObject, destroyTimer); //Cleanup of the network aware clones
        } 
    }
               
        
}