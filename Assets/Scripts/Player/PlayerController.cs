using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Get reference variables
    private Rigidbody2D rb;
    private Animator animator;
    private Camera mainCamera;
    public HealthBar healthBar;

    // Movement variables
    private Vector2 moveInput;
    private float idleSlow = 0.9f;
    public float startingMoveSpeed = 6f;
    [HideInInspector] public float moveSpeed;
    
    // Attack variables
    public float startingAttackDamage = 10f;
    [HideInInspector] public float attackDamage;

    private float attackCooldown = 0.3f;
    private bool canAttack = true;

    private Vector2 mousePos;
    private Vector2 direction;

    // Health variables
    public int startingHealth = 100;
    public int currentHealth;

    public bool isInShop { private get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;

        currentHealth = startingHealth;
        healthBar.SetHealth(currentHealth, startingHealth);

        moveSpeed = startingMoveSpeed;
        attackDamage = startingAttackDamage;
    }

    // Update is called once per frame
    void Update()
    {
        // Get mouse position
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Update animator
        bool isMoving = moveInput != Vector2.zero;

        if (isMoving)
        {
            animator.SetFloat("Horizontal", moveInput.x);
            animator.SetFloat("Vertical", moveInput.y);
            animator.SetFloat("Speed", moveInput.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

        // Debugging
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
            //if (canAttack)
            //{
            //    Weapon weapon = GetComponentInChildren<Weapon>();
            //    weapon.animator.SetTrigger("Attack");

            //    canAttack = false;
            //    StartCoroutine(AttackCooldown());
            //}

            if (canAttack)
            {
                Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
                animator.SetFloat("AttackHorizontal", direction.x);
                animator.SetFloat("AttackVertical", direction.y);
                animator.SetTrigger("Attack");

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
