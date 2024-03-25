//coded by someone

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public GridManager gridManager;

    public Vector2Int gridCoords;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = transform.Find("Square").GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    private void OnMouseOver()
    {
        spriteRenderer.enabled = true;
        gridManager.OnTileHoverEnter(this);
        

    }

    private void OnMouseExit()
    {
        spriteRenderer.enabled=false;
        gridManager.OnTileHoverExit(this);
    }
}
