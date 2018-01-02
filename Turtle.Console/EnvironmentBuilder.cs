using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Turtle.ConsoleApp
{
    class EnvironmentBuilder : Builder<TurtleEnvironment>
    {
        private static readonly string ENV_FILE_FORMAT = @"[n] [m]
[start_x] [start_y]
[exit_x] [exit_y]
[direction]
[mine_x] [mine_y]
[mine_x] [mine_y]
...

where:

[n] is an integer representing the number of lines in the environment
[m] is an integer representing the number of columns in the environment
[start_x] and [start_y] are integers representing the x and y coordinates of the start position
[exit_x]  and [exit_y]  are integers representing the x and y coordinates of the exit position
[direction] is one of the following strings: North, South, East, West
[mine_x] [mine_y] are integers representing the x and y coordinates of mines; there are as many such lines as there are mines";

        private EnvironmentBuilder(string path) : base(path) { }

        public static EnvironmentBuilder From(string path)
        {
            return new EnvironmentBuilder(path);
        }

        public override TurtleEnvironment Build()
        {
            try
            {
                using (var streamReader = new StreamReader(_path))
                {
                    var integerTokens = streamReader.ReadLine()
                        .PipeInto(SplitLineIntoIntegerTokens);

                    var n = integerTokens.First();
                    var m = integerTokens.Skip(1).First();
                    
                    var startPosition = streamReader.ReadLine()
                        .PipeInto(SplitLineIntoIntegerTokens)
                        .PipeInto(PositionFromIntTokenPair);
                    
                    var exitPosition = streamReader.ReadLine()
                        .PipeInto(SplitLineIntoIntegerTokens)
                        .PipeInto(PositionFromIntTokenPair);
                    
                    var initialDirection = streamReader.ReadLine()
                        .PipeInto(line => (Direction)Enum.Parse(typeof(Direction), line, ignoreCase: true));

                    var mines = streamReader.ReadToEnd()
                        .Split(new [] { "\n" }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(SplitLineIntoIntegerTokens)
                        .Select(PositionFromIntTokenPair)
                        .ToList();

                    return new TurtleEnvironment(n, m, startPosition, exitPosition, initialDirection, mines);
                }
            }
            catch(Exception exc) when (exc is FileNotFoundException || exc is IOException)
            {
                _onIoError(exc);
            }
            catch(Exception exc)
            {
                _onParsingError(exc, _path, ENV_FILE_FORMAT);
            }

            return null;
        }

        private IEnumerable<int> SplitLineIntoIntegerTokens(string line)
        {
            return line
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(token => int.Parse(token));
        }

        private Position PositionFromIntTokenPair(IEnumerable<int> tokens)
        {
            return new Position(tokens.First(), tokens.Skip(1).First());
        }
    }
}
