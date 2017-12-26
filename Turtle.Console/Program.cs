using System;
using System.Linq;

namespace Turtle.ConsoleApp
{
    class Program
    {        
        static void Main(string[] args)
        {
            var environmentFile = Constants.DEFAULT_ENV_FILE;
            var movesFile = Constants.DEFAULT_MOVES_FILE;

            switch (args.Length)
            {
                case 0:
                    Console.WriteLine($"No args were passed in, so defaulting to:\n{Constants.DEFAULT_ENV_FILE}\n{Constants.DEFAULT_MOVES_FILE}\n");
                    break;
                case 2:
                    environmentFile = args.First();
                    movesFile = args.Skip(1).First();
                    break;
                default:
                    Console.WriteLine($"Usage: {Constants.USAGE}");
                    Exit();
                    break;
            }
            
            var environment = EnvironmentBuilder.From(environmentFile)
                .OnIOError(DisplayExcMessageAndExit)
                .AndOnParsingError(DisplayExpectedFormatAndExit)
                .Build();

            var commands = new MovesBuilder()
                .From(movesFile)
                .OnIOError(DisplayExcMessageAndExit)
                .OnParsingError(DisplayExpectedFormatAndExit)
                .Build();

            var result = new Simulation(environment, commands).Run();
            Console.WriteLine(result);
        }

        private static Action<Exception, string, string> DisplayExpectedFormatAndExit =>
            (exc, path, expectedFormat) =>
            {
                Console.WriteLine($"An error occurred while parsing {path}: {exc.Message}");
                Console.WriteLine();

                Console.WriteLine($"Please make sure the input data is in the following format:");
                Console.WriteLine();

                Console.WriteLine(expectedFormat);
                Console.WriteLine();

                Exit();
            };

        private static Action<Exception> DisplayExcMessageAndExit =>
            exc =>
            {
                Console.WriteLine(exc.Message);
                Exit();
            };

        private static void Exit()
        {
            Environment.Exit(1);
        }
    }
}
