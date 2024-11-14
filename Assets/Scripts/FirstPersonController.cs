using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5.0f;      // Movement speed
    public float lookSpeedX = 2.0f;     // Mouse look speed (X-axis)
    public float lookSpeedY = 2.0f;     // Mouse look speed (Y-axis)
    public float upDownRange = 60.0f;   // Limit to look up and down
    
    private float rotationX = 0;         // Current vertical rotation
    
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Locks the cursor to the center of the screen
        Cursor.visible = false;                    // Makes the cursor invisible
        rb = GetComponent<Rigidbody>();             // Get the Rigidbody component
    }

    // Update is called once per frame
    void Update()
    {
        // Handle mouse look (rotation)
        float rotX = Input.GetAxis("Mouse X") * lookSpeedX;    // Look left and right
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;     // Look up and down
        rotationX = Mathf.Clamp(rotationX, -upDownRange, upDownRange);  // Clamp up/down view range

        transform.Rotate(0, rotX, 0);                         // Rotate player left/right
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);  // Rotate camera up/down

        // Handle movement (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed; // Left/Right (A/D or Arrow Keys)
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;   // Forward/Backward (W/S or Arrow Keys)
        
        Vector3 movement = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + movement * Time.deltaTime);  // Move player
    }
}
