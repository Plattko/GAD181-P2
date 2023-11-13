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
    public HealthBar healthBar;

    // Movement variables
    private Vector2 moveInput;
    private float idleSlow = 0.9f;
    public float startingMoveSpeed = 6f;
    [HideInInspector] public float moveSpeed;

    // Attack variables
    public float startingAttackDamage = 10f;
    [HideInInspector] public float attackDamage;

    // Health variables
    public int startingHealth = 100;
    public int currentHealth;

    public bool isInShop = false;

    // Upgrade variables
    public float healthReturn = 0f;
    public float potionPotency = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = startingHealth;
        healthBar.SetHealth(currentHealth, startingHealth);

        attackDamage = startingAttackDamage;
        moveSpeed = startingMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
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
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    UpdateHealth(-20);
        //}
        //if (Input.GetKeyDown(KeyCode.LeftControl))
        //{
        //    UpdateHealth(10);
        //}
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
        moveInput = inputValue.Get<Vector2>();
    }
}
