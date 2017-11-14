namespace Turtle
{
    public interface ITurtle
    {
        Direction CurrentDirection { get; }
        Position CurrentPosition { get; }

        void Move();
        void Rotate();
    }
}