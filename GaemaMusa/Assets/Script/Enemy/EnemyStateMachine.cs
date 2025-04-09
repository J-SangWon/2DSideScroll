using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentEnemyState { get; private set; }

    public void InitializeEnemyState(EnemyState initState)
    {
        currentEnemyState = initState;
        currentEnemyState.Enter();
    }
    public void ChangeState(EnemyState enemyState)
    {
        currentEnemyState.Exit();
        currentEnemyState = enemyState;
        currentEnemyState.Enter();
    }



}
