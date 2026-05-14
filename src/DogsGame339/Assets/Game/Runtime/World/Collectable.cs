using Game339.Shared.Models;

namespace Game.Runtime.World
{
    public abstract class Collectible<T_GridObject> : WorldObject<T_GridObject> where T_GridObject : GridObject
    {
        protected override int RenderPriority => 6;
    }
}