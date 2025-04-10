using UnityEngine;
using System.Collections;

public class Enemy_Skeleton : Enemy
{
    #region States
    public EnemySkeletonIdle enemyIdle { get; private set; }
    public EnemySkeletonMove enemyMove { get; private set; }
    public EnemySkeletonBattle enemyBattle { get; private set; }
    public EnemySkeletonAttack enemyAttack { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();
        enemyIdle = new EnemySkeletonIdle(this, enemyStateMachine, "Idle", this);
        enemyMove = new EnemySkeletonMove(this, enemyStateMachine, "Move", this);
        enemyBattle = new EnemySkeletonBattle(this, enemyStateMachine, "Move", this);
        enemyAttack = new EnemySkeletonAttack(this, enemyStateMachine, "Attack", this);
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

    
    
}
