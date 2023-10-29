using UnityEngine;

public class GoblinAttackState : GoblinBaseState
{
    public override void EnterState(GoblinStateManager goblin)
    {
        Debug.Log("Entered the Attack State!");
    }

    public override void UpdateState(GoblinStateManager goblin)
    {
        
    }

    public override void FixedUpdateState(GoblinStateManager goblin)
    {

    }

    public override void OnCollisionEnter2D(GoblinStateManager goblin, Collision2D collision)
    {

    }
}
