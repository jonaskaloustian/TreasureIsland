using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundReactionStep : ReactionStep
{
    public Sprite sprite;

    protected override void React()
    {
        node.grid.backgroundImage.Fade(sprite);
    }
}
