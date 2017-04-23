using UnityEngine;
using System.Collections;

public class UniSelect : MonoBehaviour
{
    private Ray mouse;
    private RaycastHit mouseClickHit;

    private void Update()
    {
        mouse = Switch.inst.GetTacticalCamera().ScreenPointToRay(Input.mousePosition); //Get the mouse position from the tactical camera
        Raycast(); //Call the raycast method
    }

    private void Raycast()
    {
        if (Input.GetMouseButtonUp(0)) //When left click...
        {
            if (Physics.Raycast(mouse, out mouseClickHit)) //Create the raycast
            {
                if (mouseClickHit.collider.gameObject.tag == "Player1") //if you click on the player
                {
                    UnitInfo unitInfoScript = mouseClickHit.collider.gameObject.GetComponent<UnitInfo>(); //Get the unit info script

                    if (Switch.inst.GetPlayerTurn() == unitInfoScript.GetInfo("player")) //If the selected unit is owned by the same person whos turn it is, then select the object
                    {
                        UnitManager.inst.SelectUnit(mouseClickHit.collider.gameObject); //The selected player now the selected object
                        Switch.inst.MoveCameraToSelectedUnit(); //Move the fps camera to the selected object
                    }

                }
            }
        }

    }
}
