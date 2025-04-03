using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody2D rb;
    private Animator anim;
    [Header("플레이어 속성")]
    float MoveSpeed = 10f;
    float JumpForce = 14f;

    private int facingDir = 1;
    private bool facingRight = true;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance = 1.4f;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Movement();
        KeyInput();

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        
        AnimationController();
        FlipController();
    }

    void KeyInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(isGrounded == true) Jump();
        }


    }

    private void Movement()
    {
        float moveX = MoveSpeed * Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, JumpForce);
        
    }

    private void AnimationController()
    {
        bool isMoving = rb.linearVelocityX != 0;
        anim.SetBool("isMoving", isMoving);

    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void FlipController()
    {
        if (rb.linearVelocityX > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.linearVelocityX < 0 && facingRight)
        {
            Flip();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
