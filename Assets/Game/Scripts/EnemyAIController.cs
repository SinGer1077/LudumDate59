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
            if (enemySquads[randomSquad].currentHP <= 0) continue;

            enemySquads[randomSquad].Move((EDirection)Random.Range(0, 6));
            state.DicreaseEnemyPoints(1);
        }
    }

    public bool HaveArmy()
    {
        foreach (var squad in enemySquads)
        {
            if (squad.currentHP > 0)
            {
                return true;
            }
        }

        return false;
    }
}
