using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int numRows = 5, numColumns = 6;

    [SerializeField] private GridTile tilePrefab;

    [SerializeField] public float padding;

    [SerializeField] public TextMeshProUGUI text;

    private void Awake()
    {
        InitGrid();
    }

    public void InitGrid()
    {
        for(int y = 0; y < numRows; y++)
        {
            for(int x = 0; x<numColumns; x++)
            {
                GridTile tile = Instantiate(tilePrefab, transform);
                Vector2 tilePos = new Vector2(x+(padding*x), y+(padding*y));
                tile.transform.localPosition= tilePos;
                tile.name = $"Tile_{x}_{y}";
                tile.gridManager = this;
                tile.gridCoords = new Vector2Int(x, y);
            }
        }
    }

    public void OnTileHoverEnter(GridTile gridTile)
    {
        text.text = gridTile.gridCoords.ToString();
    }

    public void OnTileHoverExit(GridTile tile)
    {
        text.text = "-, -";
    }
}

