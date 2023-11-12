using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float attractDistance = 3.5f;
    private float attractDistanceSqr;
    public float attractSpeed = 2f;
    public float idleSlow = 0.1f;

    private Rigidbody2D rb;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.Find("Player").transform;
        attractDistanceSqr = attractDistance * attractDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log(Vector2.Distance(playerTransform.position, transform.position));
        }
    }

    private void FixedUpdate()
    {
        if ((playerTransform.position - transform.position).sqrMagnitude < attractDistanceSqr)
        {
            Vector2 targetDirection = (playerTransform.position - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * attractSpeed;
        }
        else if ((playerTransform.position - transform.position).sqrMagnitude > attractDistanceSqr)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleSlow);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            playerController.UpdateHealth(Random.Range(1, 20));
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attractDistance);
    }
}
