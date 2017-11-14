using System;

namespace Turtle.ConsoleApp
{
    public static class Extensions
    {
        public static TOut PipeInto<TIn, TOut>(this TIn input, Func<TIn, TOut> function)
        {
            return function(input);
        }
    }
}
