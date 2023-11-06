using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    // Get reference variables
    private PlayerController playerController;
    private Animator animator;
    private Transform attackHitbox;
    private Camera mainCamera;

    // Attack variables
    private float attackCooldown = 0.3f;
    private bool canAttack = true;

    private Vector2 mousePos;

    public enum AttackType
    {
        Right,
        Left,
        Up,
        Down
    }

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        attackHitbox = transform.GetChild(2);
        mainCamera = Camera.main;
    }

    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnAttack()
    {
        if (!playerController.isInShop)
        {
            if (canAttack)
            {
                Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

                Debug.Log("Vector2 direction is: " + direction);
                Debug.Log("Horizontal value is: " + direction.x);
                Debug.Log("Vertical value is: " + direction.y);

                animator.SetFloat("AttackHorizontal", direction.x);
                animator.SetFloat("AttackVertical", direction.y);
                animator.SetTrigger("Attack");

                canAttack = false;
                StartCoroutine(AttackCooldown());
            }
        }
    }

    public void HitboxPositioning(AttackType attack)
    {
        switch (attack)
        {
            case AttackType.Right:
                Debug.Log("Hitbox positioned for right attack.");
                attackHitbox.localRotation = Quaternion.identity;
                attackHitbox.localPosition = Vector2.zero;
                
                break;

            case AttackType.Left:
                Debug.Log("Hitbox positioned for left attack.");
                attackHitbox.localRotation = Quaternion.Euler(0, 180, 0);
                attackHitbox.localPosition = Vector2.zero;
                
                break;

            case AttackType.Up:
                Debug.Log("Hitbox positioned for up attack.");
                attackHitbox.localRotation = Quaternion.Euler(0, 0, 90);
                attackHitbox.localPosition = new Vector2(0.063f, 0.063f);

                break;

            case AttackType.Down:
                Debug.Log("Hitbox positioned for down attack.");
                attackHitbox.localRotation = Quaternion.Euler(0, 0, 270);
                attackHitbox.localPosition = new Vector2(-0.189f, -0.189f);

                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(playerController.attackDamage);
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
