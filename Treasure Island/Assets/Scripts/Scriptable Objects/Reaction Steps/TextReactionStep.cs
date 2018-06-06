using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextReactionStep : ReactionStep {

    public string text;

    protected override void React()
    {
        node.grid.actionPanel.DisplayDescription(text);
    }

}
