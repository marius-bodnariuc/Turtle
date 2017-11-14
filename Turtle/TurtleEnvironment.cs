using System.Collections.Generic;
using System.Linq;

namespace Turtle
{
    public class TurtleEnvironment
    {
        public Position StartPosition { get; private set; }
        public Position ExitPosition { get; private set; }
        public Direction InitialDirection { get; private set; }

        /// <summary>
        /// Alternatively, just keep a list/dictionary of Mine positions
        /// </summary>
        private PositionType[,] _grid;

        private int _n;
        private int _m;

        public TurtleEnvironment(
            int n, int m,
            Position startPosition,
            Position exit,
            Direction initialDirection,
            IList<Position> mines)
        {
            StartPosition = startPosition;
            ExitPosition = exit;

            InitialDirection = initialDirection;

            _n = n;
            _m = m;
            _grid = new PositionType[n, m];

            mines.ToList()
                .ForEach(mine => _grid[mine.X, mine.Y] = PositionType.Mine);
        }

        internal PositionType GetPositionType(Position position)
        {
            if (position.Equals(ExitPosition))
            {
                return PositionType.Exit;
            }

            if (position.X < 0 || position.X >= _n || position.Y < 0 || position.Y >= _m)
            {
                return PositionType.Invalid;
            }

            return _grid[position.X, position.Y];
        }
    }
}
