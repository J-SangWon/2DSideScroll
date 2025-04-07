using UnityEngine;

public class Player : Entity
{

    [Header("이동 정보")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 14f;
    private float xInput;

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

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        CheckInput();
        Movement();

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


    


}