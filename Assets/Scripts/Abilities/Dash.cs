using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 2f;
    private float nextDashAllowed;

    private bool canDash = true;
    private Rigidbody2D rb;
    private Vector2 originalVelocity = Vector2.zero;

    private bool isPressingW;
    private bool isPressingA;
    private bool isPressingS;
    private bool isPressingD;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    void Update()
    {
        UpdateKeyPress();

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > nextDashAllowed)
        {
            //retrieves the direction for the dash based on the current input.
            Vector2 dashDirection = GetDashDirection();

            if (dashDirection != Vector2.zero)
            {
                originalVelocity = rb.velocity;

                //initiates the dash action according to the determined dash direction.
                StartCoroutine(PerformDash(dashDirection));
            }
        }
    }

    IEnumerator PerformDash(Vector2 dashDirection)
    {
        //applies a cooldown period
        nextDashAllowed = Time.time + dashCooldown;

        Vector2 startPos = rb.position;

        //the target position for the dash.
        Vector2 endPos = startPos + dashDirection * dashDistance;

        float startTime = Time.time;

        //loop to move the Rigidbody from the start to the end position over the dash duration.
        while (Time.time < startTime + dashDuration)
        {
            //move the Rigidbody towards the end position based on the dash duration.
            rb.MovePosition(Vector2.Lerp(startPos, endPos, (Time.time - startTime) / dashDuration));
            yield return null;
        }

        rb.velocity = originalVelocity;
    }

    void UpdateKeyPress()
    {
        //update the boolean flags to indicate whether each movement key is currently pressed or not.
        isPressingW = Input.GetKey(KeyCode.W);
        isPressingA = Input.GetKey(KeyCode.A);
        isPressingS = Input.GetKey(KeyCode.S);
        isPressingD = Input.GetKey(KeyCode.D);
    }

    Vector2 GetDashDirection()
    {
        if (isPressingW)
        {
            if (isPressingA)
            {
                return new Vector2(-1, 1).normalized; //up and left
            }
            else if (isPressingD)
            {
                return new Vector2(1, 1).normalized; //up and right
            }
            else
            {
                return new Vector2(0, 1).normalized; //up
            }
        }
        else if (isPressingS)
        {
            if (isPressingA)
            {
                return new Vector2(-1, -1).normalized; //down and left
            }
            else if (isPressingD)
            {
                return new Vector2(1, -1).normalized; //down and right
            }
            else
            {
                return new Vector2(0, -1).normalized; //down
            }
        }
        else if (isPressingA)
        {
            return new Vector2(-1, 0).normalized; //left
        }
        else if (isPressingD)
        {
            return new Vector2(1, 0).normalized; //right
        }

        return Vector2.zero;
    }
}
