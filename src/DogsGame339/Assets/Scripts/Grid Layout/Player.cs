using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private void FixedUpdate()
    {
        Vector2 inputDirection = InputSystem.actions["Move"].ReadValue<Vector2>();
        Walk(inputDirection);
    }
    
    private void Walk(Vector2 direction)
    {
        //Debug.Log(direction);
        FaceDirection(direction);
        this.transform.position = direction;
    }
    
    private void FaceDirection(Vector2 direction)
    {
        if (direction.x == 0) return;

        bool shouldFlipSprite = direction.x < 0;
            
        int angleToRotate = shouldFlipSprite ? 180 : 0;
            
        transform.rotation = Quaternion.Euler(0, angleToRotate, 0);
    }
}
