using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;  // Patrol points for the enemy
    public float patrolSpeed = 2f;    // Speed at which the enemy patrols
    public float chaseSpeed = 4f;     // Speed when chasing the player
    public float chaseDistance = 5f;  // Distance to chase the player
    public Transform player;          // Reference to the player

    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private bool movingForward = true; // Direction flag for patrolling
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Get the Rigidbody component
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;  // Automatically find player by tag
        }
    }

    void Update()
    {
        // Calculate distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the chase range
        if (distanceToPlayer <= chaseDistance)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        // Chase or patrol based on whether the enemy is chasing the player
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // Ensure patrolPoints array has at least two points to move back and forth
        if (patrolPoints.Length > 0)
        {
            Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
            Vector3 direction = (targetPatrolPoint.position - transform.position).normalized;

            // Move the enemy using Rigidbody velocity
            rb.velocity = new Vector3(direction.x * patrolSpeed, rb.velocity.y, direction.z * patrolSpeed);

            // Check if the enemy has reached the patrol point
            if (Vector3.Distance(transform.position, targetPatrolPoint.position) < 1f)
            {
                // Reverse direction at the end of the patrol path
                if (movingForward)
                {
                    currentPatrolIndex++;
                    if (currentPatrolIndex >= patrolPoints.Length)  // Reached the last patrol point
                    {
                        currentPatrolIndex = patrolPoints.Length - 2;  // Move backwards
                        movingForward = false;  // Change direction
                    }
                }
                else
                {
                    currentPatrolIndex--;
                    if (currentPatrolIndex < 0)  // Reached the first patrol point
                    {
                        currentPatrolIndex = 1;  // Start moving forward
                        movingForward = true;  // Change direction
                    }
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;  // Stop if no patrol points are set
        }
    }

    void ChasePlayer()
    {
        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector3(direction.x * chaseSpeed, rb.velocity.y, direction.z * chaseSpeed);

        // Check if the enemy collides with the player
        if (Vector3.Distance(transform.position, player.position) < 1f)  // Adjust for collision detection
        {
            EndGame();
        }
    }

    void EndGame()
    {
        // Game over logic
        Debug.Log("Game Over!");
        // Load the "GameOver" scene (replace "GameOverScene" with your scene name)
        SceneManager.LoadScene("GameOverScene");
    }
}
