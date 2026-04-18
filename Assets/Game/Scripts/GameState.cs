using UnityEngine;

public class GameState : MonoBehaviour
{
    public EnemyAIController enemy;

    public int pointsPerTurn;

    [HideInInspector]
    public int roundIdx = 0;

    [HideInInspector]
    public int currentPlayerTurnPoints;
    [HideInInspector]
    public int currentEnemyTurnPoints;

    private void Start()
    {
        currentPlayerTurnPoints = pointsPerTurn;
        currentEnemyTurnPoints = pointsPerTurn;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateRound();
        }
    }

    public void DicreasePlayerPoints(int point)
    {
        currentPlayerTurnPoints = Mathf.Clamp(currentPlayerTurnPoints - point, 0, pointsPerTurn);
    }

    public void DicreaseEnemyPoints(int point)
    {
        currentEnemyTurnPoints = Mathf.Clamp(currentEnemyTurnPoints - point, 0, pointsPerTurn);
    }

    public void UpdateRound()
    {
        Debug.Log("Round updated");
        currentPlayerTurnPoints = pointsPerTurn;
        currentEnemyTurnPoints = pointsPerTurn;
        roundIdx++;
        enemy.DoRandomMove();
    }
}
