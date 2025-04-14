using Unity.VisualScripting;
using UnityEngine;

public class EnemySkeletonAttack : EnemyState
{
    private Enemy_Skeleton enemy;
    public EnemySkeletonAttack(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        enemy.SetZeroVelocity();

        if (triggerCalled) enemyStateMachine.ChangeState(enemy.enemyBattle);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTimeAttacked = Time.time;
    }

    
}
