using System;
using System.Linq;
using Game339.Shared.Infastructure.DataTypes;
using Game339.Shared.Infastructure.Diagnostics;
using Game339.Shared.Models;
using Direction = Game339.Shared.Infastructure.DataTypes.GridPosition.Direction;

namespace Game339.Shared.Services.Implementation
{
    public class MoveService : I_MoveService
    {
        private readonly IGameLog _log;
        private readonly Random _random;
        private readonly int _numRows, _numCols;

        public MoveService((int numRows, int numColumns) gridDimensions, IGameLog gameLog)
        {
            if (gridDimensions.numRows < 1 || gridDimensions.numColumns < 1) throw new ArgumentOutOfRangeException(nameof(gridDimensions));
            _log = gameLog;
            _random = new();
            (_numRows, _numCols) = gridDimensions;
        }

        public (int numRows, int numColumns) GridDimensions => (_numRows, _numCols);

        public bool IsValidPosition(int row, int column)
        {
            if (0 > row || row >= _numRows) return false;
            return 0 <= column && column < _numCols;
        }
        
        public bool IsValidPosition(GridPosition position) => IsValidPosition(position.row, position.col);

        public void Move(GridEntity gridEntity, GridPosition position)
        {
            int row = Math.Clamp(position.row, 0, _numRows-1);
            int column = Math.Clamp(position.col, 0, _numCols-1);
            position = (row, column);
            
            while (gridEntity.Position != position) StepTowards(gridEntity, position);
            _log.Info($"{gridEntity.Name} moved to row {position.row}, column {position.col}");
        }

        private void StepTowards(GridEntity gridEntity, GridPosition targetPosition)
        {
            bool reverseCheckOrder = _random.Next(0, 2) == 0;
            GridPosition currentPosition = gridEntity.Position;
            Direction? moveDirection = null;

            if (reverseCheckOrder) goto columnCheck;
            
            rowCheck:
            if (targetPosition.row > currentPosition.row) moveDirection = Direction.Down;
            if (targetPosition.row < currentPosition.row) moveDirection = Direction.Up;
            if (moveDirection != null) goto end;
            
            columnCheck:
            if (targetPosition.col > currentPosition.col) moveDirection = Direction.Right;
            if (targetPosition.col < currentPosition.col) moveDirection = Direction.Left;
            if (moveDirection != null) goto end;
            
            if (reverseCheckOrder) goto rowCheck;
            
            end:
            if (moveDirection != null) gridEntity.Step(moveDirection.Value);
            else throw new NullReferenceException();
        }

        public void MoveRandom(GridEntity gridEntity, int tilesToMove)
        {
            for (int i = 0; i < tilesToMove; i++)
            {
                StepRandom(gridEntity);
            }
            _log.Info($"{gridEntity.Name} moved to row {gridEntity.Position.row}, column {gridEntity.Position.col}");
        }

        private void StepRandom(GridEntity gridEntity)
        {
            Direction? randomDirection = null;

            var directions = Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList();

            while (directions.Count > 0)
            {
                if (!_IsValidDirection(randomDirection))
                {
                    randomDirection = directions[_random.Next(directions.Count)];
                    directions.Remove(randomDirection.Value);
                }
                else break;
            }

            if (randomDirection != null) gridEntity.Step(randomDirection.Value);
            else throw new NullReferenceException();
            
            bool _IsValidDirection(Direction? direction)
            {
                if (direction == null) return false;
                GridPosition projectedPosition = GridPosition.Calculate(gridEntity.Position, direction.Value);
                
                return IsValidPosition(projectedPosition);
            }
        }
    }
}