using UnityEngine;

namespace battle
{
    public class EnemyDog : BattleDog
    {
        public override void Attack()
        {
            Debug.Log("Enemy dog attacks!");
        }

        public override void Flee()
        {
            Debug.Log("Enemy dog turns tail and runs away!");
        }
    }
}
