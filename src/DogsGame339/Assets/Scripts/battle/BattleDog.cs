using Game339.Shared.Models;
using UnityEngine;

namespace battle
{
    public abstract class BattleDog : DogObject
    {
        [SerializeField] protected BattleDog opponent;
        protected abstract Character Owner { get; }

        protected virtual void Attack() {
            BattleManager.NextTurn();
        }
        
        protected virtual void Flee() {
            BattleManager.EndBattle();
        }

        public virtual void TakeDamage(int damage)
        {
            Owner.TakeDamage(damage);
            if (Owner.Health.Value <= 0) BattleManager.EndBattle();
        }
    }
}
