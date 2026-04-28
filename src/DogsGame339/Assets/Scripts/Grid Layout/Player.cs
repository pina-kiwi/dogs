using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float tileSize = 1f;
    public SpriteRenderer spriteRenderer;

    private Vector3 targetPos;
    private bool isMoving = false;

    void Start()
    {
        targetPos = transform.position;
    }
    
    /*
   private void FixedUpdate()
    {
        Vector2 inputDirection = InputSystem.actions["Move"].ReadValue<Vector2>();
        Walk(inputDirection);
    }
    */

    private void FixedUpdate()
    {
        if (isMoving)
        {
            MoveToTarget();
            return;
        }
        
        Vector2 inputDirection = InputSystem.actions["Move"].ReadValue<Vector2>();

        if (inputDirection != Vector2.zero)
        {
            Vector2 moveDir = GetCardinalDirection(inputDirection);
            TryMove(moveDir);
        }
    }
    
    Vector2 GetCardinalDirection(Vector2 input)
    {
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            return new Vector2(Mathf.Sign(input.x), 0);
        else
            return new Vector2(0, Mathf.Sign(input.y));
    }
    
    void TryMove(Vector2 direction)
    {
        targetPos = transform.position + new Vector3(direction.x, direction.y, 0) * tileSize;
        isMoving = true;

        FaceDirection(direction);
    }
    
    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            transform.position = targetPos;
            isMoving = false;
        }
    }
    
    private void Walk(Vector2 direction)
    {
        //Debug.Log(direction);
        FaceDirection(direction);
        //this.transform.position = direction +
        spriteRenderer.transform.position = new Vector2(transform.position.x + direction.x * moveSpeed + tileSize, transform.position.y + direction.y * moveSpeed + tileSize);
    }
    
    private void FaceDirection(Vector2 direction)
    {
        if (direction.x == 0) return;

        bool shouldFlipSprite = direction.x < 0;
            
        int angleToRotate = shouldFlipSprite ? 180 : 0;
            
        transform.rotation = Quaternion.Euler(0, angleToRotate, 0);
    }

}
