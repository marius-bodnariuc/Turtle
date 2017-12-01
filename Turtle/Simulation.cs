﻿using System;

namespace Turtle
{
    /// <summary>
    /// This class performs the whole simulation of the Turtle's itinerary.
    /// 
    /// I decided to make it 'all-or-nothing', as in: 
    /// pass in all the commands and get the final result back.
    /// 
    /// Therefore, the API isn't very flexible - it is very problem-specific, instead
    /// (there's no control over potential actions to be taken in between moves)
    /// </summary>
    public class Simulation
    {
        private TurtleEnvironment _environment;
        private TurtleCommandBatch _commands;

        private TurtleState _turtleState;
        
        public Simulation(TurtleEnvironment environment, TurtleCommandBatch commandBatch)
        {
            _environment = environment;
            _commands = commandBatch;
        }

        public SimulationResult Run()
        {
            _turtleState = new TurtleState(_environment.StartPosition, _environment.InitialDirection);

            var currentPositionType = PositionType.Clear;
            foreach (var command in _commands.Commands)
            {
                currentPositionType = UpdateTurtleState(command);
                if (currentPositionType != PositionType.Clear)
                {
                    break;
                }
            }

            var result = ToSimulationResult(currentPositionType);
            return result;
        }

        private static SimulationResult ToSimulationResult(PositionType positionType)
        {
            switch (positionType)
            {
                case PositionType.Exit:
                    return SimulationResult.Success;
                case PositionType.Mine:
                    return SimulationResult.MineHit;
                case PositionType.Invalid:
                    return SimulationResult.OutOfBounds;
                default:
                    return SimulationResult.StillInDanger;
            }
        }

        private PositionType UpdateTurtleState(ITurtleCommand command)
        {
            _turtleState = TurtleController.ProcessCommand(_turtleState, command);
            return _environment.GetPositionType(_turtleState.Position);
        }
    }
}
