using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStateManager : MonoBehaviour
{
    // Declare reference variables
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator animator;
    private CapsuleCollider2D hurtbox;
    private CircleCollider2D pushbox;
    private Transform sprite;
    public GameObject potionPrefab;
    [HideInInspector] public Transform playerTransform;

    // Health variables
    public float startingHealth = 35f;
    public float currentHealth; // Serialized for debugging
    private bool isDead = false;
    
    // Aggro variables
    private float aggroRange = 4f;
    public float aggroRangeSqr;
    [HideInInspector] public float moveSpeed = 4f;

    // Attack variables
    private float atkRange = 3f;
    public float atkRangeSqr;
    private float atkCooldown = 1f;
    [HideInInspector] public int atkDMG = -10;
    [SerializeField] private bool canAttack = true; // Serialized for debugging

    // Reference to the active state in the state machine
    GoblinBaseState currentState;

    public GoblinPatrolState PatrolState = new GoblinPatrolState();
    public GoblinChaseState ChaseState = new GoblinChaseState();
    public GoblinAttackState AttackState = new GoblinAttackState();
    public GoblinDeadState DeadState = new GoblinDeadState();

    void Awake()
    {
        // Set reference variables
        animator = GetComponentInChildren<Animator>();
        hurtbox = GetComponent<CapsuleCollider2D>();
        pushbox = GetComponentInChildren<CircleCollider2D>();
        sprite = transform.GetChild(0);

        currentHealth = startingHealth;
        aggroRangeSqr = aggroRange * aggroRange;
        atkRangeSqr = atkRange * atkRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        currentState = PatrolState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);

        if (rb.velocity.magnitude > 0f)
        {
            animator.SetBool("IsMoving", true);
        }
        else if (rb.velocity.magnitude > 0f)
        {
            animator.SetBool("IsMoving", false);
        }
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(GoblinBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        Debug.Log("<color=green>Enemy health is </color>" + currentHealth);
    }
}
