using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestValueReactionStep : ReactionStep
{
    public Resource resource;

    public enum Condition
    {
        superiorThan,
        inferiorThan,
        equals
    }
    public Condition condition;

    public int value;

    protected override void React()
    {
        switch (condition)
        {
            case Condition.superiorThan:
                {
                    if (node.grid.globalValues.CheckIfValueStrictlySuperior(resource, value))
                    {
                        node.animator.SetTrigger(DialogParameters.valueConditionMetString);
                    } else
                    {
                        node.animator.SetTrigger(DialogParameters.valueConditionNotMetString);
                    }                    
                    break;
                }
            case Condition.inferiorThan:
                {
                    if (node.grid.globalValues.CheckIfValueStrictlyInferior(resource, value))
                    {
                        node.animator.SetTrigger(DialogParameters.valueConditionMetString);
                    }
                    else
                    {
                        node.animator.SetTrigger(DialogParameters.valueConditionNotMetString);
                    }
                    break;
                }
            case Condition.equals:
                {
                    if (node.grid.globalValues.CheckIfValueEquals(resource, value))
                    {
                        node.animator.SetTrigger(DialogParameters.valueConditionMetString);
                    }
                    else
                    {
                        node.animator.SetTrigger(DialogParameters.valueConditionNotMetString);
                    }
                    break;
                }
            default:
                break;
        }
    }
}
