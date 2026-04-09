using UnityEngine;

public abstract class DogObject : MonoBehaviour
{
    [SerializeField] protected DogSize size;
    public DogCard Card { get; private set; }

    private bool _isInitialized;
    protected bool Initialized { get => _isInitialized; set => _isInitialized = value; }
    
    private SpriteRenderer _spriteRenderer;

    private void Awake() => Initialize();

    public void Initialize(DogSize size) {
        if (Initialized) return;
        
        this.size = size;
        
        Initialize();
    }

    public void Initialize(DogCard card) {
        if (Initialized) return;
        
        Card = card;
        size = card.Size;
        
        Initialize();
    }

    protected void Initialize()
    {
        if (Initialized) return;
        
        Card ??= new(size);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (_spriteRenderer.sprite == null) SetSprite();
        
        Initialized = true;
    }

    private void SetSprite()
    {
        string spritePath = "";
            
        switch (size)
        {
            case DogSize.Small:
                spritePath = "sprites/smallDog";
                break;
            case DogSize.Medium:
                spritePath = "sprites/mediumDog";
                break;
            case DogSize.Large:
                spritePath = "sprites/bigDog";
                break;
        }
            
        _spriteRenderer.sprite = Resources.Load<Sprite>(spritePath);
    }
}