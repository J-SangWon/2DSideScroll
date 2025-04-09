using UnityEngine;

public class PlayerWallJump : PlayerAirState
{
    public PlayerWallJump(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        stateTimer = 0.4f;
        player.SetVelocity(player.wallJumpForce * -player.facingDir, player.wallJumpForce);

    }
    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
        
    }
    public override void Exit()
    {
        base.Exit();
    }
}
