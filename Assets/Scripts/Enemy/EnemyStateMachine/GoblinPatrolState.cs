using UnityEngine;

public class GoblinPatrolState : GoblinBaseState
{
    public override void EnterState(GoblinStateManager goblin)
    {
        Debug.Log("Entered the Patrol State!");
    }

    public override void UpdateState(GoblinStateManager goblin)
    {
        // If player is not in range, patrol

        // Else, enter chase state
        // goblin.SwitchState(goblin.ChaseState);

        // If currentHealth <= 0, enter dead state
        // goblin.SwitchState(goblin.DeadState);
    }
    public override void FixedUpdateState(GoblinStateManager goblin)
    {
        // Patrol physics
    }

    public override void OnCollisionEnter2D(GoblinStateManager goblin, Collision2D collision)
    {

    }
}
