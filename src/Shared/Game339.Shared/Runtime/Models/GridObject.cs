using System;
using Game339.Shared.Infastructure.DataTypes;

namespace Game339.Shared.Models
{
    public class GridObject
    {
        public GridPosition Position { get; protected set; }

        public static Action<GridObject> CreateEvent;

        protected GridObject(int row, int column)
        {
            Position = (row, column);
            CreateEvent?.Invoke(this);
        }
    }
}