using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    public GameObject potionPrefab;
    
    [HideInInspector] public int attackDMG = -10;
    public float startingHealth = 35f;
    private float currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        currentHealth = startingHealth;
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
        Debug.Log("<color=green>Enemy health is </color>" + currentHealth);

        animator.SetTrigger("Hurt");

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
        Debug.Log("Enemy died!");

        animator.SetBool("IsDead", true);

        Instantiate(potionPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f));

        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponentInChildren<CircleCollider2D>().enabled = false;
        
        this.enabled = false;
    }
}
