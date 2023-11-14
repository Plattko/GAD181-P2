using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    static float knockbackDuration = 1f;
    
    public static IEnumerator DoKnockback(float knockbackPower, Rigidbody2D rb, Transform attacker, Transform target)
    {
        Debug.Log("Called knockback.");
        Debug.Log("KnockbackPower is: " + knockbackPower);
        Debug.Log("Rigidbody is: " + rb);
        Debug.Log("Attacker is: " + attacker);
        Debug.Log("Target is: " + target);

        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.time;
            Vector2 direction = (target.position - attacker.position).normalized;
            rb.AddForce(direction *  knockbackPower);
        }
        yield return null;
    }
}
