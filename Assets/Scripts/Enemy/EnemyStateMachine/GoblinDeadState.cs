using UnityEngine;

public class GoblinDeadState : GoblinBaseState
{
    public override void EnterState(GoblinStateManager goblin)
    {
        Debug.Log("Entered the Dead State!");
    }

    public override void UpdateState(GoblinStateManager goblin)
    {

    }

    public override void FixedUpdateState(GoblinStateManager goblin)
    {
        
    }
}
