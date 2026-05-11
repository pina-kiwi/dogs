using System;
using Game339.Shared.Diagnostics;
using Game339.Shared.Models;

namespace Game339.Shared.Services.Implementation
{
    public class BoneService : I_BoneService
    {
        private readonly IGameLog _log;
        private readonly GameState _game;
        private readonly int _maxBones;
        
        private string PlayerDog => _game.Player.Name;
        
        public int BoneCount => _game.PlayerBones.Value;

        public BoneService(int maxBones, GameState gameState, IGameLog gameLog)
        {
            _log = gameLog;
            _game = gameState;
            _maxBones = maxBones;
        }

        public int CalculateBones(int changeAmount)
        {
            return Math.Clamp(BoneCount + changeAmount, 0, _maxBones);
        }

        public void AddBone()
        {
            int initialBones = BoneCount;
            int remainingBones = CalculateBones(1);
            _game.SetBones(remainingBones);
            
            bool bonesAlreadyMaxed = initialBones == remainingBones;
            string foundBoneMessage = $"{PlayerDog} found a bone, ";
            foundBoneMessage += bonesAlreadyMaxed ? "but they couldn't hold any more!" : $"they now have {BoneCount}!";
            
            _log.Info(foundBoneMessage);
        }
        
        public void StealBones(string attackerName, int bonesToSteal)
        {
            int remainingBones = CalculateBones(-bonesToSteal);
            int lostBones = BoneCount - remainingBones;

            _game.SetBones(remainingBones);
            _log.Info($"{attackerName} took {lostBones} bones from {PlayerDog}, they have {BoneCount} left!");
        }

        public void StealBones(EnemyDog attacker) => StealBones(attacker.Name, attacker.BonesToSteal);
    }
}