using UnityEngine;

public enum EDirection
{
    Left,
    Right, 
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}

public class Cell : MonoBehaviour
{
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
