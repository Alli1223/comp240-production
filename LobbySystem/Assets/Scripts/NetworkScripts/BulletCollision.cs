//-------------------------------------------------------------------------------
// Author : Ross Perry 
// Date : 17 / 10 / 2016  
// Description : Manages bullet collisions and the effects
// Features functions to check for collisons and to lower health where relevant
//-------------------------------------------------------------------------------


using UnityEngine;
using UnityEngine.Networking; //Namespace to give access to NetworkBehavior
using System.Collections;

public class BulletCollision : NetworkBehaviour //Derives from network behaviour
{
    [SyncVar] //This attribute synchronises the variable over server/clients
    public float damage = 10f;
    public GameObject playerSpawnedFrom;


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") //Handles collision detection.
        {
            if (col.gameObject != playerSpawnedFrom) 
            {
                DamagePlayer(col.gameObject); //calls the method to damage the object the bullet collides with.
                Destroy(this.gameObject, 0.1f);
            }
        }

    }

    void DamagePlayer(GameObject hitObject) //Handles updating the players health from the damage done
    {
        hitObject.GetComponent<HealthManager>().playerHealth -= damage; //Removes health from the hit players health stat located in the health manager
    }


}
