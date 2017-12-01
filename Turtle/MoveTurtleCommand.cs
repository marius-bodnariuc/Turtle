using System;

namespace Turtle
{
    public class MoveTurtleCommand : ITurtleCommand
    {
        public TurtleState Execute(TurtleState turtleState)
        {
            switch (turtleState.Direction)
            {
                case Direction.East:
                    return MoveEast(turtleState);
                case Direction.West:
                    return new TurtleState(new Position(turtleState.Position.X, turtleState.Position.Y - 1), turtleState.Direction);
                case Direction.North:
                    return new TurtleState(new Position(turtleState.Position.X - 1, turtleState.Position.Y), turtleState.Direction);
                case Direction.South:
                default:
                    return new TurtleState(new Position(turtleState.Position.X + 1, turtleState.Position.Y), turtleState.Direction);
            }
        }

        Func<TurtleState, TurtleState> MoveEast =>
            currentState => new TurtleState(
                new Position(currentState.Position.X, currentState.Position.Y + 1),
                currentState.Direction);
    }
}
