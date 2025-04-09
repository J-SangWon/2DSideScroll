using UnityEngine;

public class EnemySkeleltonAttack : EnemyState
{
    private Enemy_Skeleton enemy;
    public EnemySkeleltonAttack(Enemy _enemybase, EnemyStateMachine _enemyStateMachine, string _animationName, Enemy_Skeleton _enemy) : base(_enemy, _enemyStateMachine, _animationName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        //if (enemyBase.playerTransform != null)
        //{
        //    float directionToPlayer = enemyBase.playerTransform.position.x - enemyBase.transform.position.x;
        //    if ((enemyBase.FacingDir == 1 && directionToPlayer < 0) || (enemyBase.FacingDir == -1 && directionToPlayer > 0))
        //    {
        //        enemyBase.Flip();
        //    }
        //}
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
