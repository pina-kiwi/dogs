using UnityEngine;

public abstract class DogObject : MonoBehaviour
{
    [SerializeField] protected DogSize size;
    
    private SpriteRenderer _spriteRenderer;
    
    protected DogCard Card;

    private void Awake()
    {
        Card = new(size);
    }

    public int Health => Card.Health.Value;
    public int MaxHealth => Card.MaxHealth.Value;
    public int AttackPower => Card.Damage.Value;
    public int Speed => Card.Speed.Value;
    
    protected void FlipX(bool shouldFlip) => _spriteRenderer.flipX = shouldFlip;
}