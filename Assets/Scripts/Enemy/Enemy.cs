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
    private bool isDead = false;

    // Attack variables
    public int attackDMG = -20;
    private float atkRange = 1.25f;
    private float atkRangeSqr;
    
    private float atkCooldown = 1f;
    private float nextAttackAllowed;

    // Potion variables
    public float dmgDone = 0f;
    public int healingMin = 5;
    public int healingMax = 20;

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
        atkRangeSqr = atkRange * atkRange;
    }

    private void OnEnable()
    {
        currentHealth = startingHealth;
        dmgDone = 0f;
        goblinMovement.canMove = true;
        isDead = false;

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
        if (!isDead)
        {
            animator.SetFloat("Speed", rb.velocity.magnitude);

            if ((goblinMovement.playerTransform.position - transform.position).sqrMagnitude < atkRangeSqr)
            {
                if (Time.time > nextAttackAllowed)
                {
                    Attack();
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("<color=green>Enemy health is </color>" + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        nextAttackAllowed = Time.time + atkCooldown;
    }

    private void Die()
    {
        animator.SetTrigger("Death");
        isDead = true;

        rb.simulated = false;
        hurtbox.enabled = false;
        pushbox.enabled = false;
        goblinMovement.enabled = false;

        EnemySpawnPoint spawnPoint = transform.parent.GetComponent<EnemySpawnPoint>();
        spawnPoint.EnemyDied();

        HealthPotion healthPotion = Instantiate(potionPrefab, transform.position, Quaternion.identity).GetComponent<HealthPotion>();
        healthPotion.dmgDone = dmgDone;
        healthPotion.healingRange = Random.Range(healingMin, healingMax);

        Debug.Log("Enemy died!");
    }
}
