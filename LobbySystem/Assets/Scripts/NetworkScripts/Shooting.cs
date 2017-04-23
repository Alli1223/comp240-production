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
   
    [SerializeField]
    private float bulletForce = 10f;
    private float destroyTimer = 3.5f;

    void Start()
    {
        this.enabled = false;
    }

    void Update()
    {
        if (!isLocalPlayer) //Prevent duplication across server/client when firing
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) //Handles calling the function to shoot 
        {
            CmdShooting();
        }

    }

    //  [Command] //This attribute specifies that a function will be called on the server from the client (Attribute to be added when applicable for networking)
    public void CmdShooting()
    {
        
        if (UnitManager.inst.unitSelected == this.gameObject)
        {
            var bulletClone = (GameObject)Instantiate(bullet, bulletOrigin.transform.position, transform.rotation); //Manages the spawning of the clone
            bulletClone.GetComponent<BulletCollision>().playerSpawnedFrom = this.gameObject;
            bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * bulletForce; //Functionality to make the clone move forward

            NetworkServer.Spawn(bulletClone.gameObject); //Spawns a network aware (able to be seen across the network) prefab

            if (bulletClone.gameObject != null) //Handles the check to only destroy the cloned bullets which have been spawned
            {
                Destroy(bulletClone.gameObject, destroyTimer); //Cleanup of the network aware clones
            }

        }
        else
        {
            return;
        }

    }


}