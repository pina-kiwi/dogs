using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Sprite baseColor, offsetColor;
    public SpriteRenderer spriteRenderer;

    public void Init(bool isOffset)
    {
        spriteRenderer.sprite = isOffset ? offsetColor : baseColor;
    }
}
