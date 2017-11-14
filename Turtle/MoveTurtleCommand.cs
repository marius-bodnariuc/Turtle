namespace Turtle
{
    public class MoveTurtleCommand : ITurtleCommand
    {
        public void Execute(ITurtle turtle)
        {
            turtle.Move();
        }
    }
}
