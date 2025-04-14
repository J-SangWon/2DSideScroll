using UnityEngine;

public class EnemySkeletonStun : EnemyState
{
    private Enemy_Skeleton enemy;
    public EnemySkeletonStun(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBoolName, Enemy_Skeleton enemy) : base(_enemy, _enemyStateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.fx.InvokeRepeating("RedColorBlink",0, 0.1f);

        stateTimer = enemy.stunDuration;
        enemy.SetVelocity(-enemy.facingDir * enemy.stunDirection.x, enemy.stunDirection.y);

    }
    public override void Update()
    {
        base.Update();
        if (stateTimer < 0) enemyStateMachine.ChangeState(enemy.enemyIdle);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelRedBlink", 0);
    }

}
