using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : NetworkBehaviour
{
    //Author: Kate Georgiou 25/ 9/ 16 Desc: handles the management of all the units and the different lists in the units. 
    //To Use: add the a empty game object in the scene and drag in relevant components. 

    //Declare the singleton
    public static UnitManager inst;
    //Variables for the list
    public List<GameObject> player1Units = new List<GameObject>(), player2Units = new List<GameObject>();

    public GameObject unitSelected;

    void Awake()
    {
        inst = this;
    }

    public void AddPlayerToList(int playerNumber, GameObject objectToAdd) //Require the player number and object to add an add it to the list
    {
        switch (playerNumber)
        {
            case 1:
                player1Units.Add(objectToAdd);
                break;
            case 2:
                player2Units.Add(objectToAdd);
                break;
        }

    }

    public void RemovePlayerFromList(int playerNumber, GameObject objectToRemove) //Remove the specified player object from the required list
    {
        switch (playerNumber)
        {
            case 1:
                player1Units.Remove(objectToRemove);
                break;
            case 2:
                player2Units.Remove(objectToRemove);
                break;
        }
    } 

    public void ClearBothPlayerLists() //Clear both player lists
    {
        player1Units.Clear();
        player2Units.Clear();
    }

    public void SelectUnit(GameObject unitToSelect) //Feed this method the arguement of the unit selected
    {
      
        unitSelected = unitToSelect;
    }

    public int GetNumberOfPlayerUnits(int playerNumber) //Get the amount of players in the list
    {
        switch (playerNumber)
        {
            case 1:
                return player1Units.Count;
            case 2:
                return player2Units.Count;
        }
        return 0;
    }

    public List<GameObject> GetContentsOfList(int playerNumber) //Use me to get the contents of the player lists
    {
        switch (playerNumber)
        {
            case 1:
                return player1Units;
            case 2:
                return player2Units;
        }
        return player2Units;
     
    }

    public GameObject GetSelectedUnit() //Return the selected unit
    {
       
        return unitSelected;

    }
}