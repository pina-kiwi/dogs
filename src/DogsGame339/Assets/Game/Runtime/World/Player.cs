using Game.Runtime.Infastructure;
using Game339.Shared.Models;

namespace Game.Runtime.World
{
    public class Player : WorldEntity<PlayerDog>
    {
        protected override PlayerDog InstantiateModel() => ServiceResolver.Rig<PlayerDog>(World2GridPos);
        
        protected override int RenderPriority => 5;
    }
}