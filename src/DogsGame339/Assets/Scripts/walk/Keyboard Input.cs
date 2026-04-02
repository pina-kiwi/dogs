using UnityEngine;


public class KeyboardInput : MonoBehaviour
{

    public Player Player;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Player.MoveManually(new Vector2(0,1));
            print("up");
        }
        if (Input.GetKey(KeyCode.S))
        {
            Player.MoveManually(new Vector2(0,-1));
            print("down");
        }
        if (Input.GetKey(KeyCode.A))
        {
            Player.MoveManually(new Vector2(-1, 0));
            print("left");
        }
        if (Input.GetKey(KeyCode.D))
        {
            Player.MoveManually(new Vector2(1, 0));
            print("right");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }
}