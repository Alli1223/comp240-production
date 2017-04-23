//-------------------------------------------------------------------------------
// Author : Ross Perry 
// Date : 17 / 10 / 2016  
// Description : Manages bullet collisions and the effects
// Features functions to check for collisons and to lower health where relevant
//-------------------------------------------------------------------------------


using UnityEngine;
using UnityEngine.Networking; //Namespace to give access to NetworkBehavior
using System.Collections;

public class BulletCollision : NetworkBehaviour
{
    [SyncVar] //This attribute synchronises the variable over server/clients
    public float damage = 10f;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") //Handles collision detection
        {
            DamagePlayer();
        }

    }
        
    void DamagePlayer()
    {
        HealthManager.instance.playerHealth -= damage; //Handles updating the players health from the damage done
    }


}
