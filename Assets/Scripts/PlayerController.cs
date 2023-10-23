using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public HealthBar healthBar;

    public float startingMoveSpeed = 5f;
    [HideInInspector] public float moveSpeed = 5f;
    public float startingAttackDamage = 10f;
    [HideInInspector] public float attackDamage = 10f;
    private float attackCooldown = 0.3f;

    private float idleSlow = 0.9f;

    private Vector2 moveInput;

    private bool canAttack = true;
    public bool isInShop { private get; set; } = false;

    public int startingHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = startingHealth;
        healthBar.SetHealth(currentHealth, startingHealth);

        moveSpeed = startingMoveSpeed;
        attackDamage = startingAttackDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateHealth(-20);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            UpdateHealth(10);
        }
    }

    private void FixedUpdate()
    {
        if (!isInShop)
        {
            if (moveInput != Vector2.zero)
            {
                rb.velocity = moveInput * moveSpeed;
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleSlow);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void UpdateHealth(int health)
    {
        currentHealth += health;
        healthBar.SetHealth(currentHealth, startingHealth);

        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        //if (!isInShop)
        //{
        //    moveInput = inputValue.Get<Vector2>();
        //}

        moveInput = inputValue.Get<Vector2>();
    }

    private void OnAttack()
    {
        if (!isInShop)
        {
            if (canAttack)
            {
                Weapon weapon = GetComponentInChildren<Weapon>();
                weapon.animator.SetTrigger("Attack");

                canAttack = false;
                StartCoroutine(AttackCooldown());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(attackDamage);
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
