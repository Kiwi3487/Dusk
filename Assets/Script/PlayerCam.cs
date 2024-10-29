using UnityEngine;
public class PlayerCam : MonoBehaviour
{
    //variables
    public float sensX; 
    public float sensY;

    public Transform orientation;
    public Transform directionalLight;

    private float yRotation;
    private float xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
    }

    private void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        // Adjust rotation based on mouse input
        yRotation += mouseX;
        xRotation -= mouseY;

        // Clamp the xRotation to avoid flipping the camera vertically
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate the camera based on input
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Rotate the player's body or orientation along the Y axis
        if (orientation != null)
        {
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        // Rotate the directional light along both X and Y axes
        if (directionalLight != null)
        {
            directionalLight.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }
}