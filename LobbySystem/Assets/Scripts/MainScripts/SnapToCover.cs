using UnityEngine;
using System.Collections;
//Author: Fabian (Designer) & Kate Georgiou 29/10/16 Desc: allows for the player to snap to a cover point while in the collider radius by pressing left contrl.

public class SnapToCover : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform targetLocation;
    [SerializeField]
    private Canvas myCanvas;
    private bool isSnapped = false;

    void Start()
    {
        targetLocation = this.gameObject.transform.GetChild(0); //hooked gameObject
    }

    void OnTriggerStay(Collider other) //while inside the collider 
    {
        if (other.gameObject.tag == "Player") //and if tagged as player.
        {
            player = other.gameObject;
            if (isSnapped == false) // if not already snapped.
            {
                player.transform.position = targetLocation.transform.position; //teleports player to snap point.
                isSnapped = true; //change the bool. 
            }
            ManualSnap(); //run the manual snap code.
        }

    }
    void OnTriggerEnter(Collider other) //when entering the collider...
    {
        if (other.gameObject.tag == "Player") // and tagged as player
        {
            player = other.gameObject;
            myCanvas.enabled = true; //enabled the canvas to prompt the player.

            ManualSnap(); //run manual snap.
            isSnapped = true; //change the bool. 
        }

    }
    void OnTriggerExit(Collider other) //when exiting the collider. 
    {
        if (other.gameObject.tag == "Player")
        {
            player = null;
            myCanvas.enabled = false; //does the oppsite of everything in the trigger enter collider.
            isSnapped = false;
        }
    }
    public void ManualSnap() //runs manual snapping function. 
    {

        if (Input.GetKeyDown(KeyCode.LeftControl) && myCanvas.enabled == true && player != null) //if pressing left control while in the trigger zone...
        {
            player.transform.position = targetLocation.transform.position; //changes the players posistion to th snap point. 
            isSnapped = true;
            myCanvas.enabled = false;
        }
    }

}
