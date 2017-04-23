//------------------------------------------------------------------------------------
// Author : Ross Perry 
// Date : 17 / 10 / 2016  
// Description : Setup to fix players mirroring movement / repeating actions
// Features functionality to check an array of scripts and disable them on the clients
//-------------------------------------------------------------------------------------


using UnityEngine;
using System.Collections;
using UnityEngine.Networking; 

public class Setup : NetworkBehaviour //Initialises network functionality
{
    [SerializeField]
    Behaviour[] disableComponent; //Defines an array of behaviours e.g. scripts
    [SerializeField]
    private Camera lobbyCamera;

	void Start () 
    {
        if (!isLocalPlayer) //Handles disabling scripts if we're not the local host and so fixing duplicated movement/shooting etc
        {
            for (int i = 0; i < disableComponent.Length; i++)
            {
                disableComponent[i].enabled = false;
            }
        } 
        else
        {
            lobbyCamera = Camera.main; //Handles setting the scene camera if we're not the local host
            if (lobbyCamera != null)
            {
                lobbyCamera.gameObject.SetActive(false); 
            }

        }
	}

    void OnDisable() //Checks if behaviour has been disabled and loads the scene camera
    {
        if (lobbyCamera != null)
        {
            lobbyCamera.gameObject.SetActive(true); 
        }
    }
	

}
