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
    public LevelController levelController;

    public int baseHP = 4;

    [HideInInspector]
    public float currentHP;

    public SquadType typeOfSquad;
    public ETeam team;

    private Cell currentCell;

    private void Start()
    {
        currentHP = baseHP;
    }

    public void TakeDamage(int  damage)
    {
        currentHP -= damage;
        transform.localScale *= (currentHP / baseHP);
        if (currentHP <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject); // todo
    }

    public bool Move(EDirection direction)
    {
        Cell finded = map.GetNeighbor(currentCell, direction);
        if (finded != null)
        {
            if (finded.squadInCell != null)
            {
                if (finded.squadInCell.team == this.team) return false; // we cant stand on filled cell with the same team;
                else
                {
                    bool battle = levelController.StartBattle(this, finded.squadInCell); // todo need 
                    if (!battle)
                    {
                        FillCell(finded);
                        return true;
                    }
                }
            }
            else
            {
                FillCell(finded);
                return true;
            }
        }
        return false;
    }

    public void PlaceOn(Vector2Int pos)
    {
        FillCell(map.grid[pos]);
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

    public void FillCell(Cell to)
    {
        if (currentCell != null && currentCell.squadInCell == this)
        {
            currentCell.squadInCell = null;
        }
        currentCell = to;
        VisualMove(to, true); // todo false
        to.squadInCell = this;
    }
}
