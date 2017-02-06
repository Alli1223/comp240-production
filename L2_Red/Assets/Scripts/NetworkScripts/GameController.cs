//--------------------------------------------------------------------------------------
// Author : Ross Perry 
// Date : 25 / 10 / 2016  
// Description : Will be a script to send prompts to the clients
// Features a function to check if the server and client has connected and send a prompt
//----------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    //Defines the script as a singleton
    public static GameController instance;

    void Awake()
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

    void CheckForConnections()
    {
        if (SpawnManager.instance.serverConnected == true)
        {
            if (SpawnManager.instance.clientConnected == true)
            {
                //Send message to clients notifying them to click to place units in the their designated area
            }
            
        }

    }

}  
