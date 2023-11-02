using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoblinMovement : MonoBehaviour
{
    public Transform playerTransform;
    public bool CanSeePlayer { get; private set; }

    [Header("Patrol Variables")]
    public float speed;
    private float waitTime;
    public float startWaitTime;

    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D awarenessCollider;

    public List<Transform> patrolPoints = new List<Transform>();
    private int randomPatrolPoint;

    [Header("Chase Variables")]
    public float chaseSpeed;
    private float distance;

    public float separationRadius = 2.0f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //patrol//
        Transform spawnPoint = transform.parent;
        int noOfPatrolPoints = spawnPoint.childCount - 1;
        int index = 0;

        // Get all the patrol points
        for (int i = 0; i < noOfPatrolPoints; i++)
        {
            Transform patrolPoint = spawnPoint.GetChild(index);
            patrolPoints.Add(patrolPoint);
            index++;
        }

        randomPatrolPoint = Random.Range(0, (patrolPoints.Count - 1));
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isRunning", true);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanSeePlayer = true;
            Debug.Log("The player is being seen");
        }
        else
        {
            Debug.Log("Something entered goblin's trigger but it's not the player");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanSeePlayer = false;
            Debug.Log("The player isnt seen");
        }
        else
        {
            Debug.Log("Something exited goblin's trigger but it's not the player");
        }
    }

    private void Patrol()
    {

            Vector2 targetPosition = patrolPoints[randomPatrolPoint].position;
            Vector2 currentPosition = transform.position;

            Vector2 moveDirection = (targetPosition - currentPosition).normalized;

            //flip the sprite based on the movement direction
            if (moveDirection.x > 0)  //right
            {
                transform.localScale = new Vector3(1, 1, 1); //no flipping
            }
            else if (moveDirection.x < 0)  //left
            {
                transform.localScale = new Vector3(-1, 1, 1); //flipping the sprite
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
        distance = Vector2.Distance(transform.position, playerTransform.position);
        Vector2 direction = playerTransform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //flip the sprite based on the movement direction
        if (direction.x > 0)  //right
        {
            transform.localScale = new Vector3(1, 1, 1); //no flipping
        }
        else if (direction.x < 0)  //left
        {
            transform.localScale = new Vector3(-1, 1, 1); //flipping the sprite
        }

        transform.position = Vector2.MoveTowards(this.transform.position, playerTransform.position, speed * Time.deltaTime);
        anim.SetBool("isRunning", true);

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
