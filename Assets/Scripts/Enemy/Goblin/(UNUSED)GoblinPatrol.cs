using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinPatrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    private Rigidbody2D rb;
    private Animator anim;

    public Transform[] moveSpots;
    private int randomSpot;

    private void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isRunning", true);
    }

    private void Update()
    {
        Vector2 targetPosition = moveSpots[randomSpot].position;
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
                randomSpot = Random.Range(0, moveSpots.Length);
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
}

