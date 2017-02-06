using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UnitPlacement : NetworkBehaviour
     //Author: Kate Georgiou & Ross Perry 10/11/16
{   //needs to keep track of how many players have been instantiated so that it knows how many to spawn when ready up button is pressed.

    public static bool allUnitsPlaced = false;
    public bool canPlace = true;
    private bool p1CanPlace;
    private bool p2CanPlace;

    [SerializeField]
    private GameObject player1UnitsToPlace, player2UnitsToPlace;
    [SerializeField]
    private GameObject[] spawnpoints1, spawnpoints2;

    // Author Ross /*
    [SyncVar(hook = "UpdatePlayerID")] public int playerID = 0; //Sync playerID across clients and call UpdatePlayerID method when this happens
    [SyncVar] public int remainingSpawns = 3; // Amount Of Unit Spawns, 'SyncVar' replicates it to the client
    // */

    [SerializeField]
    private int p1Counter = 0; //Variables to keep track of the units placed for each player
    private int p2Counter = 0;

    private Ray mouse;
    private RaycastHit click;

    public static UnitPlacement inst;

    void Awake()
    {
        inst = this;
    }
        
    void Start()
    {
        spawnpoints1 = GameObject.FindGameObjectsWithTag("spawnpoints1"); // add anything tagged as spawnpoints 1 to the list for spanwpoints 1. 
        spawnpoints2 = GameObject.FindGameObjectsWithTag("spawnpoints2"); // add anything tagged as spawnpoints 2 to the list for spanwpoints 2. 
        p1CanPlace = true;
        p2CanPlace = true;
    }

    public override void OnStartLocalPlayer()
    {
        CmdRegisterPlayerController(); //Handles calling the function so that the player ID is updated on the server
    }
        

    //Author Ross Perry /*
    void Update()
    {
        if (isLocalPlayer)
        {
            PlaceUnits(); 
        }

        if (remainingSpawns <= 1)
        {
            canPlace = false;
        }
    }
        
    void UpdatePlayerID(int _playerID) //Hook function to change the player name and ID
    {
        playerID = _playerID;
        transform.name = "Player : " + playerID;
    }

    [Command] //Invoke this on the server
    void CmdRegisterPlayerController()
    {
        UpdatePlayerID(playerID); //Handles updating the player name/ID across the server
    }

    void PlaceUnits()
    {
        mouse = Camera.main.ScreenPointToRay(Input.mousePosition); //Can use the main camera singleton as a reference because we only have one camera to switch

        if (Physics.Raycast(mouse, out click) && canPlace == true)
        {
            Vector3 spawnPosition = click.point;
            Quaternion spawnRotation = this.gameObject.transform.rotation;

            if (Input.GetMouseButtonUp(0))
            {
                
                if (click.collider.gameObject.tag == "Player1Area") //Handles spawning of the correct player depending on where they click
                {
                    ReadyUp.inst.gameObject.SetActive(false); //Prevents players auto spawning if units have already been placed
                    CmdSpawnUnit(spawnPosition, spawnRotation);
                    p1Counter++; //Increases the relevant counter as units are placed
                }
                if (click.collider.gameObject.tag == "PLayer2Area") //have the floor made of several different components that have different tags so if the player clicks on their area their unit is put down?
                {
                    ReadyUp.inst.gameObject.SetActive(false);
                    CmdSpawnUnit(spawnPosition, spawnRotation);
                    p2Counter++;
                }

            }


            if (Input.GetMouseButtonUp(2)) //Handles the toggle for placement mode
            {
                if (click.collider.gameObject.tag == "Player")
                {
                    canPlace = false;
                    click.collider.gameObject.GetComponent<Placement>().isInPlacementMode = true;
                }
                if (click.collider.gameObject.tag == "Player")
                {
                    canPlace = false;
                    click.collider.gameObject.GetComponent<Placement>().isInPlacementMode = true;
                }
            }

        }

        // */

        if (p1Counter <= 2 && ReadyUp.inst.countDown <= 0)
        {
            int x = 0;
            foreach (GameObject SP in spawnpoints1)
            {

                if (p1Counter <= 2)
                {
                    Instantiate(player1UnitsToPlace, spawnpoints1[x].transform.position, Quaternion.identity);
                    p1Counter++;
                    x++;
                }
                else
                {
                    break;
                }

            }
        }
        if (p2Counter <= 2 && ReadyUp.inst.countDown <= 0) //if p2 counter is less than 3 then allow placmenent.
        {
            int x = 0;
            foreach (GameObject SP in spawnpoints2)
            {

                if (p2Counter <= 2)
                {
                    Instantiate(player2UnitsToPlace, spawnpoints2[x].transform.position, Quaternion.identity); //instnatiate the unit. 
                    p2Counter++;
                    x++;
                }
                else
                {
                    break;
                }


            }
                
        }

    }

    // Author Ross /*
    [Command]
    void CmdSpawnUnit(Vector3 spawnPosition, Quaternion spawnRotation) //Handles spawning of the new player unit so that it's visible across the network
    {
        GameObject newUnit = (GameObject) GameObject.Instantiate(player1UnitsToPlace, spawnPosition, spawnRotation);
        NetworkServer.Spawn(newUnit); //Spawn network aware unit
        remainingSpawns--; //Prevents infinite unit spawning
        playerID++;
    }
    // */

    public Vector3 GetClickHit()
    {
        return click.point;
    }
    public void CanPlace(bool setBool)
    {
        if (setBool == true)
        {
            canPlace = true;
        }
        else
        {
            canPlace = false;
        }

    }

}
