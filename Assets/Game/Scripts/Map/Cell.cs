using UnityEngine;

public enum EDirection
{
   Right, 
   UpRight,
   UpLeft,
   Left,
   DownLeft,
   DownRight
}

public class Cell
{
    public int q;
    public int r;

    public SquadController squadInCell;
    public Vector2 actualWorldPosition;

    public Cell(int q, int r)
    {
        this.q = q;
        this.r = r;
    }
}
