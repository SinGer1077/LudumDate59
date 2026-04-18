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

    public bool StartBattle(SquadController squad1, SquadController squad2)
    {
        Debug.Log("first" + BattleController.battleTable[(int)squad1.typeOfSquad, (int)squad2.typeOfSquad]);
        Debug.Log("second" + BattleController.battleTable[(int)squad2.typeOfSquad, (int)squad1.typeOfSquad]);
        squad1.TakeDamageInPercentage(BattleController.battleTable[(int)squad1.typeOfSquad, (int)squad2.typeOfSquad]);
        squad2.TakeDamageInPercentage(BattleController.battleTable[(int)squad2.typeOfSquad, (int)squad1.typeOfSquad]);

        if (squad1.typeOfSquad == squad2.typeOfSquad)
        {
            squad1.Move(EDirection.Left); // todo
            squad2.Move(EDirection.Right);
            return true; // should change pos;
        }

        return false; // shouldnt change pos
    }
}
