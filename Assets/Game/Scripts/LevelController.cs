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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            squads[1].Move(EDirection.Right);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            squads[1].Move(EDirection.Left);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            squads[1].Move(EDirection.UpRight);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            squads[1].Move(EDirection.UpLeft);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            squads[1].Move(EDirection.DownRight);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            squads[1].Move(EDirection.DownLeft);
        }
    }

    public void StartBattle(SquadController squad1, SquadController squad2)
    {
        squad1.TakeDamage(BattleController.battleTable[(int)squad1.typeOfSquad, (int)squad2.typeOfSquad]);
        squad2.TakeDamage(BattleController.battleTable[(int)squad2.typeOfSquad, (int)squad1.typeOfSquad]);

        if (squad1.typeOfSquad == squad2.typeOfSquad)
        {
            squad1.Move(EDirection.Left); // todo
            squad2.Move(EDirection.Right);
        }
    }
}
