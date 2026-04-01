using UnityEngine;
using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public Gameer Gameer;
    public SpriteRenderer PlayerSpriteRenderer;
    
    public void Move(Vector2 direction)
    {
        //FaceCorrectDirection(direction);
        
        float xAmount = direction.x * GameParameters.PlayerMoveSpeed * Time.deltaTime;
        float yAmount = direction.y * GameParameters.PlayerMoveSpeed * Time.deltaTime;
        
        PlayerSpriteRenderer.transform.Translate(xAmount, yAmount, 0f);
        
        //PlayerSpriteRenderer.transform.position = SpriteTools.ConstrainToScreen(PlayerSpriteRenderer);
    }

    public void MoveManually(Vector2 direction)
    {
        if (!Gameer.IsPlaying)
        {
            return;
        }
        Move(direction);
    }

    public void Reset()
    {
        PlayerSpriteRenderer.transform.position = new Vector3(0, 0, 0);
        PlayerSpriteRenderer.flipX = false;
    }
}
