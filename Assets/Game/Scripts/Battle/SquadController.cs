using UnityEngine;


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

    }

    public void PlaceOn(Vector2Int pos)
    {
        map.grid[pos].squadInCell = this;
        VisualMove(pos, true);
        
    }

    public void VisualMove(Vector2Int pos, bool teleport)
    {
        if (teleport)
        {
            Vector2 squadPos = map.grid[pos].actualWorldPosition;
            squadPos.x -= 0.25f * ((int)this.team == 0 ? 1 : -1);
            transform.position = squadPos;
        }
    }
}
