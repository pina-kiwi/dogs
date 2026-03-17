using UnityEngine;

public class DogTypes : MonoBehaviour
{
    //create dog types with different stats
    //stats include: breed of dog, health #, attack power
    public float health;
    public float attackPower;
    public void Dalmation()
    {
        health = 20;
        attackPower = 5;
    }

    public void Corgi()
    {
        health = 15;
        attackPower = 7;

    }

    public void Pug()
    {
        health = 20;
        attackPower = 3;
    }


    
}
