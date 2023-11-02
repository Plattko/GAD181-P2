using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Get reference variables
    private Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D hurtbox;
    private CircleCollider2D pushbox;
    private GoblinMovement goblinMovement;
    public GameObject potionPrefab;
    
    // Health variables
    public float startingHealth = 35f;
    [SerializeField] private float currentHealth;

    // Attack variables
    [HideInInspector] public int attackDMG = -10;

    // Start is called before the first frame update
    void Awake()
    {
        // Set reference variables
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hurtbox = GetComponent<CapsuleCollider2D>();
        pushbox = GetComponentInChildren<CircleCollider2D>();
        goblinMovement = GetComponent<GoblinMovement>();

        currentHealth = startingHealth;
    }

    private void OnEnable()
    {
        currentHealth = startingHealth;

        if (!rb.simulated)
        {
            rb.simulated = true;
        }

        if (!hurtbox.enabled)
        {
            hurtbox.enabled = true;
        }

        if (!pushbox.enabled)
        {
            pushbox.enabled = true;
        }

        if (!goblinMovement.enabled)
        {
            goblinMovement.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }

        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        Debug.Log("<color=green>Enemy health is </color>" + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void Die()
    {
        rb.simulated = false;
        hurtbox.enabled = false;
        pushbox.enabled = false;
        goblinMovement.enabled = false;
        animator.SetBool("IsDead", true);

        EnemySpawnPoint spawnPoint = transform.parent.GetComponent<EnemySpawnPoint>();
        spawnPoint.EnemyDied();

        Instantiate(potionPrefab, transform.position, Quaternion.identity);

        Debug.Log("Enemy died!");
    }
}
