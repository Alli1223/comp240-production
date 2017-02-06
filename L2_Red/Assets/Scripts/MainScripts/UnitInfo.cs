using UnityEngine;
using System.Collections;

public class UnitInfo : MonoBehaviour
{
    //Author: Kate Georgiou 10/10/16 Desc: different stats for the different units, attach the the player prefab for the unit and you can set the information in the inspector. 
    [SerializeField]
    private float speed, strength, energy, energyDrainRate; 
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

    }

    public float GetInfo(string variableName) //Will be used for getting the count of any of the variables
    {
        variableName.ToLower(); //Will make the input variable lower case regardless

        switch (variableName)
        {
            case "speed":
                return speed; //if someone types speeed then return the speed.
            case "strength":
                return strength; //if someone types stregnth then return the strength.
            case "energy":
                return energy; //if someone types energy then return the energy.
            case "player":
                return playerOwner; //if someone types player owner then return the player owner.
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
