using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Vector2 mousePos;

    private Camera mainCamera;
    [HideInInspector] public Animator animator;
    private Transform weaponPivot;

    private Vector2 pivotDirection;
    
    private bool isAttacking = false;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        weaponPivot = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (!isAttacking)
        {
            pivotDirection = (mousePos - (Vector2)weaponPivot.position).normalized;
        }

        weaponPivot.right = pivotDirection;

        Vector2 scale = weaponPivot.localScale;
        if (pivotDirection.x < 0)
        {
            scale.y = -1;
        }
        else if (pivotDirection.x > 0)
        {
            scale.y = 1;
        }
        weaponPivot.localScale = scale;
    }

    public void IsAttacking()
    {
        isAttacking = true;
    }

    public void IsNotAttacking()
    {
        isAttacking = false;
    }
}
