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

    public Cell(int q, int r)
    {
        this.q = q;
        this.r = r;
    }

    public static readonly Cell[] directions = new Cell[]
    {
        new Cell(1, 0),   // вправо
        new Cell(1, -1),  // вверх-вправо
        new Cell(0, -1),  // вверх-влево
        new Cell(-1, 0),  // влево
        new Cell(-1, 1),  // вниз-влево
        new Cell(0, 1)    // вниз-вправо
    };


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
