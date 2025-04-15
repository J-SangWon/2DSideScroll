using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        //아래 2줄은 동일함
        SkillManager.instance.clone.CreateClone(player.transform); 
        //player.skill.clone.CreateClone(player.transform);
    }
    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);

        if (stateTimer < 0) stateMachine.ChangeState(player.idleState);

        if(!player.IsGroundDetected() && player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.linearVelocityY);
    }

}
