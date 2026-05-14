using Game339.Shared.Models;

namespace Game339.Shared.Services
{
    public interface I_BoneService
    {
        int BoneCount { get; }
        int CalculateBones(int changeAmount);
        
        void AddBone();
        void StealBones(EnemyDog attacker);
    }
}