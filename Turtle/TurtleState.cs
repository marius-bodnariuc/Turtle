using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turtle
{
    public class TurtleState
    {
        public Position Position { get; private set; }
        public Direction Direction { get; private set; }

        public TurtleState(Position position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }
    }
}
