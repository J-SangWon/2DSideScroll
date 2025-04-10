using UnityEngine;

public class EnemySkeletonIdle : EnemySkeletonGround
{
    public EnemySkeletonIdle(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animationName, Enemy_Skeleton _enemy) : base(_enemyBase, _enemyStateMachine, _animationName, _enemy)
    {
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
