using UnityEngine;

public class Enemy_Skeleton : Entity
{
    [Header("이동 정보")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Player detection")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D isPlayerDetected;
    private bool isAttacking;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                //추적
                rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocityY);
                isAttacking = false;
            }
            else
            {
                Debug.Log("공격 " + isPlayerDetected.collider.gameObject.name);
                isAttacking = true;
            }
        }

        if (!isGrounded || isWall) Flip();
        Movement();

    }

    private void Movement()
    {
        if(!isAttacking)
        rb.linearVelocity = new Vector2(moveSpeed * facingDir, rb.linearVelocityY);
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDir, whatIsPlayer);

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDir, transform.position.y));
    }

}
