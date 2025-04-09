
using UnityEngine;

public class Enemy : Entity
{
  
    #region 정보
    [Header("이동 정보")]
    public int enemyMoveSpeed;
    public float idleTime;

    public float playerCheckDistance;
    public LayerMask whatIsPlayer;

    public Transform playerTransform;

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

        
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    protected override void Update()
    {
        base.Update();
        enemyStateMachine.currentEnemyState.Update();
        
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, playerCheckDistance);
    }

   

    //public bool PlayerCheck() => Physics2D.Raycast();

   

}
