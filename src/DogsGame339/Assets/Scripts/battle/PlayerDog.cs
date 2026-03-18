using UnityEngine;

namespace battle
{
    public class PlayerDog : BattleDog
    {
        public override void Attack()
        {
            Debug.Log("Player's dog attacks!");
        }

        public override void Flee()
        {
            Debug.Log("Player's dog turns tail and runs away!");
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
