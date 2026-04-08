using Game.Runtime;
using Game339.Shared.Models;
using UnityEngine;

namespace overworld
{
    public class PlayerDog : WorldDog
    {
        private void Start() => Card.SetCharacter(ServiceResolver.Resolve<GameState>().Player);

        public new void FlipX(bool shouldFlip) => base.FlipX(shouldFlip);

        public new static GameObject CreateDog(DogSize size)
        {
            GameObject dogObject = WorldDog.CreateDog(size);
            PlayerDog playerDogComponent = dogObject.AddComponent<PlayerDog>();
            playerDogComponent.SetSize(size);
            
            return dogObject;
        } 
    }
}