using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerReactionStep : ReactionStep
{
    public string[] answers = new string[4];

    protected override void React()
    {
        node.grid.actionPanel.DisplayAnswers(answers);
    }
}
