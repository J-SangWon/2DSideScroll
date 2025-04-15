using UnityEngine;

public class EnemySkeletonGround : EnemyState
{
    protected Enemy_Skeleton enemy;
    protected Transform playerTransfrom;
    public EnemySkeletonGround(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animationName, Enemy_Skeleton _enemy) : base(_enemyBase, _enemyStateMachine, _animationName)
    {
        this.enemy = _enemy;
    }


    public override void Enter()
    {
        base.Enter();
        playerTransfrom = PlayerManager.instance.player.transform;
    }
    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, playerTransfrom.position) < 2 )
            enemyStateMachine.ChangeState(enemy.enemyBattle);
    }

    public override void Exit()
    {
        base.Exit();

    }

}
