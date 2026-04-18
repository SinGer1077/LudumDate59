using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    public SquadController[] enemySquads;
    public GameState state;

    public void DoRandomMove()
    {
        while (state.currentEnemyTurnPoints > 0)
        {
            int randomSquad = Random.Range(0, enemySquads.Length);
            enemySquads[randomSquad].Move((EDirection)Random.Range(0, 6));
            state.DicreaseEnemyPoints(1);
        }
    }
}
