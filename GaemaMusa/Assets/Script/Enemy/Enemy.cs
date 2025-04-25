
using System.Collections;
using UnityEngine;

public class Enemy : Entity
{

    #region 정보
    [Header("이동 정보")]
    public float enemyMoveSpeed;
    public float idleTime;
    public float battleTime;
    private float defaultMoveSpeed;

    [Header("공격 정보")]
    public float playerCheckDistance = 1.5f;
    [SerializeField] protected LayerMask whatIsPlayer;
    public float attackDistance;
    public float attackCooldown;
    public float lastTimeAttacked;

    [Header("스턴 정보")]
    public float stunDuration = 1f;
    public Vector2 stunDirection;
    protected bool canBeStunned = true;
    [SerializeField] protected GameObject counterImage;
    #endregion

    #region 
    public EnemyStateMachine enemyStateMachine { get; private set; }
    public string lastAnimBoolName { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        enemyStateMachine = new EnemyStateMachine();
        defaultMoveSpeed = enemyMoveSpeed;

    }
    protected override void Start()
    {
        base.Start();


    }

    protected override void Update()
    {
        base.Update();
        enemyStateMachine.currentEnemyState.Update();

    }

    public virtual void AssignLastAnimBoolName(string _animBoolName)
    {
        lastAnimBoolName = _animBoolName;
    }
    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            enemyMoveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            enemyMoveSpeed = defaultMoveSpeed;
            anim.speed = 1;
        }
    }
    protected virtual IEnumerator FreezeTimerFor(float _seconds)
    {
        FreezeTime(true);

        yield return new WaitForSeconds(_seconds);

        FreezeTime(false);
    }



    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #region Gizmos
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer);

    public virtual void AnimationFinishTrigger() => enemyStateMachine.currentEnemyState.AnimationFinishTrigger();
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        //Gizmos.DrawWireSphere(transform.position, playerCheckDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));

    }
    #endregion


}
