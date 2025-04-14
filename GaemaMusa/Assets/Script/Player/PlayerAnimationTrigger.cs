using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{

    private Player player => GetComponentInParent<Player>();
    
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
        
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                collider.GetComponent<Enemy>().Damage(player.attackDamage);
            }
        }
    }

}
