using NUnit.Framework;
using System.Collections.Generic;

namespace Turtle.Core.Test
{
    /// <summary>
    /// Basic test suite
    /// I would add more cases around, say, scenarios with grids of small dimensions
    /// </summary>
    [TestFixture]
    public class SimulationTest
    {
        [Test]
        public void Simulation_EndsIn_Success()
        {
            var environment = new TurtleEnvironment(5, 6,
                new Position(0, 0),
                new Position(4, 5),
                Direction.East,
                new List<Position>
                {
                    new Position(1, 1),
                    new Position(2, 2),
                    new Position(3, 3),
                    new Position(4, 4)
                });

            var commands = new TurtleCommandBatch(
                new List<ITurtleCommand>
                {
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new RotateTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand()
                });

            var simulation = new Simulation(environment, commands);
            Assert.That(simulation.Run(), Is.EqualTo(SimulationResult.Success));
        }

        [Test]
        public void Simulation_EndsIn_StillInDanger()
        {
            var environment = new TurtleEnvironment(5, 6,
                new Position(0, 0),
                new Position(4, 5),
                Direction.East,
                new List<Position>
                {
                    new Position(1, 1),
                    new Position(2, 2),
                    new Position(3, 3),
                    new Position(4, 4)
                });

            var commands = new TurtleCommandBatch(
                new List<ITurtleCommand>
                {
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new RotateTurtleCommand(),
                    new MoveTurtleCommand()
                });

            var simulation = new Simulation(environment, commands);
            Assert.That(simulation.Run(), Is.EqualTo(SimulationResult.StillInDanger));
        }

        [Test]
        public void Simulation_EndsIn_MineHit()
        {
            var environment = new TurtleEnvironment(5, 6,
                new Position(0, 0),
                new Position(4, 5),
                Direction.East,
                new List<Position>
                {
                    new Position(1, 1),
                    new Position(2, 2),
                    new Position(3, 3),
                    new Position(4, 4)
                });

            var commands = new TurtleCommandBatch(
                new List<ITurtleCommand>
                {
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new RotateTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand()
                });

            var simulation = new Simulation(environment, commands);
            Assert.That(simulation.Run(), Is.EqualTo(SimulationResult.MineHit));
        }

        [Test]
        public void Simulation_EndsIn_OutOfBounds_NegativeOnX()
        {
            var environment = new TurtleEnvironment(5, 6,
                new Position(0, 0),
                new Position(4, 5),
                Direction.West,
                new List<Position>());

            var commands = new TurtleCommandBatch(
                new List<ITurtleCommand>
                {
                    new MoveTurtleCommand()
                });

            var simulation = new Simulation(environment, commands);
            Assert.That(simulation.Run(), Is.EqualTo(SimulationResult.OutOfBounds));
        }

        [Test]
        public void Simulation_EndsIn_OutOfBounds_TooFarAwayOnX()
        {
            var environment = new TurtleEnvironment(2, 2,
                new Position(0, 0),
                new Position(1, 1),
                Direction.East,
                new List<Position>());

            var commands = new TurtleCommandBatch(
                new List<ITurtleCommand>
                {
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand()
                });

            var simulation = new Simulation(environment, commands);
            Assert.That(simulation.Run(), Is.EqualTo(SimulationResult.OutOfBounds));
        }

        [Test]
        public void Simulation_EndsIn_OutOfBounds_NegativeOnY()
        {
            var environment = new TurtleEnvironment(5, 6,
                new Position(0, 0),
                new Position(4, 5),
                Direction.North,
                new List<Position>());

            var commands = new TurtleCommandBatch(
                new List<ITurtleCommand>
                {
                    new MoveTurtleCommand()
                });

            var simulation = new Simulation(environment, commands);
            Assert.That(simulation.Run(), Is.EqualTo(SimulationResult.OutOfBounds));
        }

        [Test]
        public void Simulation_EndsIn_OutOfBounds_TooFarAwayOnY()
        {
            var environment = new TurtleEnvironment(2, 2,
                new Position(0, 0),
                new Position(2, 2),
                Direction.South,
                new List<Position>());

            var commands = new TurtleCommandBatch(
                new List<ITurtleCommand>
                {
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand(),
                    new MoveTurtleCommand()
                });

            var simulation = new Simulation(environment, commands);
            Assert.That(simulation.Run(), Is.EqualTo(SimulationResult.OutOfBounds));
        }
    }
}
