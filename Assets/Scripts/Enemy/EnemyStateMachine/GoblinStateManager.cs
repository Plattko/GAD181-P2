using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStateManager : MonoBehaviour
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
        atkRangeSqr = atkRange * atkRange;
        followRangeSqr = followRange * followRange;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        currentState = PatrolState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);

        // Call handle rotation
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this, collision);
    }

    public void SwitchState(GoblinBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    // Handle the rotation of the enemy
    void HandleRotation()
    {
        // Rotate enemy based on movement direction when player isn't in range
        // Rotate enemy based on player direction when player is in range
    }

    public IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(atkCooldown);
        canAttack = true;
    }
}
