using System;
using Game339.Shared.Infastructure.Diagnostics;
using Game339.Shared.Models;

namespace Game339.Shared.Services.Implementation
{
    public class BoneService : I_BoneService
    {
        private readonly IGameLog _log;
        private readonly PlayerDog _player;
        
        public int BoneCount => _player.NumBones.Value;

        public BoneService(PlayerDog player, IGameLog gameLog)
        {
            _log = gameLog;
            _player = player;
        }

        public int CalculateBones(int changeAmount) => Math.Clamp(BoneCount + changeAmount, 0, _player.MaxBones);

        public void AddBone()
        {
            int initialBones = BoneCount;
            int remainingBones = CalculateBones(1);
            _player.SetBones(remainingBones);
            
            bool bonesAlreadyMaxed = initialBones == remainingBones;
            string foundBoneMessage = $"{_player.Name} found a bone, ";
            foundBoneMessage += bonesAlreadyMaxed ? "but they couldn't hold any more!" : $"they now have {BoneCount}!";
            
            _log.Info(foundBoneMessage);
        }
        
        public void StealBones(EnemyDog attacker)
        {
            int remainingBones = CalculateBones(-attacker.BonesToSteal);
            int lostBones = BoneCount - remainingBones;

            _player.SetBones(remainingBones);
            _log.Info($"{attacker.Name} took {lostBones} bones from {_player.Name}, they have {BoneCount} left!");
        }
    }
}