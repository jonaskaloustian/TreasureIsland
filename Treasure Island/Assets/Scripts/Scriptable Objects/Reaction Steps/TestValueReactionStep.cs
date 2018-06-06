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
                    node.grid.globalValues.CheckIfValueStrictlySuperior(resource, value);
                    break;
                }
            case Condition.inferiorThan:
                {
                    node.grid.globalValues.CheckIfValueStrictlyInferior(resource, value);
                    break;
                }
            case Condition.equals:
                {
                    node.grid.globalValues.CheckIfValueEquals(resource, value);
                    break;
                }
            default:
                break;
        }
    }
}
