﻿using UnityEngine;
using System.Collections;
using System.Xml;

public class Enemy_Skeleton : Enemy
{
    #region States
    public EnemySkeletonIdle enemyIdle { get; private set; }
    public EnemySkeletonMove enemyMove { get; private set; }
    public EnemySkeletonBattle enemyBattle { get; private set; }
    public EnemySkeletonAttack enemyAttack { get; private set; }
    public EnemySkeletonStun enemyStun { get; private set; }
    public EnemySkeletonDeadState enemyDead { get; private set; }
    #endregion

    #region 정보
    [Header("공갹 정보")]
    public float attackDamage = 2f;
    #endregion


    protected override void Awake()
    {
        base.Awake();
        enemyIdle = new EnemySkeletonIdle(this, enemyStateMachine, "Idle", this);
        enemyMove = new EnemySkeletonMove(this, enemyStateMachine, "Move", this);
        enemyBattle = new EnemySkeletonBattle(this, enemyStateMachine, "Move", this);
        enemyAttack = new EnemySkeletonAttack(this, enemyStateMachine, "Attack", this);
        enemyStun = new EnemySkeletonStun(this, enemyStateMachine, "Stun", this);
        enemyDead = new EnemySkeletonDeadState(this, enemyStateMachine, "Idle", this);
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
        if (Input.GetKeyDown(KeyCode.Keypad4)) enemyStateMachine.ChangeState(enemyStun);
    }

    public override bool CanBeStunned()
    {
        if(base.CanBeStunned())
        {
            enemyStateMachine.ChangeState(enemyStun);  
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Death()
    {
        base.Death();
        enemyStateMachine.ChangeState(enemyDead);
    }



}
