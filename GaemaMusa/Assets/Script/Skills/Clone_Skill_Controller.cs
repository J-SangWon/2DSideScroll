using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float colorLoosingSpeed = 0.5f;
    private float cloneTimer;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = 0.8f;
    Transform closestTarget;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer <= 0)
        {
            sr.color = new Color(1,1,1,sr.color.a - Time.deltaTime * colorLoosingSpeed);

            if(sr.color.a <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetupClone(Transform _transform, float cloneDuration, bool _canAttack)
    {
        if (_canAttack) anim.SetInteger("AttackNumber", Random.Range(1, 4));
        
        transform.position = _transform.position;
        cloneTimer = cloneDuration;

        FaceClosetTarget();
    }


    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
                collider.GetComponent<Enemy>().Damage(0);
        }
    }

    private void FaceClosetTarget()
    {
        // 전체 충돌체를 가져와서 가장 가까운 적을 찾습니다.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, 25);
        float closestDistance = Mathf.Infinity;
        
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = collider.transform;
                }
            }
        }
        if (closestTarget != null)
        {
            if(closestTarget.position.x < transform.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }




}
