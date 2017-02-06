using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
//desc: camera movement for first person controller.
//This script needs to be attached to the camera object, however it is poorly optimised right now and I need to clamp the directions in which the camera can turn (for now just pause the game to exit without it going crazy)
//You can change the sensitivity in the editor.
{



    public bool allowedToMove = true;
    public static CameraMovement instance;
    [SerializeField]
    //   private Transform player;
    public Transform camTransform;
    private Camera cam;
    private float distance = 2f, currentX, currentY;
    [SerializeField]
    private float SensX = 4f, SensY = 4f;

    void Awake()
    {
        instance = this;


    }


    void Start()
    {
        camTransform = transform;
        cam = Camera.main;
        //  player = UnitManager.inst.unitSelected.transform;

    }

    void Update()
    {
        // if (allowedToMove == true)
        // {
        currentX += -Input.GetAxis("Mouse Y");
        currentY += Input.GetAxis("Mouse X");
        // }
    }
    void LateUpdate()
    {


        Vector3 dir = new Vector3(0, 2.5f, -distance); //camera placement 
        Quaternion rotation = Quaternion.Euler(currentX, currentY, 0);
        camTransform.position = UnitManager.inst.GetSelectedUnit().transform.position + rotation * dir;
        camTransform.LookAt(UnitManager.inst.GetSelectedUnit().GetComponent<CharacterMovement>().lookAtCube.transform);

    }
}

