//-------------------------------------------------------------------------------------------------------
// Author : Ross Perry 
// Date : 01 / 10 / 2016  
// Description : Provides customised functionality to spawn players in and interacts with other managers
// Features functions to carry out server/client checks and then spawn them at the relevent times
// Primarily created to fix the players auto spawning before the player had clicked to place them
//-------------------------------------------------------------------------------------------------------


using UnityEngine;
using UnityEngine.Networking;
//using UnityEditor; //Namespace to enable editor functionality (editor popups)
using System.Collections;

public class SpawnManager : NetworkManager //Enables creation of a manual network manager and access to network manager functionality
{

    //Defines the script as a singleton
    public static SpawnManager instance;

    private bool doOnce = false; //Prevent OnServerConnect being called twice
    public bool serverConnected, clientConnected = false;

    //Error checking
    private string title, output;

    [SerializeField] private GameObject[] Units; 
    private short playerId = 0;



    //Preliminary checks before connecting to the server/client
    void Awake()
    {
        //EditorUtility.DisplayDialog(title, output, "Have changes been applied and a client built?"); // Handles the reminder to build everytime for client testing
        if (instance != null) 
        {
            Destroy(this);
        }
        else 
        {
            instance = this;
        }

    }
        
    //Called when the server connection is first established
    public override void OnServerConnect(NetworkConnection conn) //To modify any of the original network manager behaviour we need to override it
    {

            OnServerAddPlayer(conn, playerId);
            serverConnected = true;
        
       
    }

    public void PlaceUnits() //When player clicks to place unit, this method is called
    {
        NetworkServer.SpawnObjects(); //re-enables all objects with a network identity component
        client = new NetworkClient(); //Creates a new network client connection

        //Tell the server to spawn a player and send the message to the OnServerAddPlayer function
        ClientScene.AddPlayer(client.connection, playerId);
    }

    public override void OnStartClient(NetworkClient client) //When the client is initialised
    {
        base.OnStartClient (client); //Handles calling the base behaviour for this particular function
        foreach (GameObject unit in Units)
        {
            ClientScene.RegisterPrefab(unit); //Handles registering the clients units so they can be spawned
        }
            
    }
        
    //Called when the server connection is first established
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        clientConnected = true;
    }
        
    //Used in conjunction with "AddPlayerForConnection" to spawn a localhost into the game
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        //GameObject player = (GameObject)Instantiate(Units[playerId%2], GetStartPosition().transform.position, Quaternion.identity);
        GameObject newPlayer = GameObject.Instantiate(Units[playerId]); //Check the array and spawn the correct player
        newPlayer.transform.position = GetStartPosition().transform.position + Vector3.right * playerControllerId; 
        NetworkServer.AddPlayerForConnection(conn, Units[playerId], playerId);
    }

    //Called once the scene has been loaded on the clients
    public override void OnClientSceneChanged(NetworkConnection conn) 
    {
        //Always become ready
        ClientScene.Ready(conn);
        //Tell the server to spawn a player if there isn't one in the scene
        ClientScene.AddPlayer(client.connection, playerId);
    }
        
    //Removes the client when it disconnects to prevent errors (server is handled automatically)
    public void OnClientDisconnect(NetworkConnection conn, short playerControllerId)
    {
        client.Shutdown();
        SpawnManager.instance.StopClient();
        Network.Disconnect();
    }
        
       
 
}
