using UnityEngine;
using System.Collections;

public class Enemy_Skeleton : Enemy
{
    #region States
    public EnemySkeletonIdle enemyIdle { get; private set; }
    public EnemySkeletonMove enemyMove { get; private set; }
    public EnemySkeleltonAttack enemyAttack { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();
        enemyIdle = new EnemySkeletonIdle(this, enemyStateMachine, "Idle", this);
        enemyMove = new EnemySkeletonMove(this, enemyStateMachine, "Move", this);
        enemyAttack = new EnemySkeleltonAttack(this, enemyStateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();

        enemyStateMachine.InitializeEnemyState(enemyIdle);
    }

    protected override void Update()
    {
        base.Update();

        StateInputController();
    }


    public void StateInputController()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) enemyStateMachine.ChangeState(enemyIdle);
        if (Input.GetKeyDown(KeyCode.Keypad2)) enemyStateMachine.ChangeState(enemyMove);
        if (Input.GetKeyDown(KeyCode.Keypad3)) enemyStateMachine.ChangeState(enemyAttack);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (whatIsPlayer == (whatIsPlayer | (1 << collision.gameObject.layer)))
    //    {
    //        playerTransform = collision.transform; // 감지된 플레이어의 Transform 저장
    //        if (enemyStateMachine.currentEnemyState != enemyAttack && IsPlayerInFront())
    //        {
    //            enemyStateMachine.ChahgeState(enemyAttack);
    //        }
    //    }
    //}
    //public bool IsPlayerInFront()
    //{
    //    if (playerTransform == null) return false; // 플레이어가 감지되지 않았으면 false 반환

    //    float directionToPlayer = playerTransform.position.x - transform.position.x;
    //    // 적의 현재 방향과 플레이어 방향 비교
    //    if ((FacingDir == 1 && directionToPlayer > 0) || (FacingDir == -1 && directionToPlayer < 0))
    //    {
    //        return true; // 플레이어가 적의 앞쪽에 있음
    //    }
    //    else
    //    {
    //        return false; // 플레이어가 적의 뒤쪽에 있음
    //    }
    //}
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (whatIsPlayer == (whatIsPlayer | (1 << collision.gameObject.layer)))
    //    {
    //        playerTransform = collision.transform; // 감지된 플레이어의 Transform 저장
    //        if (enemyStateMachine.currentEnemyState != enemyAttack && !IsPlayerInFront())
    //        {
    //            // 플레이어가 뒤에 있으면 필요에 따라 방향을 전환
    //            Flip();
    //        }
    //        else if (enemyStateMachine.currentEnemyState != enemyAttack && IsPlayerInFront())
    //        {
    //            enemyStateMachine.ChahgeState(enemyAttack);
    //        }
    //    }
    //}
    
}
