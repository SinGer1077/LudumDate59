using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SquadController lancerSquad;
    public SquadController horseSquad;
    public SquadController shieldSquad;

    public GameState gameState;
    public Map map;

    private SquadController currentSquadToCommand;
    private Cell currentChoosenCell;

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
            ChooseSquad(lancerSquad, lancerSquad.currentCell.cellController);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChooseSquad(horseSquad, horseSquad.currentCell.cellController);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChooseSquad(shieldSquad, shieldSquad.currentCell.cellController);
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
                        ChooseSquad(cellController.cell.squadInCell, cellController);
                    }

                    if (currentSquadToCommand != null)
                    {
                        var neighbors = map.GetNeighbors(cellController.cell);
                        Cell neig = null;

                        for (int i = 0; i < neighbors.Length; i++)
                        {
                            if (neighbors[i].q == currentSquadToCommand.currentCell.q && neighbors[i].r == currentSquadToCommand.currentCell.r)
                            {
                                neig = cellController.cell;
                            }
                        }


                        if (neig != null)
                        {
                            if (currentChoosenCell == neig)
                            {
                                Debug.Log(new Vector2Int(cellController.cell.q - currentSquadToCommand.currentCell.q, cellController.cell.r - currentSquadToCommand.currentCell.r));
                                bool isMoved = currentSquadToCommand.Move(new Vector2Int(cellController.cell.q - currentSquadToCommand.currentCell.q, cellController.cell.r - currentSquadToCommand.currentCell.r));
                                if (isMoved)
                                {
                                    gameState.DicreasePlayerPoints(1);
                                }
                                currentChoosenCell = null;
                                ChooseSquad(currentSquadToCommand, currentSquadToCommand.currentCell.cellController);
                            }
                            else if (currentChoosenCell != null)
                            {
                                currentChoosenCell.cellController.ResetChoose();
                                currentChoosenCell = neig;
                                neig.cellController.SetChoose();
                            }
                            else
                            {
                                currentChoosenCell = neig;
                                neig.cellController.SetChoose();
                            }

                        }
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
                        var neig = map.GetNeighbor(currentSquadToCommand.currentCell, keyCodesDirection[i].Key);
                        if (neig != null)
                        {
                            if (currentChoosenCell == neig)
                            {
                                bool isMoved = currentSquadToCommand.Move(keyCodesDirection[i].Key);
                                if (isMoved)
                                {
                                    gameState.DicreasePlayerPoints(1);
                                }
                                currentChoosenCell = null;
                                ChooseSquad(currentSquadToCommand, currentSquadToCommand.currentCell.cellController);
                            }
                            else if (currentChoosenCell != null)
                            {
                                currentChoosenCell.cellController.ResetChoose();
                                currentChoosenCell = neig;
                                neig.cellController.SetChoose();
                            }
                            else
                            {
                                currentChoosenCell = neig;
                                neig.cellController.SetChoose();
                            }

                        }
 
                    }
                    
                }
               
            }
        }
    }

    private void ChooseSquad(SquadController squad, CellController cell)
    {
        currentSquadToCommand = squad;
        Debug.Log("Choosen squad " + squad.typeOfSquad);

        map.ResetCells(new ECellSprite[0]);

        if (cell != null)
        {
            cell.SetSprite(ECellSprite.Chosen);
            Cell[] neighbors = map.GetNeighbors(cell.cell);
            foreach (Cell neighbor in neighbors)
            {
                neighbor.cellController.SetSprite(ECellSprite.Path);
            }
        }
    }
}
