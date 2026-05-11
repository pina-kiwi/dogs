using System;

namespace Game339.Shared
{
    public readonly struct GridPosition : IEquatable<GridPosition>
    {
        public int row { get; }
        public int col { get; }
        
        GridPosition(int row, int column)  => (this.row, col) = (row, column);

        public enum Direction
        {
            Up, North = Up,
            Right, East = Right,
            Down, South = Down,
            Left, West = Left
        }
        
        public static GridPosition Calculate(GridPosition initialPosition, Direction direction)
        {
            initialPosition += direction switch
            {
                Direction.Up => (-1, 0),
                Direction.Down => (1, 0),
                Direction.Left => (0, -1),
                Direction.Right => (0, 1),
                _ => (0, 0)
            };
            return initialPosition;
        }

        public static implicit operator GridPosition((int row, int col) position) => new(position.row, position.col);
        
        public static GridPosition operator +(GridPosition a, GridPosition b) => new(a.row + b.row, a.col + b.col);
        public static GridPosition operator -(GridPosition a, GridPosition b) => new(a.row - b.row, a.col - b.col);
        public static GridPosition operator *(GridPosition a, GridPosition b) => new(a.row * b.row, a.col * b.col);
        public static GridPosition operator /(GridPosition a, GridPosition b) => new(a.row / b.row, a.col / b.col);
        public static GridPosition operator %(GridPosition a, GridPosition b) => new(a.row % b.row, a.col % b.col);
        
        public static bool operator ==(GridPosition a, GridPosition b) => a.Equals(b);
        public static bool operator !=(GridPosition a, GridPosition b) => !a.Equals(b);
        
        public bool Equals(GridPosition other) => row == other.row && col == other.col;
        public override bool Equals(object obj) => obj is GridPosition other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(row, col);
    }
}