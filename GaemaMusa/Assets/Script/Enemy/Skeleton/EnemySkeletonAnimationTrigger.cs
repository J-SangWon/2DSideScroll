using UnityEngine;

public class EnemySkeletonAnimationTrigger : MonoBehaviour
{

    private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

    private void AnimatonTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Player>() != null)
            {
                collider.GetComponent<Player>().Damage(enemy.attackDamage);
            }
        }
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
