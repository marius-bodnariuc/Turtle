namespace Turtle
{
    public class Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj is Position)
            {
                var other = obj as Position;
                return X == other.X && Y == other.Y;
            }

            return false;
        }

        // https://stackoverflow.com/questions/5221396/what-is-an-appropriate-gethashcode-algorithm-for-a-2d-point-struct-avoiding
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
        # endregion Overrides
    }
}
