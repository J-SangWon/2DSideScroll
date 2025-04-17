using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        //if (Input.GetKeyDown(KeyCode.LeftShift)) stateMachine.ChangeState(player.dashState);
        if (Input.GetKeyDown(KeyCode.Mouse0)) stateMachine.ChangeState(player.primaryAttackState);
        if(Input.GetKeyDown(KeyCode.Mouse1) && HasNotSword()) stateMachine.ChangeState(player.aimSwordState);

        if (!player.IsGroundDetected()) stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected()) stateMachine.ChangeState(player.jumpState);

        if(Input.GetKeyDown(KeyCode.F)) stateMachine.ChangeState(player.counterAtackState);
    }

    private bool HasNotSword()
    {
        if (player.sword == null)
        {
            return true;
        }
        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }

    public override void Exit()
    {
        base.Exit();
    }

}
