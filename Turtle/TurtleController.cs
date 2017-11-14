namespace Turtle
{
    /// <summary>
    /// This was used in the beginning, but I gave up on it,
    /// as it felt like I was having one too many objects.
    /// 
    /// Left here for reference.
    /// </summary>
    internal class TurtleController
    {
        private ITurtle _turtle;

        public TurtleController(ITurtle turtle)
        {
            _turtle = turtle;
        }

        public void ProcessCommand(ITurtleCommand command)
        {
            command.Execute(_turtle);
        }
    }
}
