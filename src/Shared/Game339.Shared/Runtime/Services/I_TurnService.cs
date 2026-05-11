using Game339.Shared.Models;

namespace Game339.Shared.Services
{
    public interface I_TurnService
    {
        Character GetCharacterOnMove();
        void EndCurrentTurn();
    }
}