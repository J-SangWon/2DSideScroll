using UnityEngine;

public class DNF_FSF : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 14f;

    [Header("대쉬 정보")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 2f;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown = 4f;
    private float dashCooldownTimer = 0f;

    [Header("공격 정보")]
    private bool isAttacking;
    private int comboCounter;
    private float comboTime = 0.5f;
    private float comboTimer;


    [Header("이동")]
    private float xInput;
    private int facingDir = 1;
    private bool facingRight = true;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {

        CheckInput();
        Movement();
        CollisionChecks();

        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimer -= Time.deltaTime;

        FlipController();
        AnimatorControllers();

    }

    public void AttackOver()
    {
        isAttacking = false;

        comboCounter++;
        if (comboCounter > 2) comboCounter = 0;
    }
    private void StartAttackEvent()
    {
        if (comboTimer < 0)
            comboCounter = 0;

        isAttacking = true;
        comboTimer = comboTime;
    }
    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }


    }

    private void DashAbility()
    {

        if (dashCooldownTimer < 0 && !isAttacking)
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
        }
    }

    private void Movement()
    {
        if (isAttacking)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else if (dashTime > 0)
        {
            rb.linearVelocity = new Vector2(facingDir * dashSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        }


    }

    private void Jump()
    {
        if (isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }


    //Alt + 화살표키
    private void AnimatorControllers()
    {

        bool isMoving = rb.linearVelocity.x != 0;


        anim.SetFloat("yVelocity", rb.linearVelocityY);

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGround", isGrounded);
        anim.SetBool("isDash", dashTime > 0);
        anim.SetBool("isAttack", isAttacking);
        anim.SetInteger("ComboCounter", comboCounter);
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