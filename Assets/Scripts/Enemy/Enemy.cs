using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Declare reference variables
    private Animator animator;
    private CapsuleCollider2D hurtbox;
    private CircleCollider2D pushbox;
    private Transform sprite;
    private Transform playerTransform;
    public GameObject potionPrefab;

    // Health variables
    public float startingHealth = 35f;
    [SerializeField] private float currentHealth; // Serialized for debugging
    private bool isDead = false;

    // Attack variables
    private float atkRange = 3f;
    private float atkRangeSqr;
    private float followRange = 4f;
    private float followRangeSqr;
    private float atkCooldown = 1f;
    [HideInInspector] public int atkDMG = -10;
    [SerializeField] private bool canAttack = true; // Serialized for debugging

    // Start is called before the first frame update
    void Awake()
    {
        // Set reference variables
        animator = GetComponentInChildren<Animator>();
        hurtbox = GetComponent<CapsuleCollider2D>();
        pushbox = GetComponentInChildren<CircleCollider2D>();
        sprite = transform.GetChild(0);

        currentHealth = startingHealth;
        atkRangeSqr = atkRange * atkRange;
        followRangeSqr = followRange * followRange;
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        currentHealth = startingHealth;
        isDead = false;

        if (!hurtbox.enabled)
        {
            hurtbox.enabled = true;
        }

        if (!pushbox.enabled)
        {
            pushbox.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((playerTransform.position - transform.position).sqrMagnitude < followRangeSqr & !isDead)
        {
            Vector2 pivotDirection = (playerTransform.position - transform.position).normalized;
            Vector2 scale = sprite.localScale;
            
            if (pivotDirection.x < 0)
            {
                scale.x = -1;
            }
            else if (pivotDirection.x > 0)
            {
                scale.x = 1;
            }
            sprite.localScale = scale;

            if ((playerTransform.position - transform.position).sqrMagnitude < atkRangeSqr && canAttack)
            {
                canAttack = false;
                Attack();
                Invoke("RegainAttack", 2f);
            }
        }
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
        hurtbox.enabled = false;
        pushbox.enabled = false;
        isDead = true;
        animator.SetBool("IsDead", true);

        EnemySpawnPoint spawnPoint = transform.parent.GetComponent<EnemySpawnPoint>();
        spawnPoint.EnemyDied();

        Instantiate(potionPrefab, transform.position, Quaternion.identity);

        Debug.Log("Enemy died!");
    }

    private void RegainAttack()
    {
        canAttack = true;
    }
}
