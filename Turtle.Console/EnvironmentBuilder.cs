using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Turtle.ConsoleApp
{
    interface EnvironmentBuilderFromFile
    {
        EnvironmentBuilderWithIOErrorHandler OnIOError(Action<Exception> errorHandler);
        EnvironmentBuilderWithParsingErrorHandler OnParsingError(Action<Exception, string, string> errorHandler);
    }

    interface EnvironmentBuilderWithIOErrorHandler
    {
        EnvironmentBuilderWithIOAndParsingErrorHandler AndOnParsingError(Action<Exception, string, string> errorHandler);
    }

    interface EnvironmentBuilderWithParsingErrorHandler
    {
        EnvironmentBuilderWithIOAndParsingErrorHandler AndOnIOError(Action<Exception> errorHandler);
    }

    interface EnvironmentBuilderWithIOAndParsingErrorHandler
    {
        TurtleEnvironment Build();
    }

    class EnvironmentBuilder :
        EnvironmentBuilderFromFile,
        EnvironmentBuilderWithIOErrorHandler,
        EnvironmentBuilderWithParsingErrorHandler,
        EnvironmentBuilderWithIOAndParsingErrorHandler
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

        private string _path;

        private Action<Exception> _onIoError;
        private Action<Exception, string, string> _onParsingError;

        private EnvironmentBuilder() { }

        private EnvironmentBuilder(string path)
        {
            _path = path;
        }

        public static EnvironmentBuilderFromFile From(string path)
        {
            return new EnvironmentBuilder(path);
        }

        public EnvironmentBuilderWithIOErrorHandler OnIOError(Action<Exception> doThis)
        {
            _onIoError = doThis;
            return this;
        }

        public EnvironmentBuilderWithParsingErrorHandler OnParsingError(Action<Exception, string, string> doThis)
        {
            _onParsingError = doThis;
            return this;
        }

        public EnvironmentBuilderWithIOAndParsingErrorHandler AndOnIOError(Action<Exception> doThis)
        {
            _onIoError = doThis;
            return this;
        }

        public EnvironmentBuilderWithIOAndParsingErrorHandler AndOnParsingError(Action<Exception, string, string> doThis)
        {
            _onParsingError = doThis;
            return this;
        }

        public TurtleEnvironment Build()
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
