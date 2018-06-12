public class EndReactionStep : TextReactionStep {

    protected override void React()
    {
        node.grid.LaunchEnding(text);
    }

}
