using System;

namespace Game339.Shared
{
    public class GridObject
    {
        private readonly ObservableValue<GridPosition> _position;

        public GridPosition Position
        {
            get => _position.Value;
            protected set => _position.Value = value;
        }
            
        private void OnMoveEvent(GridPosition value) => MoveEvent?.Invoke(value);
        public Action<GridPosition> MoveEvent;

        public static Action<GridObject> CreateEvent;

        protected GridObject(int row, int column)
        {
            _position =  new((row, column));
            _position.ChangeEvent += OnMoveEvent;
            CreateEvent?.Invoke(this);
        }
    }
}