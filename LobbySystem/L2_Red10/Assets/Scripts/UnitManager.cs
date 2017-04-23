using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    //Declare the singleton
    public static UnitManager inst;
    //Variables for the list
    [SerializeField]
    private List<GameObject> player1Units = new List<GameObject>(), player2Units = new List<GameObject>();
    [SerializeField]
    private GameObject unitSelected;

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

    public void RemovePlayerFromList(int playerNumber, GameObject objectToRemove)
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
    } //Remove the specified player object from the required list

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
        return player1Units;
    }

    public GameObject GetSelectedUnit() //Return the selected unit
    {
        return unitSelected;
    }
}
