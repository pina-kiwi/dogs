using Game339.Shared.Infastructure;
using Game339.Shared.Infastructure.DataTypes;
using Game339.Shared.Models;

namespace Game339.Shared.Services
{
    public interface I_MoveService
    {
        (int numRows, int numColumns) GridDimensions { get; }
        
        bool IsValidPosition(int row, int column);
        bool IsValidPosition(GridPosition position);
        
        void Move(GridEntity gridEntity, GridPosition position);
        void MoveRandom(GridEntity gridEntity, int tilesToMove);
    }
}