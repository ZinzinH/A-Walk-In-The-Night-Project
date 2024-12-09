using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f, lookSpeedX = 2f, lookSpeedY = 2f, upDownRange = 60f, gravity = -9.81f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public AudioSource walkAudioSource; 

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

       
        if (!walkAudioSource) walkAudioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundLayer);

        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upDownRange, upDownRange);
        transform.Rotate(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        Vector3 movement = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime;

        if (isGrounded)
        {
            movement.y = 0;
        }
        else
        {
            movement.y = gravity * Time.deltaTime;  
        }

        rb.MovePosition(rb.position + movement);

        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && isGrounded)
        {
            if (!walkAudioSource.isPlaying) 
            {
                walkAudioSource.Play();
            }
        }
        else
        {
            if (walkAudioSource.isPlaying)
            {
                walkAudioSource.Stop(); 
            }
        }
    }
}