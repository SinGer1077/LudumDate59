using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject cellPrefab;

    public Vector2 gridSize;
    public float cellSize;

    public Vector2 generationOffset;

    [HideInInspector]
    public Dictionary<Vector2Int, Cell> grid = new Dictionary<Vector2Int, Cell>();

    Vector2Int[] evenDirs = {
        new Vector2Int(+1, 0),   // Right
        new Vector2Int(0, +1),   // UpRight
        new Vector2Int(-1, +1),  // UpLeft
        new Vector2Int(-1, 0),   // Left
        new Vector2Int(-1, -1),  // DownLeft
        new Vector2Int(0, -1)    // DownRight
    };

    Vector2Int[] oddDirs = {
        new Vector2Int(+1, 0),   // Right
        new Vector2Int(+1, +1),  // UpRight
        new Vector2Int(0, +1),   // UpLeft
        new Vector2Int(-1, 0),   // Left
        new Vector2Int(0, -1),   // DownLeft
        new Vector2Int(+1, -1)   // DownRight
    };

    public void Awake()
    {
        GenerateGrid((int)gridSize.x, (int)gridSize.y);
        VisualizeGrid();
    }


    public void GenerateGrid(int width, int height)
    {
        for (int q = 0; q < width; q++)
        {
            for (int r = 0; r < height; r++)
            {
                grid[new Vector2Int(q, r)] = new Cell(q, r);
            }
        }
    }

    public Vector2 HexToWorld(Vector2Int hex, float size)
    {
        float width = Mathf.Sqrt(3) * size;
        float height = 2f * size;

        float x = hex.x * width;

        if (hex.y % 2 == 1)
            x += width / 2f;

        float y = hex.y * (height * 0.75f);

        return new Vector2(x, y);
    }

    public void VisualizeGrid()
    {
        foreach (var hex in grid)
        {
            Vector2 pos = HexToWorld(hex.Key, cellSize);
            Vector2 actualPos = pos + generationOffset;
            Instantiate(cellPrefab, actualPos, Quaternion.identity);
            hex.Value.actualWorldPosition = actualPos;
        }
    }

    public Cell GetNeighbor(Cell currentCell, EDirection type)
    {
        Vector2Int direction = new Vector2Int(currentCell.q, currentCell.r);
        if (currentCell.r % 2 == 0)
            direction += evenDirs[(int)type];
        else
            direction += oddDirs[(int)type];

        if (grid.ContainsKey(direction))
        {
            return grid[direction];
        }
        else
        {
            return null;
        }
    }
}
