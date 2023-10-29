using UnityEngine;

public class GoblinAttackState : GoblinBaseState
{
    private bool animationFinished = false;
    
    public override void EnterState(GoblinStateManager goblin)
    {
        Debug.Log("Entered the Attack State!");
        goblin.animator.SetTrigger("Attack");
    }

    public override void UpdateState(GoblinStateManager goblin)
    {
        AnimatorStateInfo animStateInfo = goblin.animator.GetCurrentAnimatorStateInfo(0);
        float NTime = animStateInfo.normalizedTime;

        if (NTime > 1.0f)
        {
            animationFinished = true;
        }

        if (animationFinished)
        {
            goblin.SwitchState(goblin.PatrolState);
        }
    }

    public override void FixedUpdateState(GoblinStateManager goblin)
    {

    }
}
