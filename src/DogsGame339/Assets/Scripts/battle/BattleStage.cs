
using Game.Runtime;
using Game339.Shared.Models;
using UnityEngine.SceneManagement;

namespace battle
{
    public static class BattleStage
    {
        private static (PlayerDog playerDog, EnemyDog enemyDog) _combatants;

        public static PlayerDog PlayerDog { set => _combatants.playerDog = value; }
        public static EnemyDog EnemyDog { set => _combatants.enemyDog = value; }

        private static bool _isPlayersTurn;
        
        public static void StartBattle(DogCard opponent)
        {
            opponent.SetCharacter(ServiceResolver.Resolve<GameState>().BadGuy);
            
            SceneManager.LoadScene("combatScene");
        }

        public static void NextTurn()
        {
            _isPlayersTurn = !_isPlayersTurn;
            if (!_isPlayersTurn) _combatants.enemyDog.TakeTurn();
        }

        public static void EndBattle()
        {
            SceneManager.LoadScene("walkingScene");
        }
    }
}
