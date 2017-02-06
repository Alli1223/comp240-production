using UnityEngine;
using System.Collections;

public class Placement : MonoBehaviour
{
    //Author: Kate Georgiou 11/1/16 Desc: deal with the placement of units and where they can be placed.
    //ATTACH TO PLAYER PREFABS.
    private Ray mouse;
    private RaycastHit click;
    [SerializeField]
    private Camera tactCam;
   

    public bool isInPlacementMode = false;

    

    void Start()
    {
        tactCam = UnitPlacement.inst.GetComponent<Camera>(); //retrives the camera component from the unit placement script.
    }
    // Update is called once per frame
    void Update()
    {
        if (isInPlacementMode == true) //if the player is able to place.
        {
            this.gameObject.layer = 2; //place this gameobject on layer 2
            transform.position = UnitPlacement.inst.GetClickHit(); //transform the posistion the the unit placement area that is hit with the raycast click.

            mouse = tactCam.ScreenPointToRay(Input.mousePosition); // assigns the area that is clicked on the tact cam to the mouse variable.
            if (Physics.Raycast(mouse, out click))
            {
                if (Input.GetMouseButtonDown(0)) //left click
                {
                    if (click.collider.gameObject.tag == "Player1Area") //have the floor made of several different components that have different tags so if the player clicks on their area their unit is put down?
                    {
                        
                        isInPlacementMode = false;
                        this.gameObject.layer = 0;
                        UnitPlacement.inst.CanPlace(true);
                    }
                    if (click.collider.gameObject.tag == "PLayer2Area") //have the floor made of several different components that have different tags so if the player clicks on their area their unit is put down?
                    {
                        isInPlacementMode = false;
                        this.gameObject.layer = 0;
                        UnitPlacement.inst.CanPlace(true);
                    }
                }
            }
        }
    }
}
