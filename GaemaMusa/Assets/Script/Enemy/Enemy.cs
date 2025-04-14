
using UnityEngine;

public class Enemy : Entity
{
  
    #region 정보
    [Header("이동 정보")]
    public int enemyMoveSpeed;
    public float idleTime;


    [Header("공격 정보")]
    public float playerCheckDistance = 1.5f;
    [SerializeField] protected LayerMask whatIsPlayer;
    public float attackDistance;
    public float attackCooldown;
    public float lastTimeAttacked;
    public float battleTime;

    [Header("스턴 정보")]
    public float stunDuration = 1f;
    public Vector2 stunDirection;
    protected bool canBeStunned = true;
    [SerializeField] protected GameObject counterImage;
    #endregion

    #region State
    public EnemyStateMachine enemyStateMachine { get; private set; }
    
    #endregion

    protected override void Awake()
    {
        base.Awake();
        enemyStateMachine = new EnemyStateMachine();
        

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

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        //Gizmos.DrawWireSphere(transform.position, playerCheckDistance);
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
        
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

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer);

    public virtual void AnimationFinishTrigger() => enemyStateMachine.currentEnemyState.AnimationFinishTrigger();

}
