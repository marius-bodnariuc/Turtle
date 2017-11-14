using System;
using System.IO;
using System.Linq;

namespace Turtle.ConsoleApp
{
    class MovesBuilder
    {
        public static readonly string MOVES_FILE_FORMAT = @"[command][command][command]...

where [command] is one of the following characters: M, R
M stands for Move
R stands for Rotate";

        private string _path;

        private Action<Exception> _onIoError;
        private Action<Exception, string, string> _onParsingError;

        public MovesBuilder() { }

        public MovesBuilder From(string path)
        {
            _path = path;
            return this;
        }

        public MovesBuilder OnIOError(Action<Exception> doThis)
        {
            _onIoError = doThis;
            return this;
        }

        public MovesBuilder OnParsingError(Action<Exception, string, string> doThis)
        {
            _onParsingError = doThis;
            return this;
        }

        public TurtleCommandBatch Build()
        {
            try
            {
                using (var streamReader = new StreamReader(_path))
                {
                    var line = streamReader.ReadLine();
                    var commands = line.ToCharArray()
                        .Select(CommandFromChar)
                        .ToList();

                    return new TurtleCommandBatch(commands);
                }
            }
            catch (Exception exc) when (exc is FileNotFoundException || exc is IOException)
            {
                _onIoError(exc);
            }
            catch (Exception exc)
            {
                _onParsingError(exc, _path, MOVES_FILE_FORMAT);
            }

            return null;
        }

        private static ITurtleCommand CommandFromChar(char c)
        {
            switch (c)
            {
                case 'M':
                    return new MoveTurtleCommand();
                case 'R':
                    return new RotateTurtleCommand();
                default:
                    throw new ArgumentException($"Unexpected character encountered: {c}");
            }
        }
    }
}
