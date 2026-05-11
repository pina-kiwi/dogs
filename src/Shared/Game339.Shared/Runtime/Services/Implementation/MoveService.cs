using System;
using System.Collections.Generic;
using System.Linq;
using Game339.Shared.Diagnostics;
using Game339.Shared.Models;
using Direction = Game339.Shared.GridPosition.Direction;

namespace Game339.Shared.Services.Implementation
{
    public class MoveService : I_MoveService
    {
        private readonly IGameLog _log;
        private readonly (int rowNum, int colNum) _gridSize;
        private readonly Random _random;

        public MoveService((int rowNum, int colNum) gridSize, IGameLog gameLog)
        {
            _log = gameLog;
            _gridSize = gridSize;
            _random = new();
        }
        
        public bool IsValidPosition(int row, int column)
        {
            if (0 > row || row >= _gridSize.rowNum) return false;
            if (0 > column || column >= _gridSize.colNum) return false;
            return true;
        }
        
        public bool IsValidPosition(GridPosition position) => IsValidPosition(position.row, position.col);

        public void Move(Character character, GridPosition position)
        {
            int row = Math.Clamp(position.row, 0, _gridSize.rowNum-1);
            int column = Math.Clamp(position.col, 0, _gridSize.colNum-1);
            position = (row, column);
            
            while (character.Position != position) StepTowards(character, position);
            _log.Info($"{character.Name} moved to row {position.row}, column {position.col}");
        }

        private void StepTowards(Character character, GridPosition targetPosition)
        {
            bool reverseCheckOrder = _random.Next(0, 2) == 0;
            var currentPosition = character.Position;
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
            character.Move(moveDirection!.Value);
        }

        public void MoveRandom(Character character, int tilesToMove)
        {
            for (int i = 0; i < tilesToMove; i++)
            {
                StepRandom(character);
            }
            _log.Info($"{character.Name} moved to row {character.Position.row}, column {character.Position.col}");
        }

        private void StepRandom(Character character)
        {
            Direction? randomDirection = null;

            List<Direction> directions = Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList();

            while (directions.Count > 0)
            {
                if (!_IsValidDirection(randomDirection))
                {
                    randomDirection = directions[_random.Next(directions.Count)];
                    directions.Remove(randomDirection.Value);
                }
                else break;
            }

            character.Move(randomDirection!.Value);
            
            bool _IsValidDirection(Direction? direction)
            {
                if (direction == null) return false;
                GridPosition projectedPosition = GridPosition.Calculate(character.Position, direction.Value);
                
                return IsValidPosition(projectedPosition);
            }
        }
    }
}