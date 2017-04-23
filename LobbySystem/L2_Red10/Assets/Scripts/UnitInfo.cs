using UnityEngine;
using System.Collections;

public class UnitInfo : MonoBehaviour
{
    [SerializeField]
    private float speed, strength, energy, energyDrainRate; //Obviously add your own variables for this
    [SerializeField]
    private int playerOwner = 1; //Player1 is owner by default

    private void Start()
    {
        UnitManager.inst.AddPlayerToList(playerOwner, this.gameObject);
    }

    private void Update()
    {
        //Check if the energy has hit zero then remove from the switch list thing
        if (energy <= 0 && Switch.inst.GetPlayerTurn() == playerOwner)
        {
            Switch.inst.DecreaseUnitsLeftBeforeSwitch();
        }
        //FIX STILL BEING ABLE TO MOVE ETC
    }

    public float GetInfo(string variableName) //Will be used for getting the count of any of the variables
    {
        variableName.ToLower(); //Will make the input variable lower case regardless

        switch (variableName)
        {
            case "speed":
                return speed;
            case "strength":
                return strength;
            case "energy":
                return energy;
            case "player":
                return playerOwner;
        }
        return 0;
    }

    public void RemoveEnergy() //This will be called in the movement script when the player is moving to remove energy
    { 
        energy = energy - energyDrainRate * Time.deltaTime;
    }

    public void ResetEnergy() //Call me to restore the energy values
    {
        energy = 100;
   }
}
