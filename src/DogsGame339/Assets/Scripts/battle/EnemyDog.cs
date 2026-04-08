using UnityEngine;

namespace battle
{
    public class EnemyDog : BattleDog
    {
        private void Start() => BattleStage.EnemyDog = this;

        public void TakeTurn()
        {
            Attack();
        }
        
        public override void Attack()
        {
            Debug.Log("Enemy dog attacks!");
            base.Attack();
        }

        public override void Flee()
        {
            Debug.Log("Enemy dog turns tail and runs away!");
            base.Flee();
        }
    }
}
