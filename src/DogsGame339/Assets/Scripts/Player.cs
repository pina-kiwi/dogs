using UnityEngine;
using System;
using System.Collections;
using Game.Runtime;
using Game339.Shared.Models;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public SpriteRenderer PlayerSpriteRenderer;
    public SpriteRenderer DogSpriteRenderer;
    public string fightScene;
    
    private Vector3 lastPosition;
    private float distanceAccumulated = 0f;

// You can tweak this to control how far the player needs to move for +1 health
    public float pixelsPerHealth;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("bam " + collision.gameObject.tag);
        SceneManager.LoadScene(fightScene);
    }
    
    private void Start()
    {
        lastPosition = transform.position;
    }
    
    private void Update()
    {
        // Calculate distance moved since last frame
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);
        distanceAccumulated += distanceThisFrame;

        // Check if enough distance has been covered
        if (distanceAccumulated >= pixelsPerHealth)
        {
            GainHealth(1);
            distanceAccumulated = 0f; // reset counter
        }

        lastPosition = transform.position;
    }
    
    private void GainHealth(int amount)
    {
        var player = ServiceResolver.Resolve<GameState>().Player;

        int maxHealth = 3; // or whatever your max is
        player.Health.Value = Mathf.Min(player.Health.Value + amount, maxHealth);

        Debug.Log($"Player gained {amount} health! Current health: {player.Health.Value}");
    }
    
    public void Move(Vector2 direction)
    {
        FaceCorrectDirection(direction);
        
        float xAmount = direction.x * GameParameters.PlayerMoveSpeed * Time.deltaTime;
        float yAmount = direction.y * GameParameters.PlayerMoveSpeed * Time.deltaTime;
        
        PlayerSpriteRenderer.transform.Translate(xAmount, yAmount, 0f);
        
        //PlayerSpriteRenderer.transform.position = SpriteTools.ConstrainToScreen(PlayerSpriteRenderer);
    }

    public void MoveManually(Vector2 direction)
    {
        Move(direction);
    }

    public void Reset()
    {
        PlayerSpriteRenderer.transform.position = new Vector3(0, 0, 0);
        PlayerSpriteRenderer.flipX = false;
    }
    
    private void FaceCorrectDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            PlayerSpriteRenderer.flipX = false;
            DogSpriteRenderer.flipX = false;
        }
        
        if (direction.x < 0)
        {
            PlayerSpriteRenderer.flipX = true;
            DogSpriteRenderer.flipX = true;
        }
        
        //if (direction.y > 0)
        //CorgiSpriteRenderer.flipY = false;
        
        //if (direction.y < 0)
        //CorgiSpriteRenderer.flipY = true;
    }
}
