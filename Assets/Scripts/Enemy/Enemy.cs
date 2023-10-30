using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider2D hurtbox;
    private CircleCollider2D pushbox;

    public GameObject potionPrefab;
    
    [HideInInspector] public int attackDMG = -10;
    
    public float startingHealth = 35f;
    [SerializeField] private float currentHealth;
    
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        hurtbox = GetComponent<CapsuleCollider2D>();
        pushbox = GetComponentInChildren<CircleCollider2D>();

        currentHealth = startingHealth;
    }

    private void OnEnable()
    {
        currentHealth = startingHealth;

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
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
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
        animator.SetBool("IsDead", true);

        EnemySpawnPoint spawnPoint = transform.parent.GetComponent<EnemySpawnPoint>();
        spawnPoint.EnemyDied();

        Instantiate(potionPrefab, transform.position, Quaternion.identity);

        Debug.Log("Enemy died!");
    }
}