namespace Game339.Shared.Models
{
    public abstract class EnemyDog : Character
    {
        public abstract int BonesToSteal { get; }
        public abstract int TilesPerTurn { get; }

        protected EnemyDog(int row, int column) : base(row, column) {}
    }
    
    public class SmallDog : EnemyDog
    {
        public override string Name => "Small Dog";
        public override int BonesToSteal => 2;
        public override int TilesPerTurn => 12;
        
        public SmallDog(int row, int column) : base(row, column) {}
    }
    
    public class MediumDog : EnemyDog
    {
        public override string Name => "Medium Dog";
        public override int BonesToSteal => 3;
        public override int TilesPerTurn => 8;
        
        public MediumDog(int row, int column) : base(row, column) {}
    }
    
    public class LargeDog : EnemyDog
    {
        public override string Name => "Large Dog";
        public override int BonesToSteal => 5;
        public override int TilesPerTurn => 5;
        
        public LargeDog(int row, int column) : base(row, column) {}
    }

    public class PlayerDog : Character
    {
        public override string Name => "Player";
        
        public PlayerDog(int row, int column) : base(row, column) {}
    }
}