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

        [SerializeField] private GameObject playerView;

        public PlayerDog Dog { get; private set; }
        
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            Dog = PlayerDog.CreateDog(dogSize).GetComponent<PlayerDog>();
            Dog.gameObject.transform.SetParent(transform);
            
            Character player = ServiceResolver.Resolve<GameState>().Player;
            Dog.Card.SetOwner(player);
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
            
            transform.Translate(walkDistance, Space.World);
            playerView.transform.Translate(walkDistance, Space.World);

            //PlayerSpriteRenderer.transform.position = SpriteTools.ConstrainToScreen(PlayerSpriteRenderer);
            
            UpdateDistanceUntilHeal(walkDistance.magnitude);
        }

        private void FaceDirection(Vector2 direction)
        {
            if (direction.x == 0) return;

            bool shouldFlipSprite = direction.x < 0;
            
            int angleToRotate = shouldFlipSprite ? 180 : 0;
            
            transform.rotation = Quaternion.Euler(0, angleToRotate, 0);
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