using UnityEngine;

// Very simple smooth mouselook modifier for the MainCamera in Unity
// by Francis R. Griffiths-Keam - www.runningdimensions.com

public class ClampedMouseLook : MonoBehaviour
{
    Vector2 mousePosition;
    Vector2 _smoothMouse;

    [SerializeField]
    private Vector2 clampValue = new Vector2(360, 180), sensitivity = new Vector2(2, 2), smoothing = new Vector2(3, 3);
    private Vector2 targetDirection, targetCharacterDirection;
    [SerializeField]
    private bool lockCursor;

    // Assign this if there's a parent object controlling motion, such as a Character Controller.
    [SerializeField]
    private GameObject playerAttatchedTo;

    void Start()
    {
        // Set target direction to the camera's initial orientation.
        targetDirection = transform.localRotation.eulerAngles;

        // Set target direction for the character body to its inital state.
        if (playerAttatchedTo) targetCharacterDirection = playerAttatchedTo.transform.localRotation.eulerAngles;
    }

    void Update()
    {
        // Ensure the cursor is always locked when set
        Screen.lockCursor = lockCursor;

        // Allow the script to clamp based on a desired target value.
        var targetOrientation = Quaternion.Euler(targetDirection);
        var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

        // Get raw mouse input for a cleaner reading on more sensitive mice.
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Scale input against the sensitivity setting and multiply that against the smoothing value.
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // Interpolate mouse movement over time to apply smoothing delta.
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Find the absolute mouse movement value from point zero.
        mousePosition += _smoothMouse;

        // CLAMP X AXIS FIRST, so as not to be affected by world transforms.
        if (clampValue.x < 360)
            mousePosition.x = Mathf.Clamp(mousePosition.x, -clampValue.x * 0.5f, clampValue.x * 0.5f);

        var xRotation = Quaternion.AngleAxis(-mousePosition.y, targetOrientation * Vector3.right);
        transform.localRotation = xRotation;

        // CLAMP AND APPLY THE GLOBAL Y VALUE
        if (clampValue.y < 360)
            mousePosition.y = Mathf.Clamp(mousePosition.y, -clampValue.y * 0.5f, clampValue.y * 0.5f);

        transform.localRotation *= targetOrientation;

        // If there's a character body that acts as a parent to the camera
        if (playerAttatchedTo)
        {
            var yRotation = Quaternion.AngleAxis(mousePosition.x, playerAttatchedTo.transform.up);
            playerAttatchedTo.transform.localRotation = yRotation;
            playerAttatchedTo.transform.localRotation *= targetCharacterOrientation;
        }
        else
        {
            var yRotation = Quaternion.AngleAxis(mousePosition.x, transform.InverseTransformDirection(Vector3.up));
            transform.localRotation *= yRotation;
        }
    }
    void FixedUpdate()
    {
        if (transform.parent != null)
        {
            playerAttatchedTo = transform.parent.gameObject;
        }
    }
}