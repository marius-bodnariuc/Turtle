namespace Turtle
{
    public class RotateTurtleCommand : ITurtleCommand
    {
        public TurtleState Execute(TurtleState turtleState)
        {
            switch (turtleState.Direction)
            {
                case Direction.East:
                    return new TurtleState(turtleState.Position, Direction.South);
                case Direction.South:
                    return new TurtleState(turtleState.Position, Direction.West);
                case Direction.West:
                    return new TurtleState(turtleState.Position, Direction.North);
                case Direction.North:
                default:
                    return new TurtleState(turtleState.Position, Direction.East);
            }
        }
    }
}
