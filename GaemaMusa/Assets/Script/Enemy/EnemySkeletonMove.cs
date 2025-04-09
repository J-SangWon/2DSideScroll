using UnityEngine;

public class EnemySkeletonMove : EnemyState
{
    private Enemy_Skeleton enemy;
    public EnemySkeletonMove(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animationName, Enemy_Skeleton _enemy) : base(_enemy, _enemyStateMachine, _animationName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.facingDir * enemy.enemyMoveSpeed, enemy.rb.linearVelocityY);
        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            enemyStateMachine.ChangeState(enemy.enemyIdle);
        }
    }
}
