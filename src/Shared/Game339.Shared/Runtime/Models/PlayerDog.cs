using System.Runtime.InteropServices;
using Game339.Shared.Infastructure.DataTypes;

namespace Game339.Shared.Models
{
    public class PlayerDog : GridEntity
    {
        public override string Name => "Player";
        
        public readonly int MaxBones;
        public ObservableValue<int> NumBones { get; private set; }

        public PlayerDog(int maxBones, int startingBones, [Optional] GridPosition position) : base(position.row, position.col)
        {
            MaxBones = maxBones;
            NumBones = new(startingBones);
        }
        
        public void SetBones(int bones) => NumBones.Value = bones;
    }
}