using Game339.Shared.Diagnostics;
using Game339.Shared.Models;

namespace Game339.Shared.Services.Implementation
{
    public class TurnService : I_TurnService
    {
        private readonly IGameLog _log;
        private readonly GameState _game;

        public TurnService(GameState gameState, IGameLog gameLog)
        {
            _log = gameLog;
            _game = gameState;
        }

        public Character GetCharacterOnMove()
        {
            int numActors = _game.Enemies.Count + 1;
            int turnIndex = _game.CurrentTurn.Value % numActors;

            if (turnIndex == 0) return _game.Player;
            return _game.Enemies[turnIndex-1];
        }
        
        public void EndCurrentTurn()
        {
            _game.AdvanceTurn();
        }
    }
}