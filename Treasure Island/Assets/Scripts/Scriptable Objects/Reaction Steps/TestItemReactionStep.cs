using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItemReactionStep : ReactionStep
{
    public Item item;

    protected override void React()
    {
        if (node.grid.inventory.CheckForItem(item))
        {
            node.animator.SetTrigger("itemConditionMet");
        } else
        {
            node.animator.SetTrigger("itemConditionNotMet");
        }
        
    }
}
