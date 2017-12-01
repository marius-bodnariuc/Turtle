namespace Turtle
{
    internal static class TurtleController
    {
        internal static TurtleState ProcessCommand(TurtleState currentState, ITurtleCommand command)
        {
            return command.Execute(currentState);
        }
    }
}
