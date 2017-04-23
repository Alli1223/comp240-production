//-----------------------------------------------------------------------------------------------
// Author : Ross Perry 
// Date : 01 / 10 / 2016  
// Description : Provides customised functionality to spawn players in
// Features functions to carry out server/client checks and then spawn them at the relevent times 
//------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Networking;

using System.Collections;

public class SpawnManager : NetworkManager
{
    private NetworkManager netManager;

    [SerializeField]
    private GameObject player1Spawn;

    //Error checking
    private string title;
    private string output;

    [SerializeField]
    private Camera playerCam;

    //Preliminary checks before connecting to the server/client
    void Awake()
    {
        //EditorUtility.DisplayDialog(title, output, "Have changes been applied and a client built?");
    }

    //Called when the "local host" is clicked
    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("Server Connected");
        OnServerAddPlayer(conn, -1);
    }

    //Called when "client" is clicked
    public void OnClientConnect()
    {
        Debug.Log("Client Connected");
        //Spawn Client prefab
        OnClientAddPlayer();
    }

    //Spawn player on server
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GetStartPosition();

        GameObject player = (GameObject)Instantiate(playerPrefab, player1Spawn.transform.position, Quaternion.identity);
        //playerCam.enabled = false;

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
        
    public void OnClientAddPlayer()
    {

    }

    public virtual void OnClientSceneChanged(NetworkConnection conn)
    {
        // always become ready.
        ClientScene.Ready(conn);

        //Add Client prefab
        ClientScene.AddPlayer(client.connection, 0);
    }


    public void OnStartLocalPlayer()
    {
        //When the server connects do something
    }

}
