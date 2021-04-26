using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public CrashdownEnemyActor Actor;

    public Animator spriteAnimator;

    public string animationIdle;
    public string animationMoving;

    public void Update()
    {
        string animToPlay = animationIdle;

        if (Actor.movedThisFrame)
        {
            animToPlay = animationMoving;
        }

        spriteAnimator.CrossFade(animToPlay, 0f);
    }
}
