using System;
using Game339.Shared.Diagnostics;
using Game339.Shared.Models;

namespace Game339.Shared.Services.Implementation
{
    public class DamageService : IDamageService
    {
        private readonly IGameLog _gameLog;
        private readonly Random _dodge = new();

        public DamageService(IGameLog gameLog)
        {
            _gameLog = gameLog;
        }

        public void ApplyDamage(Character defender, int damage)
        {
            _gameLog.Info($"{defender.Name} takes {damage} damage");
            defender.Health.Value -= damage;
        }
        
        public int CalculateDamage(Character attacker, Character defender)
        {
            var damage = DidAttackHit(defender.Speed) ? attacker.AttackPower.Value : 0;
            _gameLog.Info($"{attacker.Name} attacked {defender.Name} for {damage} damage");
            return damage;
        }

        private bool DidAttackHit(ObservableValue<int> defenderSpeed)
        {
            return _dodge.Next(10)+1 > defenderSpeed.Value;
        }
    }
}