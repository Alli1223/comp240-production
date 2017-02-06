using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; //Needed for invoke

public class Switch : NetworkBehaviour
{ //Author: Kate Georgiou & Ross Perry 17/11/16 desc: handles the switching of the players and players turns.
    public static Switch inst; //Declare the variable
    [SerializeField]
    private int unitsLeftBeforeSwitch;

    // Author Ross */
    [SyncVar(hook = "UpdateTurn")] public int playerTurn = 1; //Update the player turn across clients
    // */

    private int defaultUnitAmounts;
    [SerializeField]
    private Camera tactCam;
    [SerializeField]
    private bool TimerDone;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float time = 60f, defaultTime;
    private bool turnHad = false, timerRun = false;
    public Canvas startTurn, endTurn;
    [SerializeField]
    private Text TimerText;
   

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        tactCam.GetComponent<CameraMovement>().enabled = false; //disables camera movement on start up.
    
        defaultTime = time; //sets the time to the default time

        DebugController.instance.WriteTextLogToExistingFile("hello", GetType());
    }

    void Update()
    {
        // startTurn.enabled = true;
        endTurn.enabled = false; //activates the end turn canvas. 

        if (Input.GetKeyUp(KeyCode.Escape) && timerRun == false /*&& UnitManager.inst.unitSelected != null*/) //When pressing the esc key
        {
            Debug.Log("enable movement");
            timerRun = true;
            tactCam.GetComponent<CameraMovement>().enabled = true;
            UnitManager.inst.unitSelected.GetComponent<CharacterMovement>().enabled = true; //enables character movement. 
            //UnitManager.inst.unitSelected.GetComponent<Shooting>().enabled = true; //enables shooting. 

            ElapseASecond(); //Invoke the timer after a second
        }


        if (time <= 0) // if the timer is 0.
        {
            RpcEndTurn(playerTurn);
            UniSelect.inst.raycastAllowed = true;
            startTurn.enabled = false; //enable and disable cavnas's. 
            endTurn.enabled = true;
            TimerDone = true;
            //UnitManager.inst.unitSelected.GetComponent<Shooting>().enabled = false; //disables shooting. 
            UnitManager.inst.unitSelected.GetComponent<CharacterMovement>().enabled = false; //disables movement.

//            if (GetPlayerTurn() == 1) //if it is player 1's turn...
//            {
//                EndTurn(playerTurn:2); //run code for ending player 1's turn. 
//            }
//            if (GetPlayerTurn() == 2) //if it is player 1's turn...
//            {
//                EndTurn(playerTurn:1); //run code for ending player 1's turn. 
//            }

        }
    }

    [Command]
    void CmdRegisterTurn()
    {
        UpdateTurn(playerTurn);
    }

    // Author Ross /*
    void UpdateTurn(int _playerTurn) //Hook function to tell the clients whos turn it is
    {
        playerTurn = _playerTurn;
        Debug.Log("UpdatedTurn: " + playerTurn);
    }
    // */

    public Camera GetTacticalCamera() //Get the tactical camera without exposing variables
    {
        return tactCam;
    }

    public void DecreaseUnitsLeftBeforeSwitch() //Decrease the amount of units before switch
    {
        unitsLeftBeforeSwitch--;
    }

    public void SetUnitsLeftBeforeSwitchToDefault() //Set units left before the switch back to default for the next player
    {
        unitsLeftBeforeSwitch = defaultUnitAmounts;
    }

    public int GetPlayerTurn() //When called will return the player whos turn it is
    {
        return playerTurn;
    }

    IEnumerator delayedCode()
    {
        yield return new WaitForSeconds(0.1f); //wait for 0.1 seconds and then run the code. 

        unitsLeftBeforeSwitch = UnitManager.inst.GetNumberOfPlayerUnits(1); //Get the amount of units found in the list
        defaultUnitAmounts = unitsLeftBeforeSwitch; //set the default unit amount to the amount of units left before switch. 
    }

    private void ResetEnergyOfAllUnits() //This will loop through the contents of the players in both lists for the players owned by each player and reset the energy values
    {
        foreach (GameObject player1 in UnitManager.inst.GetContentsOfList(1)) //Get contents of the list holding all of player 1s units and reset each energy value
        {
            UnitInfo infoScript = player1.GetComponent<UnitInfo>();
            infoScript.ResetEnergy();
        }
        foreach (GameObject player2 in UnitManager.inst.GetContentsOfList(2)) //Get contents of the list holding all of player 2s units and reset each energy value
        {
            UnitInfo infoScript = player2.GetComponent<UnitInfo>();
            infoScript.ResetEnergy();
        }
    }

    [ClientRpc]
    public void RpcEndTurn(int playerTurn)
    {
        startTurn.enabled = false; // Enable/disable canvas'
        endTurn.enabled = true;

        if (TimerDone == true && Input.GetKeyDown(KeyCode.F) && GetPlayerTurn() == 1)
        {
            tactCam.GetComponent<CameraMovement>().enabled = false;  //disables camera movement.
            UnitManager.inst.unitSelected.GetComponent<CharacterMovement>().enabled = false;//disable false.
            UnitManager.inst.unitSelected = null;//set unit selected to null. 
            //UnitManager.inst.unitSelected.GetComponent<Shooting>().enabled = false; //disables shooting. 

            CameraSwitch.instance.UnPair();//unpair and repair the camera. 
            CameraSwitch.instance.PairToTact();

            if (GetPlayerTurn() == 1)//if player 1 turn switch to player 2. 
            {
                UpdateTurn(_playerTurn:2);
                //playerTurn = 2; 
                ResetTimer();
                return;
            }

        }

        else if (TimerDone == true && Input.GetKeyDown(KeyCode.F) && GetPlayerTurn() == 2)
        {
            CmdDebugTurn();
            //DebugController.instance.WriteTextLogToExistingFile("changed to 1", GetType());
            Debug.Log("UpdatedTurn: " + playerTurn);

            tactCam.GetComponent<CameraMovement>().enabled = false;  //disables camera movement.
            UnitManager.inst.unitSelected.GetComponent<CharacterMovement>().enabled = false;//disable false.
            UnitManager.inst.unitSelected = null;//set unit selected to null. 
            //UnitManager.inst.unitSelected.GetComponent<Shooting>().enabled = false; //disables shooting. 

            CameraSwitch.instance.UnPair();//unpair and repair the camera. 
            CameraSwitch.instance.PairToTact();

            if (GetPlayerTurn() == 2)//if player 2 turn switch to player 1. 
            {
                UpdateTurn(_playerTurn:1);
                //playerTurn = 1; 
                ResetTimer();
                return;
            }

        }


    }

    [Command]
    void CmdDebugTurn()
    {
        Debug.Log("UpdatedTurn: " + playerTurn);
    }
        
    void ElapseASecond() //Uses recursion but shouldn't be very expensive as it is a small method that will clear up when finished
    {
        time = time - 1;
        if (time > 0)
        {
            Invoke("ElapseASecond", 1); 
        }
    }
    public void ResetTimer() //resets the timer for the next turn. 
    {
        time = defaultTime;
        timerRun = false;
        TimerDone = false;
        UniSelect.inst.usedUnits.Add(UnitManager.inst.GetSelectedUnit());
    }

}
