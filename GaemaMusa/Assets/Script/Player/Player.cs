﻿using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Entity
{
    #region 정보
    [Header("이동 정보")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    public float dashSpeed = 15f;
    public float dashDuration = 1.5f;
    public float dashDir { get; private set; }
    

    public bool isBusy = false;

    [Header("공격 디테일")]
    public float[] attackMovement;
    public float attackDamage = 10f;
    public float counterAttackDuration = 0.3f;


    public float wallJumpForce = 15f;

    private float timer;

    public GameObject sword; //{ get; private set; }
    public float swordReturnKnockback = 5f;
    #endregion

    #region State
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerGroundedState groundState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJump wallJump { get; private set; }
    public PlayerPrimaryAttack primaryAttackState { get; private set; }
    public PlayerCounterAttackState counterAtackState { get; private set; }
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    public SkillManager skill { get; private set; }
    public PlayerBlackHoleState blackHoleState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    #endregion



    protected override void Awake()
    {
        base.Awake();
        // 각 상태 인스턴스 생성 (this: 플레이어 객체, stateMachine: 상태 머신, "Idle"/"Move": 상태 이름)
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJump(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttack(this, stateMachine, "Attack");
        counterAtackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        blackHoleState = new PlayerBlackHoleState(this, stateMachine, "Idle");
        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }
    protected override void Start()
    {
        base.Start();

        skill = SkillManager.instance;

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        FlipController();
        timer -= Time.deltaTime;
        CheckForDashInput();

        if(Input.GetKeyDown(KeyCode.H))
        {
            skill.crystal.CanUseSkill();
        }
    }

    public void AssignNewSword(GameObject _newSword)
    {
        sword = _newSword;
    }
    public void CatchTheSword()
    {
        stateMachine.ChangeState(catchSwordState);
        Destroy(sword);
    }

    public void ExitBlackHoleAbillity()
    {
        stateMachine.ChangeState(airState);
    }


    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    

    private void CheckForDashInput()
    {
        if (IsWallDetected())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0) dashDir = facingDir;
            stateMachine.ChangeState(dashState);
        }
    }

    
    

}
