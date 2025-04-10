using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    private EnemySkeleton enemy => GetComponentInParent<EnemySkeleton>();

    public void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
