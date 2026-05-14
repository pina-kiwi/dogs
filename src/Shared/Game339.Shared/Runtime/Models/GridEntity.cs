using System;
using Game339.Shared.Infastructure.DataTypes;

namespace Game339.Shared.Models
{
    public abstract class GridEntity : GridObject
    {
        public Action<GridPosition> StepEvent;
        public abstract string Name { get; }

        protected GridEntity(int row, int column) : base(row, column) {}

        public void Step(GridPosition.Direction direction)
        {
            GridPosition newPosition = GridPosition.Calculate(Position, direction);
            
            Position = newPosition;
            StepEvent?.Invoke(newPosition);
        }
    }
}