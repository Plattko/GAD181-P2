using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoblinMovement : MonoBehaviour
{
    // Get reference variables
    private Rigidbody2D rb;
    private Transform playerTransform;

    [Header("FOV Variables")]
    public float radius = 5;
    [Range(1, 360)] public float angle = 45;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public bool CanSeePlayer { get; private set; }

    [Header("Patrol Variables")]
    private List<Transform> patrolPoints = new List<Transform>();
    public float speed;
    public float startWaitTime;
    private float waitTime;

    private int randomPatrolPoint;

    [Header("Chase Variables")]
    public float chaseSpeed;
    public float separationRadius = 2.0f;

    // Direction to move
    private Vector2 moveDirection;

    void Start()
    {
        // Set reference variables
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Set patrol points
        Transform spawnPoint = transform.parent;
        int noOfPatrolPoints = spawnPoint.childCount - 1;
        int index = 0;

        for (int i = 0; i < noOfPatrolPoints; i++)
        {
            Transform patrolPoint = spawnPoint.GetChild(index);
            patrolPoints.Add(patrolPoint);
            index++;
        }

        randomPatrolPoint = Random.Range(0, (patrolPoints.Count - 1));

        Transform childCollider = transform.Find("AgroRange");
        awarenessCollider = childCollider.GetComponent<CircleCollider2D>();


        if (awarenessCollider != null)
        {
            awarenessCollider.isTrigger = true;
            awarenessCollider.radius = 10.0f;
        }

        Debug.Log("all start code has been run");
    }

    private void Update()
    {
        HandleRotation();
        
        if (CanSeePlayer)
        {
            Chase();
            ApplySeparation();
        }
        else
        {
            Patrol();
        }
    }

    private void HandleRotation()
    {
        //flip the sprite based on the movement direction
        if (moveDirection.x > 0)  //right
        {
            transform.localScale = new Vector3(1, 1, 1); //no flipping
        }
        else if (moveDirection.x < 0)  //left
        {
            transform.localScale = new Vector3(-1, 1, 1); //flipping the sprite
        }
    }

    private void Patrol()
    {
        Vector2 targetPosition = patrolPoints[randomPatrolPoint].position;
        Vector2 currentPosition = transform.position;

        moveDirection = (targetPosition - currentPosition).normalized;

            //flip the sprite based on the movement direction
            if (moveDirection.x > 0)  //right
            {
                randomPatrolPoint = Random.Range(0, patrolPoints.Count);
                waitTime = startWaitTime;
            }
            else if (moveDirection.x < 0)  //left
            {
                waitTime -= Time.deltaTime;
            }

            //move the goblin
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);

            if (Vector2.Distance(currentPosition, targetPosition) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    randomPatrolPoint = Random.Range(0, patrolPoints.Count);
                    waitTime = startWaitTime;
                    anim.SetBool("isRunning", true);
                }
                else
                {
                    waitTime -= Time.deltaTime;
                    anim.SetBool("isRunning", false);
                }
            }
    }
    

    private void Chase()
    {
        moveDirection = (playerTransform.position - transform.position).normalized;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, playerTransform.position, speed * Time.deltaTime);
    }

    private void ApplySeparation()
    {
        GameObject[] goblins = GameObject.FindGameObjectsWithTag("Enemy"); //get all enemy game objects
        foreach (GameObject goblin in goblins)
        {
            if (goblin != gameObject)
            {
                if (Vector2.Distance(transform.position, goblin.transform.position) < separationRadius)
                {
                    Vector2 separationDirection = (transform.position - goblin.transform.position).normalized;
                    transform.Translate(separationDirection * speed * Time.deltaTime, Space.World);
                }
            }
        }
    }

}
