using System;
using Game.Runtime;
using Game339.Shared.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Sprite baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;

    public void Init(bool isOffset)
    {
        spriteRenderer.sprite = isOffset ? offsetColor : baseColor;
    }

    void OnMouseEnter()
    {
        var log = ServiceResolver.Resolve<IGameLog>();
        log.Info($"Hovering {name}");
        //Debug.Log("Hovering " + name);
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
