using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Map map;

    public SquadController[] squads; // from 0 to 5 - my and enemies squads;
    public Vector2Int[] cellsToPlace;

    public void Start()
    {
        PlaceSquads();
    }

    public void PlaceSquads()
    {
        for (int i = 0; i < squads.Length; i++)
        {
            squads[i].PlaceOn(cellsToPlace[i]);
        }
    }
}
