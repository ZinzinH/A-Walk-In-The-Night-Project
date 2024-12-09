using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float chaseDistance = 5f;
    public Transform player;

    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private bool movingForward = true;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);


        if (distanceToPlayer <= chaseDistance)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }


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

        if (patrolPoints.Length > 0)
        {
            Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
            Vector3 direction = (targetPatrolPoint.position - transform.position).normalized;


            rb.velocity = new Vector3(direction.x * patrolSpeed, rb.velocity.y, direction.z * patrolSpeed);


            if (Vector3.Distance(transform.position, targetPatrolPoint.position) < 1f)
            {

                if (movingForward)
                {
                    currentPatrolIndex++;
                    if (currentPatrolIndex >= patrolPoints.Length)
                    {
                        currentPatrolIndex = patrolPoints.Length - 2;
                        movingForward = false;
                    }
                }
                else
                {
                    currentPatrolIndex--;
                    if (currentPatrolIndex < 0)
                    {
                        currentPatrolIndex = 1;
                        movingForward = true;
                    }
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void ChasePlayer()
    {

        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector3(direction.x * chaseSpeed, rb.velocity.y, direction.z * chaseSpeed);


        if (Vector3.Distance(transform.position, player.position) < 1f)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
        Cursor.visible = true;  // Make the cursor visible
        SceneManager.LoadScene("GameOverScene");
    }
}