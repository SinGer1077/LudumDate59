using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SquadController lancerSquad;
    public SquadController horseSquad;
    public SquadController shieldSquad;

    public GameState gameState;

    private SquadController currentSquadToCommand;

    private KeyValuePair<EDirection, KeyCode>[] keyCodesDirection = 
    {
        new KeyValuePair<EDirection, KeyCode>(EDirection.Left, KeyCode.A),
        new KeyValuePair<EDirection, KeyCode>(EDirection.Right, KeyCode.D),
        new KeyValuePair<EDirection, KeyCode>(EDirection.UpLeft, KeyCode.Q),
        new KeyValuePair<EDirection, KeyCode>(EDirection.UpRight, KeyCode.E),
        new KeyValuePair<EDirection, KeyCode>(EDirection.DownLeft, KeyCode.Z),
        new KeyValuePair<EDirection, KeyCode>(EDirection.DownRight, KeyCode.X),
    };

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChooseSquad(lancerSquad);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChooseSquad(horseSquad);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChooseSquad(shieldSquad);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                CellController cellController;
                if (hit.collider.TryGetComponent(out cellController))
                {
                    if (cellController.cell.squadInCell != null && cellController.cell.squadInCell.team == ETeam.Main)
                    {
                        ChooseSquad(cellController.cell.squadInCell);
                    }
                }
            }
        }

        for (int i = 0; i < keyCodesDirection.Length; i++)
        {
            if (Input.GetKeyDown(keyCodesDirection[i].Value))
            {
                if (currentSquadToCommand != null)
                {
                    if (gameState.currentPlayerTurnPoints > 0)
                    {
                        bool isMoved = currentSquadToCommand.Move(keyCodesDirection[i].Key);
                        if (isMoved)
                        {
                            gameState.DicreasePlayerPoints(1);
                        }
                    }
                    
                }
               
            }
        }
    }

    private void ChooseSquad(SquadController squad)
    {
        currentSquadToCommand = squad;
        Debug.Log("Choosen squad " + squad.typeOfSquad);
    }

    
}
