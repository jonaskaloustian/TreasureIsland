using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReactionStep : StateMachineBehaviour
{
    protected Node node;

    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        node = animator.gameObject.GetComponent<Node>();

        React();
    }

    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(DialogParameters.nextString);
        node.grid.actionPanel.DisplayAnswers(false);
    }

    protected abstract void React();
}
