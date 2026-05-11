using Game339.Shared.Models;

namespace Game339.Shared.Services
{
    public interface I_MoveService
    {
        bool IsValidPosition(int row, int column);
        bool IsValidPosition(GridPosition position);
        
        void Move(Character character, GridPosition position);
        void MoveRandom(Character character, int tilesToMove);
    }
}