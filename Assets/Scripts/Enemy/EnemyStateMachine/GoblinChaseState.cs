using UnityEngine;

public class GoblinChaseState : GoblinBaseState
{
    Vector2 direction;

    public override void EnterState(GoblinStateManager goblin)
    {
        Debug.Log("Entered the Chase State!");
    }

    public override void UpdateState(GoblinStateManager goblin)
    {
        // Direction is the direction of the player from the goblin
        direction = (goblin.playerTransform.position - goblin.transform.position).normalized;

        // If player leaves the aggro range, enter patrol state
        if ((goblin.playerTransform.position - goblin.transform.position).sqrMagnitude > goblin.aggroRangeSqr)
        {
            goblin.SwitchState(goblin.PatrolState);
        }
        
        // If player is in attack range, enter attack state
        if ((goblin.playerTransform.position - goblin.transform.position).sqrMagnitude < goblin.atkRangeSqr)
        {
            goblin.SwitchState(goblin.AttackState);
        }

        // If health reaches zero, enter dead state
        if (goblin.currentHealth <= 0f)
        {
            goblin.SwitchState(goblin.DeadState);
        }
    }
    public override void FixedUpdateState(GoblinStateManager goblin)
    {
        // Chase physics
        goblin.rb.velocity = goblin.moveSpeed * direction;
    }
}
