using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyValueReactionStep : ReactionStep {

    public Resource resource;

	public enum Action
    {
        Add,
        Substract,
        SetTo
    }

    public Action action;

    public int value;

    protected override void React()
    {
        switch (action)
        {
            case Action.Add:
                {
                    node.grid.globalValues.AddToValue(resource, value);
                    break;
                }
            case Action.Substract:
                {
                    node.grid.globalValues.SusbstractFromValue(resource, value);
                    break;
                }
            case Action.SetTo:
                {
                    node.grid.globalValues.SetValue(resource, value);
                    break;
                }
            default:
                break;
        }
        node.grid.globalValues.TestCriticalValues();
    }
}
