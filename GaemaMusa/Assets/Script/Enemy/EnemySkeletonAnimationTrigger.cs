using UnityEngine;

public class EnemySkeletonAnimationTrigger : MonoBehaviour
{

    private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

    private void AnimatonTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

}
