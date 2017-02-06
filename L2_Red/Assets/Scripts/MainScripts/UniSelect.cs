using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UniSelect : MonoBehaviour
{
    //Author: Kate Georgiou 10/11/16 Desc: handles the selecting of units. 

    public static UniSelect inst;
    private Ray mouse;
    private RaycastHit mouseClickHit;
    [SerializeField]
    private Camera tactCam;
    private bool unitbeenSelected = false;
    public bool raycastAllowed = true;
        
    public List<GameObject> usedUnits = new List<GameObject>();

    void Awake()
    {
        inst = this;
    }

    void Start()
    {
        tactCam = GetComponent<Camera>(); //runs start bc it finds the camera.
        Update();
    }

    void Update() //stops running anything past this point.
    {
       
        if (raycastAllowed == true)
        {
            Raycast(); 
        }

        mouse = tactCam.ScreenPointToRay(Input.mousePosition); //Get the mouse position from the tactical camera
        Raycast(); //Call the raycast method

        int unitsInGame = UnitManager.inst.player1Units.Count + UnitManager.inst.player2Units.Count;
        if (usedUnits.Count == unitsInGame)
        {
            usedUnits.Clear();
        }

    }

    private void Raycast()
    {
        if (Input.GetMouseButtonUp(0)) //When left click...
        {
            if (Physics.Raycast(mouse, out mouseClickHit)) //Create the raycast
            {

                if (mouseClickHit.collider.gameObject.tag == "Player") //if you click on the player - this works as it runs the rest of the code except the cam moving stuff.
                {
                     bool usedBefore = false;
                    foreach (GameObject unit in usedUnits)
                    {
                        if (unit == mouseClickHit.collider.gameObject)
                        {
                            usedBefore = true; //if a unit is selected then set it to used before so it cant be seelcted again until all the other units have been played though. 
                        }
                    }
                    UnitInfo unitInfoScript = mouseClickHit.collider.gameObject.GetComponent<UnitInfo>(); //Get the unit info script

                    if (/*Switch.inst.GetPlayerTurn() == unitInfoScript.GetInfo("player") &&*/ usedBefore == false)//&& //unitbeenSelected == false) //If the selected unit is owned by the same person whos turn it is, then select the object
                    {
                       //unitbeenSelected = true;
                        raycastAllowed = false;
                        UnitManager.inst.SelectUnit(mouseClickHit.collider.gameObject); //The selected player now the selected object
                       
                        CameraSwitch.instance.MoveCameraToSelectedUnit(); //Move the fps camera to the selected object
                        //usedUnits.Add(mouseClickHit.collider.gameObject);
                    }

                }
            }
        }

    }
}

