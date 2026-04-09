using Game.Runtime;
using Game339.Shared.Models;
using UnityEngine;

namespace battle
{
    public class PlayerDog : BattleDog
    {
        protected override Character Owner => ServiceResolver.Resolve<GameState>().Player;

        protected override void Attack()
        {
            Debug.Log("Player's dog attacks!");
            opponent.TakeDamage(Owner.AttackPower.Value);
            base.Attack();
        }

        protected override void Flee()
        {
            Debug.Log("Player's dog turns tail and runs away!");
            BattleManager.EndBattle();
        }

        public void Check()
        {
            Debug.Log(@"Player examines the opponent...
Name:   [MISSINGNO]
Size:     UNKNOWN
Health:     0/0
Attack:      ?
Speed:       ?");
        }
    }
}
