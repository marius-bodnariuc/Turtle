namespace Turtle
{
    public class RotateTurtleCommand : ITurtleCommand
    {
        public void Execute(ITurtle turtle)
        {
            turtle.Rotate();
        }
    }
}
