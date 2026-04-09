using System;

namespace Game339.Shared.Models
{
    public class Character
    {
        public DogCard Dog { get; set; }
        
        public ObservableValue<string> Name { get; } = new();
        public ObservableValue<int> AttackPower { get; } = new();
        public ObservableValue<int> Speed { get; } = new();
        public ObservableValue<int> Health { get; } = new();
        public ObservableValue<int> MaxHealth { get; set; } = new(int.MaxValue);
        
        public void TakeDamage(int amount)
        {
            Health.Value = Math.Max(Health.Value - amount, 0);
        }

        public void GainHealth(int amount)
        {
            Health.Value = Math.Min(Health.Value + amount, MaxHealth.Value);
        }
    }
}