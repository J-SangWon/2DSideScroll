using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMachine enemyStateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;

    protected bool triggerCalled;
    private string animBoolName;

    protected float stateTimer;
    //protected Rigidbody2D rb;

    public EnemyState(Enemy _enemy, EnemyStateMachine _enemyStateMachine, string _animBoolName)
    {
        this.enemyBase = _enemy;
        this.enemyStateMachine = _enemyStateMachine;
        this.animBoolName = _animBoolName;

    }

    public virtual void Enter()
    {
        enemyBase.anim.SetBool(animBoolName, true);
        rb = enemyBase.rb;

        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

}
