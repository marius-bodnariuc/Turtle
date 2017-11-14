using Moq;
using NUnit.Framework;

namespace Turtle.Core.Test
{
    /// <summary>
    /// TurtleController was used in the beginning, but I gave up on it,
    /// as it felt like I was having one too many objects.
    /// 
    /// Left here for reference.
    /// </summary>
    [TestFixture]
    public class TurtleControllerTest
    {
        [Test]
        public void Controller_ExecutesMoveCommand()
        {
            var turtle = new Mock<ITurtle>();
            turtle.Setup(t => t.Move());

            var controller = new TurtleController(turtle.Object);
            controller.ProcessCommand(new MoveTurtleCommand());

            turtle.Verify(t => t.Move());
        }

        [Test]
        public void Controller_ExecutesRotateCommand()
        {
            var turtle = new Mock<ITurtle>();
            turtle.Setup(t => t.Rotate());

            var controller = new TurtleController(turtle.Object);
            controller.ProcessCommand(new RotateTurtleCommand());

            turtle.Verify(t => t.Rotate());
        }
    }
}
