using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReactionStep : ReactionStep {

	public enum Action
    {
        Add,
        Remove
    }

    public Item item;

    public Action action;

    protected override void React()
    {
        switch (action)
        {
            case Action.Add:
                {
                    node.grid.inventory.AddItem(item);
                    break;
                }
            case Action.Remove:
                {
                    node.grid.inventory.RemoveItem(item);
                    break;
                }
            default:
                break;
        }
    }
}
