using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 2f;
    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0;

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow) comboCounter = 0;
        player.anim.SetInteger("ComboCounter", comboCounter);
        //player.anim.speed = 3f;

        float attackDir = player.facingDir;

        if(xInput != 0)
        {
            attackDir = xInput;
        }
        player.SetVelocity(player.attackMovement[comboCounter] * attackDir, rb.linearVelocityY);

        stateTimer = 0.1f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            rb.linearVelocity = new Vector2(0, 0);

        if (triggerCalled) stateMachine.ChangeState(player.idleState);
    }
    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttacked = Time.time;
        player.anim.speed = 1f;

        player.StartCoroutine("BusyFor", 0.7f);
    }


}
