using Game.Runtime;
using Game339.Shared.Models;
using UnityEngine;

namespace overworld
{
    public class EnemyDog : WorldDog
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject otherObject = collision.gameObject;
            PlayerCharacter player = otherObject.GetComponent<PlayerCharacter>();
            
            if (!player) return;
            
            GameState gameState = ServiceResolver.Resolve<GameState>();

            Card.SetOwner(gameState.BadGuy);
            
            gameState.IsInCombat.Value = true;
        }

        public new static GameObject CreateDog(DogSize size) => WorldDog.CreateDog(size);
    }
}