using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region 정보
    [Header("충돌 정보")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance = 1.5f;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance = 3f;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected LayerMask whatIsWall;

    [Header("공격 정보")]
    public Transform attackCheck;
    public float attackCheckRadius = 1.5f;

    [Header("넉백 정보")]
    [SerializeField] protected Vector2 knockbackDirecton;
    [SerializeField] protected float knockbackDuration = 0.3f;
    protected bool isKnockback = false;
    #endregion

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    #endregion

    [Header("방향 정보")]
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        fx = GetComponent<EntityFX>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    public virtual void Death()
    {
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.simulated = false;
        anim.SetTrigger("Death");
        
    }
    public virtual void Damage(float damage)
    {
        fx.StartCoroutine("HitEffect");
        StartCoroutine(HitKnockBack());
        Debug.Log("Damage: " + damage);
    }

   protected virtual IEnumerator HitKnockBack()
    {
        isKnockback = true;
        rb.linearVelocity = new Vector2(knockbackDirecton.x * -facingDir, knockbackDirecton.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnockback = false;
    }
    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

    }
    public virtual void FlipController()
    {
        if (rb.linearVelocityX > 0 && !facingRight) Flip();
        else if (rb.linearVelocityX < 0 && facingRight) Flip();
    }

    #region 속력
    public void SetZeroVelocity()
    {
        if (isKnockback) return;
        rb.linearVelocity = new Vector2(0, 0);

    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnockback) return;
        rb.linearVelocity = new Vector2(_xVelocity, _yVelocity);
    }
    #endregion
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
}
