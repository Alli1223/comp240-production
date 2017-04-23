//------------------------------------------------------------------------------------------------------
// Author : Ross Perry 
// Date : 14 / 11 / 2016  
// Description : Manager to handle all functionality relating to camera switching
// Features a singleton so that the camera location is always the same and functions to manage switching
//-------------------------------------------------------------------------------------------------------


using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraSwitch : NetworkBehaviour
{
    //Defines the script as a singleton
    public static CameraSwitch instance;
    [SerializeField]
    private GameObject snapPoint;

    private bool allUnitsPlaced = false;

    //Prevents problems if more than one instance of this singleton is used
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

    //Makes sure the camera is in the correct state when network clients are started
    public override void OnStartClient()
    {
        Camera.main.enabled = true;
    }

    public void MoveCameraToSelectedUnit()
    {

        if (UnitManager.inst.GetContentsOfList(1).Count == 6)  //Prevents the camera changing to first person view when not all of the units have been placed
        {
            allUnitsPlaced = true;

            if (allUnitsPlaced == true)
            {
                Camera.main.transform.position = UnitManager.inst.GetSelectedUnit().transform.position; //Transform the fps camera to the selected unit

                Camera.main.transform.parent = UnitManager.inst.GetSelectedUnit().GetComponent<CharacterMovement>().dock.transform; //Parent to camera dock

                Camera.main.transform.localPosition = new Vector3(0, 0, 0); //Insures the camera is in the correctly location by resetting it after it's parented

                Camera.main.transform.rotation = UnitManager.inst.GetSelectedUnit().GetComponent<CharacterMovement>().dock.transform.rotation; //Set new rotation
            }
            else
            {
                return;

            }

        }

    }

    public void UnPair()
    {
        Camera.main.transform.parent = null; //Unparent the camera from the old object
    }

    public void PairToTact() //Handles moving the camera back to the tactical view position after turns
    {
        Camera.main.transform.position = snapPoint.transform.position;//Transform tactical camera back
        Camera.main.transform.parent = snapPoint.transform; //Parent to camera dock
        Camera.main.transform.rotation = snapPoint.transform.rotation; //Set new rotation
    }

}
