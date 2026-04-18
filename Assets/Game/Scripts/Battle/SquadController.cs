using UnityEngine;
using static UnityEditor.PlayerSettings;


public enum SquadType
{
    Lancer,
    Shield,
    Horse
}

public enum ETeam
{
    Main,
    Enemy
}

public class SquadController : MonoBehaviour
{
    public Map map;

    public int baseHP = 4;

    [HideInInspector]
    public float currentHP;

    public SquadType typeOfSquad;
    public ETeam team;

    private Cell currentCell;

    public void TakeDamage(int  damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Death();
        }

        transform.localScale *= currentHP / baseHP;
    }

    public void Death()
    {
        Destroy(gameObject); // todo
    }

    public void Move(EDirection direction)
    {
        Cell finded = map.GetNeighbor(currentCell, direction);
        if (finded != null && finded.squadInCell == null)
        {
            currentCell.squadInCell = null;
            currentCell = finded;
            VisualMove(finded, true); // todo false
            finded.squadInCell = this;
        }
    }

    public void PlaceOn(Vector2Int pos)
    {
        if (currentCell != null)
        {
            currentCell.squadInCell = null;
        }

        map.grid[pos].squadInCell = this;
        VisualMove(map.grid[pos], true);

        currentCell = map.grid[pos];
        map.grid[pos].squadInCell = this;
    }

    public void VisualMove(Cell pos, bool teleport)
    {
        if (teleport)
        {
            Vector2 squadPos = pos.actualWorldPosition;
            squadPos.x -= 0.25f * ((int)this.team == 0 ? 1 : -1);
            transform.position = squadPos;
        }
    }
}
