using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 정보
    [Header("이동 정보")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public float dashSpeed = 15f;
    public float dashDuration = 1.5f;
    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    [Header("충돌 정보")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 1.5f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 3f;
    [SerializeField] private LayerMask whatIsGround;

    private float timer;
    #endregion

    #region State
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerGroundedState groundState { get; private set; }
    public PlayerAirState airSteate { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    #endregion

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    private void Awake()
    {
        // 각 상태 인스턴스 생성 (this: 플레이어 객체, stateMachine: 상태 머신, "Idle"/"Move": 상태 이름)
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airSteate = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
    }
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    void Update()
    {
        stateMachine.currentState.Update();
        FlipController();

        timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R) && timer < 0)
        {
            timer = 5f;
        }
    }

    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void FlipController()
    {
        if (rb.linearVelocityX > 0 && !facingRight) Flip();
        else if (rb.linearVelocityX < 0 && facingRight) Flip();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

}
