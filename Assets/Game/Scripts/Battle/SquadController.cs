using UnityEngine;


enum SquadType
{
    Lancer,
    Shield,
    Horse
}
public class SquadController : MonoBehaviour
{
    public float baseHP;

    [HideInInspector]
    public float currentHP;
}
