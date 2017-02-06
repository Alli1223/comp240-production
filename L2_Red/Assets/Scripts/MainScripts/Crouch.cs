using UnityEngine;
using System.Collections;
//Author Ross, Just change character controller height in the CrouchPlayer method and attach script to player

public class Crouch : MonoBehaviour
{
    //DEFINE ANIM
    [SerializeField]
    private CharacterController playerCol;


    void Start()
    {
        //GET ANIM COMPONENT
    }

    void Update()
    {
        CrouchPlayer();
    }


    void CrouchPlayer()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            //PLAY ANIM
            playerCol.height = 1.0f; //Change values as appropriate
        }
        else
        {
            playerCol.height = 1.963f;
            //STOP ANIM
        }

    }

}
