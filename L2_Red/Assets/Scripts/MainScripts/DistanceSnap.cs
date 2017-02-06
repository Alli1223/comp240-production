using UnityEngine;
using System.Collections;

public class DistanceSnap : MonoBehaviour
{
    //Author: Kate Georgiou 1/11/16 Desc: SHOULD automatically snap the player to the closest cover point at the end of their turn. 

    [SerializeField]
    GameObject[] taggedCoverPoints; //an array of all the tagged cover points.
    [SerializeField]
    private GameObject closestPoint;// finds the closest cover point.
    void Start()
    {

        taggedCoverPoints = GameObject.FindGameObjectsWithTag("cover"); //get all the gameobjects with a tag
    }

    public GameObject FindClosestTag(string cover)
    {



        GameObject closestCoverPoint = null; //closest object found so far

        float shortestDistance = float.PositiveInfinity; //the shortest distance we've found so far

        foreach (GameObject go in taggedCoverPoints)//loop trough all gameobjects we found with the tag
        {

            if (go != this.gameObject)//we don't want to have the player be an option for the closest object
            {

                float currentDistance = Vector3.Distance(transform.position, go.transform.position); //the distance to the current gameobject
                                                                                                     
                if (currentDistance < shortestDistance)//this object is closer than the closest one so far 
                {
                    shortestDistance = currentDistance;
                    closestCoverPoint = go;//this one becomes the closest one so far
                }
            }
        }
       
       
        return closestCoverPoint; //return the gameobject we found

    }
    void Update()
    {
        FindClosestTag("cover"); //Runs the function by feeding the string that is the desired tag through the method and adds them to the array and therefore finds the closest point.
       
    }

}
