using UnityEngine;

namespace battle
{
    public abstract class BattleDog : MonoBehaviour
    {
        public float Health { get; protected set; }
        public readonly DogSize Size;
        
        public readonly int MaxHealth;
        public readonly int AttackPower;
        public readonly int Speed;
        
        private readonly Sprite _sprite;

        public abstract void Attack();
        public abstract void Flee();
    }
}
