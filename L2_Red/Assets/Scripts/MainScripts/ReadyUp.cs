using UnityEngine;
using System.Collections;
//using UnityEngine.Networking;

public class ReadyUp : MonoBehaviour
{
    //Author: Kate Georgiou 11/1/16
    // ATTACH TO THE READY UP CANVAS AND THEN ON THE BUTTON RETRIVE THE READYUPBUTTON FUNCTION.

    [SerializeField]
    private bool UnitsPlaced = false, buttonPressed = false; // different bools.
    // [SerializeField]
    // private Canvas visRep;
    // [SyncVar]
    public float countDown = 30f;
    [SerializeField]
    private GameObject P1VisRepToTurnOff, P2VisRepToTurnOff; //the gameobjects that act as visual representations will need turning off once the units have been placed.
    [SerializeField]
    private Canvas readyCan;
    public static ReadyUp inst; //make into a singleton.

    void Awake()
    {
        inst = this; //initialise singleton.
    }
    // Use this for initialization
    void Start()
    {
        readyCan.enabled = true; //the ready up canvas is active on start.
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed == true) //if the ready up button is pressed..
        {
            readyCan.enabled = false; //..close the canvas...
            countDown--; //start the countdown.

        }
        if (countDown == 0) //when the countdown reaches 0
        {

            Switch.inst.startTurn.enabled = true; //make the start turn canvas true.
            Switch.inst.endTurn.enabled = false; //make end turn canvas false.

            buttonPressed = false; //change bool to false.
        }


    }
    public void ReadyUpButton()//the functionality for when the button is pressed.
    {
        buttonPressed = true;  //change bools to true.
        UnitsPlaced = true;
        if (UnitsPlaced == true) //if all the units are placed.
        {


            P1VisRepToTurnOff.SetActive(false); //disabled visual rep game objects.
            P2VisRepToTurnOff.SetActive(false);

        }

    }


}
