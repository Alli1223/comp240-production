//------------------------------------------------------------------------------------------------------------------
// Author : Ross Perry 
// Date : 19 / 10 / 2016  
// Description : Manages the both the servers and clients health
// Features a singleton to make the healh accessible to the bulletcollision script and to allow for script expansion
//-------------------------------------------------------------------------------------------------------------------


using UnityEngine;
using UnityEngine.SceneManagement; //new namespace to allow scene loading
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class HealthManager : NetworkBehaviour //Adds functionality for Unet
{

    //Defines the script as a singleton
    public static HealthManager instance;

    [SyncVar] //This attribute synchronises the health variable over server/clients
    public float playerHealth = 100f;
    [SerializeField]
    private Image healthBar;

        
    //Prevents problems if more than one instance of this singleton is used
    void Awake ()
    {
        if (instance != null) 
        {
            Destroy(this);
        }
        else 
        {
            instance = this;
        }

    }
        
	void Update()
    {
        if (playerHealth <= 0f)
        {
            Destroy(this.gameObject);
        } 
    }
}
