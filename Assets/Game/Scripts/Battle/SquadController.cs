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
}
