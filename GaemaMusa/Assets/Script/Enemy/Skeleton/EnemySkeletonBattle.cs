using UnityEngine;

public class EnemySkeletonBattle : EnemyState
{
    private Enemy_Skeleton enemy;
    private Transform playerTransform;
    private int moveDir;
    public EnemySkeletonBattle(Enemy _enemybase, EnemyStateMachine _enemyStateMachine, string _animationName, Enemy_Skeleton _enemy) : base(_enemy, _enemyStateMachine, _animationName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                //공격상태
                if (CanAttack()) enemyStateMachine.ChangeState(enemy.enemyAttack);
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(playerTransform.position, enemy.transform.position) > 7)
                enemyStateMachine.ChangeState(enemy.enemyIdle);
        }

        if (playerTransform.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
            if (enemy.facingDir == -1) enemy.Flip();
        }
        else if (playerTransform.position.x < enemy.transform.position.x)
        {
            moveDir = -1;
            if (enemy.facingDir == 1) enemy.Flip();
        }

        enemy.SetVelocity(enemy.enemyMoveSpeed * moveDir, rb.linearVelocity.y);

    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
