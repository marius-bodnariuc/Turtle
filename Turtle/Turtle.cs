namespace Turtle
{
    internal class Turtle : ITurtle
    {
        public Position CurrentPosition { get; private set; }
        public Direction CurrentDirection { get; private set; }

        public Turtle(Position initialPosition, Direction initialDirection)
        {
            CurrentPosition  = initialPosition;
            CurrentDirection = initialDirection;
        }

        public void Move()
        {
            switch(CurrentDirection)
            {
                case Direction.East:
                    CurrentPosition = new Position(CurrentPosition.X, CurrentPosition.Y + 1);
                    break;
                case Direction.West:
                    CurrentPosition = new Position(CurrentPosition.X, CurrentPosition.Y - 1);
                    break;
                case Direction.North:
                    CurrentPosition = new Position(CurrentPosition.X - 1, CurrentPosition.Y);
                    break;
                case Direction.South:
                    CurrentPosition = new Position(CurrentPosition.X + 1, CurrentPosition.Y);
                    break;
            }
        }

        public void Rotate()
        {
            switch(CurrentDirection)
            {
                case Direction.East:
                    CurrentDirection = Direction.South;
                    break;
                case Direction.South:
                    CurrentDirection = Direction.West;
                    break;
                case Direction.West:
                    CurrentDirection = Direction.North;
                    break;
                case Direction.North:
                    CurrentDirection = Direction.East;
                    break;
            }
        }
    }
}
