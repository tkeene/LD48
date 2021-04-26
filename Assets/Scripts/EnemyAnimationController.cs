using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public CrashdownEnemyActor Actor;

    public Animator spriteAnimator;

    public string animationIdleLeft;
    public string animationIdleRight;
    public string animationMovingLeft;
    public string animationMovingRight;
    public string animationDyingLeft;
    public string animationDyingRight;

    public void Update()
    {
        string animToPlay = WalkOrIdleAnimation();

        if (Actor.CurrentAiState == CrashdownEnemyActor.EAiState.Dying)
        {
            animToPlay = DeathAnimation();
        }
        else if (Actor.CurrentAiState == CrashdownEnemyActor.EAiState.IsDead)
        {
            spriteAnimator.StopPlayback();
            return;
        }

        spriteAnimator.CrossFade(animToPlay, 0f);
    }

    private string WalkOrIdleAnimation()
    {
        if (Actor.movedThisFrame)
        {
            switch (GetFacing())
            {
                case Facing.Left:
                    return animationMovingLeft;
                case Facing.Right:
                    return animationMovingRight;
                default:
                    return animationMovingRight;
            }
        }
        else
        {
            switch (GetFacing())
            {
                case Facing.Left:
                    return animationIdleLeft;
                case Facing.Right:
                    return animationIdleRight;
                default:
                    return animationIdleRight;
            }
        }
    }

    private string DeathAnimation()
    {
        if (string.IsNullOrEmpty(animationDyingRight) || string.IsNullOrEmpty(animationDyingLeft))
        {
            // no dying animation for this enemy, default to idle.
            switch (GetFacing())
            {
                case Facing.Left:
                    return animationIdleLeft;
                case Facing.Right:
                    return animationIdleRight;
                default:
                    return animationIdleRight;
            }
        }

        switch (GetFacing())
        {
            case Facing.Left:
                return animationDyingLeft;
            case Facing.Right:
                return animationDyingRight;
            default:
                return animationDyingRight;
        }
    }

    private Facing GetFacing()
    {
        float x = Actor.CurrentFacing.x;
        
        if (x < 0) return Facing.Left;
        else return Facing.Right;
    }
}
