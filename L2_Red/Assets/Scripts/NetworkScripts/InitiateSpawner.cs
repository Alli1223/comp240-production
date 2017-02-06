using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class InitiateSpawner : NetworkManager
{
    private bool doOnce = false, serverConnected, clientConnected; //Prevent OnServerConnect being called twice
    private short playerId = 0;

    //When LocalHost is clicked
    public override void OnServerConnect(NetworkConnection conn) //To modify any of the original network manager behaviour we need to override it
    {

        if (doOnce == false)
        {
            doOnce = true;
            OnServerAddPlayer(conn, playerId);
            serverConnected = true;
        }

    }

    //When Client is clicked
    //Called when the server connection is first established
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        clientConnected = true;
    }


//    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
//    {
//        base.OnServerAddPlayer(NetworkConnection conn, short playerControllerId);
//        //Spawn empty unit controller
//    }

}
