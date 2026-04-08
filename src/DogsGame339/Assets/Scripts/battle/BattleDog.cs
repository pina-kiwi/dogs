namespace battle
{
    public abstract class BattleDog : DogObject
    {
        public BattleDog Opponent { set; protected get; }

        public virtual void Attack() {
            Opponent.TakeDamage(AttackPower);
            BattleStage.NextTurn();
        }
        public virtual void Flee() {
            BattleStage.EndBattle();
        }

        public void TakeDamage(int damage) => Card.TakeDamage(damage);
    }
}
