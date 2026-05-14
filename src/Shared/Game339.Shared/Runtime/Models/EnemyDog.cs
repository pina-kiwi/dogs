namespace Game339.Shared.Models
{
    public abstract class EnemyDog : GridEntity
    {
        public abstract int BonesToSteal { get; }
        public abstract int TilesPerTurn { get; }

        protected EnemyDog(int row, int column) : base(row, column) {}
    }
    
    public class SmallDog : EnemyDog
    {
        public override string Name => "Shih Tzu";
        public override int BonesToSteal => 2;
        public override int TilesPerTurn => 12;
        
        public SmallDog(int row = 0, int column = 0) : base(row, column) {}
    }
    
    public class MediumDog : EnemyDog
    {
        public override string Name => "Abomination";
        public override int BonesToSteal => 3;
        public override int TilesPerTurn => 8;
        
        public MediumDog(int row = 0, int column = 0) : base(row, column) {}
    }
    
    public class LargeDog : EnemyDog
    {
        public override string Name => "Corgi";
        public override int BonesToSteal => 5;
        public override int TilesPerTurn => 5;
        
        public LargeDog(int row = 0, int column = 0) : base(row, column) {}
    }
}