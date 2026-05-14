namespace Game339.Shared.Models
{
    public abstract class Collectables : GridObject
    {
        protected Collectables(int row, int column) : base(row, column) {}
    }

    public class Bone : Collectables
    {
        public Bone(int row, int column) : base(row, column) {}
    }
}