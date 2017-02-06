using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SwitchTurn : NetworkBehaviour
{
    //Author Ross
    //Remake of Switch to encompass networking

    [SyncVar(hook = "UpdateTurn")] public int playerTurn = 1; //Update the player turn across clients
    [SyncVar] private float turnTime = 6f;

    public static SwitchTurn instance;
    public Canvas startTurn, endTurn;
    //public KeyCode startKey, endKey;

    [SerializeField] private Camera tactCam;
    [SerializeField] private float defaultTime;
  
    private bool TimerDone;
    private bool turnHad = false, timerRun = false;


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

    void Start()
    {
        tactCam.GetComponent<CameraMovement>().enabled = false; //disables camera movement on start up.
        defaultTime = turnTime; //sets the time to the default time
    }

    void Update()
    {
//        Debug.Log("turn time: " + turnTime);
//        Debug.Log("timerdone: " + TimerDone);

        endTurn.enabled = false;

        if (turnTime <= 0)
        {
            RpcEndSetup();
            if (Input.GetKeyDown(KeyCode.F))
            {
                CmdEndTurn(playerTurn); 
            }
               
        }

        if (Input.GetKeyUp(KeyCode.Escape) && timerRun == false)
        {
            Debug.Log("turn started");
            timerRun = true;
            tactCam.GetComponent<CameraMovement>().enabled = true;
            UnitManager.inst.unitSelected.GetComponent<CharacterMovement>().enabled = true; //enables character movement.
            ElapseASecond(); //Invoke the timer after a second
        }

    }

    [Command]
    void CmdDebug() //Sends the debug to the server
    {
        Debug.Log("UpdatedTurn: " + playerTurn); //Any debug message you want to see in the editor
    }
     
    [Command]
    public void CmdEndTurn(int _playerTurn)
    {
        Debug.Log("endturnwascalled");
        if (TimerDone == true /*&& Input.GetKeyDown(KeyCode.F)*/ && playerTurn == 1)
        {
            playerTurn = 2;
            RpcManualEnd(playerTurn);
            ResetTimer();
        }
        else if (TimerDone == true /*&& Input.GetKeyDown(KeyCode.F)*/ && playerTurn == 2)
        {
            CmdDebug();
            playerTurn = 1;
            RpcManualEnd(playerTurn);
            ResetTimer();
        }

    }
        
    void UpdateTurn(int _playerTurn)
    {
        playerTurn = _playerTurn;
        Debug.Log("playerTurn: " + playerTurn);
    }

    [ClientRpc]
    void RpcManualEnd(int _playerTurn)
    {
        startTurn.enabled = false; // Enable/disable canvas'
        endTurn.enabled = true;

        tactCam.GetComponent<CameraMovement>().enabled = false;  //disables camera movement.
        UnitManager.inst.unitSelected.GetComponent<CharacterMovement>().enabled = false;//disable false.
        UnitManager.inst.unitSelected = null;//set unit selected to null. 

        CameraSwitch.instance.UnPair();//unpair and repair the camera. 
        CameraSwitch.instance.PairToTact();
    }
     
    [ClientRpc]
    void RpcEndSetup()
    {
        //Debug.Log("endedsetup");
        UniSelect.inst.raycastAllowed = true;
        startTurn.enabled = false; //enable and disable cavnas's. 
        endTurn.enabled = true;
        TimerDone = true;

        UnitManager.inst.unitSelected.GetComponent<CharacterMovement>().enabled = false; //disables movement.
    }

    void ElapseASecond() //Uses recursion but shouldn't be very expensive as it is a small method that will clear up when finished
    {
        turnTime = turnTime - 1;
        if (turnTime > 0)
        {
            Invoke("ElapseASecond", 1); 
        }

    }
    public void ResetTimer() //resets the timer for the next turn. 
    {
        turnTime = defaultTime;
        timerRun = false;
        TimerDone = false;
        UniSelect.inst.usedUnits.Add(UnitManager.inst.GetSelectedUnit());
    }
         
        
}
