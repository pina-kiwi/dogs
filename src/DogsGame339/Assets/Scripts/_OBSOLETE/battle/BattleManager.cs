using Game.Runtime;
using Game339.Shared;
using Game339.Shared.Models;

namespace battle
{
    public static class BattleManager
    {
        public static ObservableValue<bool> IsPlayersTurn { get; } = new(true);

        public static void NextTurn()
        {
            IsPlayersTurn.Value = !IsPlayersTurn.Value;
        }

        public static void EndBattle()
        {
            ServiceResolver.Resolve<GameState>().IsInCombat.Value = false;
        }
    }
}