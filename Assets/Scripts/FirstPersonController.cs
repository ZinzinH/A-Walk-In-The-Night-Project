using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f, lookSpeedX = 2f, lookSpeedY = 2f, upDownRange = 60f, gravity = -9.81f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public AudioSource walkAudioSource; // Add this line to reference the AudioSource component

    private float rotationX = 0f;
    private Rigidbody rb;
    private bool isGrounded;
    private const float groundDistance = 0.4f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (!groundCheck) groundCheck = transform;

        // Ensure an AudioSource is assigned
        if (!walkAudioSource) walkAudioSource = GetComponent<AudioSource>(); // Get the AudioSource component if not manually assigned
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundLayer);

        // Mouse look
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upDownRange, upDownRange);
        transform.Rotate(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Movement
        Vector3 movement = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime;

        // If grounded, don't apply gravity vertically
        if (isGrounded)
        {
            movement.y = 0;
        }
        else
        {
            movement.y = gravity * Time.deltaTime;  // Apply gravity if not grounded
        }

        // Apply the movement to the Rigidbody
        rb.MovePosition(rb.position + movement);

        // Play or Stop the walking sound
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && isGrounded)
        {
            if (!walkAudioSource.isPlaying) // Check if the audio is not already playing
            {
                walkAudioSource.Play(); // Play the walking sound
            }
        }
        else
        {
            if (walkAudioSource.isPlaying)
            {
                walkAudioSource.Stop(); // Stop the sound if there's no movement
            }
        }
    }
}