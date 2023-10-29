using UnityEngine;

public class GoblinChaseState : GoblinBaseState
{
    public override void EnterState(GoblinStateManager goblin)
    {
        Debug.Log("Entered the Chase State!");
    }

    public override void UpdateState(GoblinStateManager goblin)
    {
        // Direction is the direction of the player from the goblin

        // If player is in attack zone, enter the attack state
        // goblin.SwitchState(goblin.AttackState);
    }
    public override void FixedUpdateState(GoblinStateManager goblin)
    {
        // Move goblin in that direction
    }

    public override void OnCollisionEnter2D(GoblinStateManager goblin, Collision2D collision)
    {

    }
}
