namespace Game339.Shared.Models
{
    public abstract class Character : GridObject
    {
        public abstract string Name { get; }

        protected Character(int row, int column) : base(row, column) {}

        public void Move(GridPosition.Direction direction)
        {
            Position = GridPosition.Calculate(Position, direction);
        }
    }
}