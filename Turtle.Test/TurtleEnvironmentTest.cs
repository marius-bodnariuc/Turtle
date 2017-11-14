using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Turtle.Core.Test
{
    [TestFixture]
    public class TurtleEnvironmentTest
    {
        [Test]
        public void Environment_HoldsMinesPositionsCorrectly()
        {
            var n = 5;
            var m = 6;

            var startPosition = new Position(0, 0);
            var exitPosition = new Position(4, 5);

            var minePositions = new List<Position>
            {
                new Position(1, 1),
                new Position(2, 2),
                new Position(3, 3),
            };

            var environment = new TurtleEnvironment(n, m, 
                startPosition, exitPosition, 
                Direction.East, minePositions);

            var positionTypes =
                Enumerable.Range(0, n)
                .Zip(Enumerable.Range(0, m), (fst, snd) => new Position(fst, snd))
                .Select(pos => Tuple.Create(pos, environment.GetPositionType(pos)))
                .ToList();

            var positionsThatShouldBeOccupiedByMines = positionTypes
                .Where(posType => minePositions.Contains(posType.Item1))
                .ToList();

            var positionsThatShouldBeFree = positionTypes
                .Where(posType => !minePositions.Contains(posType.Item1))
                .ToList();

            // TODO create a Partition extension method for the above?

            Assert.That(positionsThatShouldBeOccupiedByMines.Select(p => p.Item2),
                Has.All.EqualTo(PositionType.Mine));

            Assert.That(positionsThatShouldBeFree.Select(p => p.Item2),
                Has.All.EqualTo(PositionType.Clear));
        }
    }
}
