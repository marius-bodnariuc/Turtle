using NUnit.Framework;

namespace Turtle.Core.Test
{
    [TestFixture]
    public class TurtleTest
    {
        [Test]
        public void Turtle_Rotates()
        {
            var turtle = new Turtle(new Position(1, 1), Direction.East);

            turtle.Rotate();
            Assert.That(turtle.CurrentDirection, Is.EqualTo(Direction.South));

            turtle.Rotate();
            Assert.That(turtle.CurrentDirection, Is.EqualTo(Direction.West));

            turtle.Rotate();
            Assert.That(turtle.CurrentDirection, Is.EqualTo(Direction.North));

            turtle.Rotate();
            Assert.That(turtle.CurrentDirection, Is.EqualTo(Direction.East));
        }

        [Test]
        public void Turtle_Moves_East()
        {
            var turtle = new Turtle(new Position(0, 0), Direction.East);

            turtle.Move();
            Assert.That(turtle.CurrentPosition, Is.EqualTo(new Position(0, 1)));
        }

        [Test]
        public void Turtle_Moves_West()
        {
            var turtle = new Turtle(new Position(0, 1), Direction.West);

            turtle.Move();
            Assert.That(turtle.CurrentPosition, Is.EqualTo(new Position(0, 0)));
        }

        [Test]
        public void Turtle_Moves_North()
        {
            var turtle = new Turtle(new Position(1, 0), Direction.North);

            turtle.Move();
            Assert.That(turtle.CurrentPosition, Is.EqualTo(new Position(0, 0)));
        }

        [Test]
        public void Turtle_Moves_South()
        {
            var turtle = new Turtle(new Position(0, 0), Direction.South);

            turtle.Move();
            Assert.That(turtle.CurrentPosition, Is.EqualTo(new Position(1, 0)));
        }
    }
}
