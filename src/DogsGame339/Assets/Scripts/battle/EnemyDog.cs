using Game.Runtime;
using Game339.Shared.Models;
using UnityEngine;

namespace battle
{
    public class EnemyDog : BattleDog
    {
        protected override Character Owner => ServiceResolver.Resolve<GameState>().BadGuy;

        private void Start()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            BattleManager.IsPlayersTurn.ChangeEvent += TakeTurn;
        }

        private void TakeTurn(bool isPlayersTurn)
        {
            if (!isPlayersTurn) Attack();
        }
        
        protected override void Attack()
        {
            Debug.Log("Enemy dog attacks!");
            ServiceResolver.Resolve<GameState>().Player.TakeDamage(Owner.AttackPower.Value);
            BattleManager.EndBattle();
        }

        protected override void Flee()
        {
            Debug.Log("Enemy dog turns tail and runs away!");
            base.Flee();
        }
    }
}
