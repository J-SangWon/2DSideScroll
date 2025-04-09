using UnityEngine;

public class EnemySkeletonIdle : EnemyState
{
    private Enemy_Skeleton enemy;
    public EnemySkeletonIdle(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animationName, Enemy_Skeleton enemy) : base(_enemyBase, _enemyStateMachine, _animationName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            enemyStateMachine.ChangeState(enemy.enemyMove);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
