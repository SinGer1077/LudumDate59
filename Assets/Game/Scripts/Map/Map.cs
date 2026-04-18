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
    public List<Cell> grid;

    public void Awake()
    {
        GenerateGrid((int)gridSize.x, (int)gridSize.y);
        VisualizeGrid();
    }


    public List<Cell> GenerateGrid(int width, int height)
    {
        List<Cell> grid = new List<Cell>();

        for (int q = 0; q < width; q++)
        {
            for (int r = 0; r < height; r++)
            {
                grid.Add(new Cell(q, r));
            }
        }

        this.grid = grid;

        return grid;
    }


    public List<Cell> GetNeighbors(Cell hex, HashSet<Cell> grid)
    {
        List<Cell> result = new List<Cell>();

        foreach (var dir in Cell.directions)
        {
            Cell neighbor = new Cell(hex.q + dir.q, hex.r + dir.r);

            if (grid.Contains(neighbor))
                result.Add(neighbor);
        }

        return result;
    }

    public static Cell GetNeighbor(EDirection direction)
    {
        return Cell.directions[(int)direction];
    }

    public Vector2 HexToWorld(Cell hex, float size)
    {
        float width = Mathf.Sqrt(3) * size;
        float height = 2f * size;

        float x = hex.q * width;

        if (hex.r % 2 == 1)
            x += width / 2f;

        float y = hex.r * (height * 0.75f);

        return new Vector2(x, y);
    }

    public void VisualizeGrid()
    {
        foreach (var hex in grid)
        {
            Vector2 pos = HexToWorld(hex, cellSize);
            Instantiate(cellPrefab, pos + generationOffset, Quaternion.identity);
        }
    }
}
