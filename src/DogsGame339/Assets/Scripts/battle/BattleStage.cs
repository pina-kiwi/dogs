
namespace battle
{
    public static class BattleStage
    {
        private static (PlayerDog playerDog, EnemyDog enemyDog) _combatants;

        private static bool _isPlayersTurn;
        
        public static void StartBattle(PlayerDog playerDog, EnemyDog enemyDog)
        {
            _combatants.playerDog = playerDog;
            _combatants.enemyDog = enemyDog;

            _isPlayersTurn = playerDog.Speed >= enemyDog.Speed;
        }
    }
}
