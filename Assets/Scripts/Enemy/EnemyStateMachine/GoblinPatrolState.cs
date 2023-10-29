using UnityEngine;

public class GoblinPatrolState : GoblinBaseState
{
    
    public override void EnterState(GoblinStateManager goblin)
    {
        Debug.Log("Entered the Patrol State!");
    }

    public override void UpdateState(GoblinStateManager goblin)
    {
        // patrol

        // If player enters aggro range, enter chase state
        if ((goblin.playerTransform.position - goblin.transform.position).sqrMagnitude < goblin.aggroRangeSqr)
        {
            goblin.SwitchState(goblin.ChaseState);
        }

        // If health reaches zero, enter dead state
        if (goblin.currentHealth <= 0f)
        {
            goblin.SwitchState(goblin.DeadState);
        }
    }
    public override void FixedUpdateState(GoblinStateManager goblin)
    {
        // Patrol physics
    }
}
