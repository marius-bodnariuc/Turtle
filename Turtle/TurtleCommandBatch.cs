using System.Collections.Generic;
using System.Linq;

namespace Turtle
{
    /// <summary>
    /// Only created this for better readability
    /// (to avoid passing around a generic collection of ITurtleCommand)
    /// </summary>
    public class TurtleCommandBatch
    {
        public List<ITurtleCommand> Commands { get; private set; }

        public TurtleCommandBatch(IEnumerable<ITurtleCommand> commands)
        {
            Commands = commands.ToList();
        }
    }
}
