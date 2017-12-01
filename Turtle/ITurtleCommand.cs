namespace Turtle
{
    public interface ITurtleCommand
    {
        TurtleState Execute(TurtleState turtleState);
    }
}
