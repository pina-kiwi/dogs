using System;
using Game.Runtime;
using Game339.Shared.Models;
using UnityEngine;
using UnityEngine.InputSystem;

namespace overworld
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private DogSize dogSize;
        [SerializeField] private float walkSpeedScale = 3f;
        [SerializeField] private float distanceForHeal = 15f; // You can tweak this to control how far the player needs to move for +1 health
        private float _distanceUntilHeal;

        private PlayerDog _dog;
        
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            _dog = PlayerDog.CreateDog(dogSize).GetComponent<PlayerDog>();
            _dog.gameObject.transform.SetParent(transform);
        }

        private void FixedUpdate()
        {
            Vector2 inputDirection = InputSystem.actions["Move"].ReadValue<Vector2>();
            Walk(inputDirection);
        }

        private void Walk(Vector2 direction)
        {
            FaceDirection(direction);
            
            // Calculate distance moved
            Vector3 walkDistance = walkSpeedScale * Time.deltaTime * direction;
            
            spriteRenderer.transform.Translate(walkDistance);

            //PlayerSpriteRenderer.transform.position = SpriteTools.ConstrainToScreen(PlayerSpriteRenderer);
            
            UpdateDistanceUntilHeal(walkDistance.magnitude);
        }

        private void FaceDirection(Vector2 direction)
        {
            if (direction.x == 0) return;

            bool shouldFlipSprite = direction.x < 0;
            
            spriteRenderer.flipX = shouldFlipSprite;
            _dog.FlipX(shouldFlipSprite);
        }

        private void UpdateDistanceUntilHeal(float distanceWalked)
        {
            _distanceUntilHeal -= distanceWalked;
            if (_distanceUntilHeal <= 0) Heal(); // Check if enough distance has been covered
        }

        private void Heal(int amount = 1)
        {
            Character player = ServiceResolver.Resolve<GameState>().Player;
            
            player.GainHealth(amount);
            
            _distanceUntilHeal = distanceForHeal; // reset counter

            //Debug.Log($"Player gained {amount} health! Current health: {player.Health.Value}");
        }
    }
}