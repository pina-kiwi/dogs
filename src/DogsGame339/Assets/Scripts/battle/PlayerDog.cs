using UnityEngine;

namespace battle
{
    public class PlayerDog : BattleDog
    {
        private void Awake() => BattleStage.PlayerDog = this;

        public override void Attack()
        {
            Debug.Log("Player's dog attacks!");
            base.Attack();
        }

        public override void Flee()
        {
            Debug.Log("Player's dog turns tail and runs away!");
            Opponent.Attack();
            base.Flee();
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
