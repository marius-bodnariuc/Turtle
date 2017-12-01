using System;

namespace Turtle
{
    internal static class TurtleController
    {
        internal static TurtleState ProcessCommand(TurtleState currentState, ITurtleCommand command)
        {
            if (command is MoveTurtleCommand)
            {
                return Move(currentState, command as MoveTurtleCommand);
            }
            else
            {
                return Rotate(currentState, command as RotateTurtleCommand);
            }
        }

        private static TurtleState Move(TurtleState currentState, MoveTurtleCommand command)
        {
            switch (currentState.Direction)
            {
                case Direction.East:
                    return MoveEast(currentState, command);
                case Direction.West:
                    return new TurtleState(new Position(currentState.Position.X, currentState.Position.Y - 1), currentState.Direction);
                case Direction.North:
                    return new TurtleState(new Position(currentState.Position.X - 1, currentState.Position.Y), currentState.Direction);
                case Direction.South:
                default:
                    return new TurtleState(new Position(currentState.Position.X + 1, currentState.Position.Y), currentState.Direction);
            }
        }

        private static TurtleState Rotate(TurtleState currentState, RotateTurtleCommand command)
        {
            switch (currentState.Direction)
            {
                case Direction.East:
                    return new TurtleState(currentState.Position, Direction.South);
                case Direction.South:
                    return new TurtleState(currentState.Position, Direction.West);
                case Direction.West:
                    return new TurtleState(currentState.Position, Direction.North);
                case Direction.North:
                default:
                    return new TurtleState(currentState.Position, Direction.East);
            }
        }

        static Func<TurtleState, ITurtleCommand, TurtleState> MoveEast =>
            (currentState, command) => new TurtleState(
                new Position(currentState.Position.X, currentState.Position.Y + 1),
                currentState.Direction);

        // TODO extract Funcs for the other directions as well?
    }
}
