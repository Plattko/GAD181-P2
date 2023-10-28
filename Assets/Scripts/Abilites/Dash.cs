using System.Collections;
using UnityEngine;

public class Dash: MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 2f;

    private bool canDash = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(PerformDash());
        }
    }

    IEnumerator PerformDash()
    {
        canDash = false;


        //obtains the direction in which the dash should occur.
        Vector2 dashDirection = GetDashDirection();
        Vector2 startPos = rb.position;
        Vector2 endPos = startPos + dashDirection * dashDistance;

        //records the starting time of the dash.
        float startTime = Time.time;

        //moves the player from the starting position to the end position over the dash duration.
        while (Time.time < startTime + dashDuration)
        {
            rb.MovePosition(Vector2.Lerp(startPos, endPos, (Time.time - startTime) / dashDuration));
            yield return null;
        }

        rb.velocity = Vector2.zero;

        //introduces a cooldown period before the player can dash again
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    Vector2 GetDashDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //if there is no input it defaults to dashing in the direction the player is facing.
        if (horizontalInput == 0 && verticalInput == 0)
        {
            //checks the player's facing direction and returns the appropriate Vector2 for dashing.
            if (Mathf.Approximately(transform.localScale.x, 1f))
                return Vector2.right;
            else
                return Vector2.left;
        }

        return new Vector2(horizontalInput, verticalInput).normalized;
    }
}
