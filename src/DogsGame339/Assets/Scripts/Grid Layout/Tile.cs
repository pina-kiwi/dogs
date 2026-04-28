using System;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Sprite baseColor, offsetColor;
    public SpriteRenderer spriteRenderer;
    public GameObject highlight;

    public void Init(bool isOffset)
    {
        spriteRenderer.sprite = isOffset ? offsetColor : baseColor;
    }

    void OnMouseEnter()
    {
        Debug.Log("Hovering " + name);
        highlight.SetActive(true);
    }

    private void OnMouseDown()
    {
        throw new NotImplementedException();
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }
    
    
}
