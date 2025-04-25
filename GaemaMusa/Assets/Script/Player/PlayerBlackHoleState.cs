using UnityEngine;

public class PlayerBlackHoleState : PlayerState
{
    private float flytime = 0.4f;
    private bool skillUsed;
    private float defaultGravityScale;

    public PlayerBlackHoleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        defaultGravityScale = player.rb.gravityScale;

        skillUsed = false;
        stateTimer = flytime;
        rb.gravityScale = 0f;
    }


    public override void Update()
    {
        base.Update();
        if (stateTimer > 0)
        {
            rb.linearVelocity = new Vector2(0, 15);
        }
        if (stateTimer <= 0)
        {
            rb.linearVelocity = new Vector2(0, -0.5f);
            if (!skillUsed)
            {
                if (player.skill.blackhole.CanUseSkill())
                {
                    player.skill.blackhole.UseSkill();
                    skillUsed = true;
                }

            }

            if (player.skill.blackhole.BlackHoleFinished())
            {
                stateMachine.ChangeState(player.airState);
            }
        }
    }
    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = defaultGravityScale;
        player.MakeTransparent(false);
    }

}
