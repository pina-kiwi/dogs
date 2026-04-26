using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float tileSize = 1f;
    public SpriteRenderer spriteRenderer;

    private bool isMoving = false;
    private Vector3 targetPos;
    
    
   private void FixedUpdate()
    {
        Vector2 inputDirection = InputSystem.actions["Move"].ReadValue<Vector2>();
        Walk(inputDirection);
    }
    
    private void Walk(Vector2 direction)
    {
        //Debug.Log(direction);
        FaceDirection(direction);
        //this.transform.position = direction +
        spriteRenderer.transform.position = new Vector2(transform.position.x + direction.x , transform.position.y + direction.y );
    }
    
    private void FaceDirection(Vector2 direction)
    {
        if (direction.x == 0) return;

        bool shouldFlipSprite = direction.x < 0;
            
        int angleToRotate = shouldFlipSprite ? 180 : 0;
            
        transform.rotation = Quaternion.Euler(0, angleToRotate, 0);
    }

}
