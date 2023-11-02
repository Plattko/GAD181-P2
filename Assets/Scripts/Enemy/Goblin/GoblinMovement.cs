using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoblinMovement : MonoBehaviour
{
    [Header("FOV Variables")]
    public float radius = 5;
    [Range(1, 360)] public float angle = 45;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public Transform playerTransform;
    public bool CanSeePlayer { get; private set; }

    [Header("Patrol Variables")]
    public float speed;
    private float waitTime;
    public float startWaitTime;

    private Rigidbody2D rb;
    private Animator anim;

    public List<Transform> patrolPoints = new List<Transform>();
    private int randomPatrolPoint;

    [Header("Chase Variables")]
    public float chaseSpeed;
    private float distance;

    public float separationRadius = 2.0f;

    void Start()
    {
        //fov//
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(FOVCheck());

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
    
    /////////////////////////////////////////////////////FOV//////////////////////////////////////////////////////////////////////////////////////
    
    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FOV();
        }
    }
    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            Vector2 facingDirection = transform.localScale.x > 0 ? transform.right : -transform.right;

            float angleToTarget = Vector2.Angle(facingDirection, directionToTarget);

            if (angleToTarget < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                    CanSeePlayer = true;
                else
                    CanSeePlayer = false;
            }
            else
                CanSeePlayer = false;
        }
        else if (CanSeePlayer)
            CanSeePlayer = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 facingDirection = transform.localScale.x > 0 ? transform.right : -transform.right;

        Vector3 angle01 = Quaternion.AngleAxis(-angle / 2, Vector3.forward) * facingDirection;
        Vector3 angle02 = Quaternion.AngleAxis(angle / 2, Vector3.forward) * facingDirection;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if (CanSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }
    }
    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
