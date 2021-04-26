using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public CrashdownEnemyActor Actor;

    public Animator spriteAnimator;
    public GameObject enragedSprite;

    public string animationIdleLeft;
    public string animationIdleRight;
    public string animationMovingLeft;
    public string animationMovingRight;
    public string animationFiringLeft;
    public string animationFiringRight;
    public string animationDyingLeft;
    public string animationDyingRight;

    public float fireAnimationLength = .2f;


    private float _timeFiring = 0f;
    private bool _isFiring = false;

    public void Update()
    {
        if (_isFiring)
        {
            _timeFiring += Time.deltaTime;
            if (_timeFiring >= fireAnimationLength)
            {
                _isFiring = false;
                _timeFiring = 0f;
            }
            else
            {
                return;
            }
        }

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
        else if (Actor.firedThisFrame)
        {
            animToPlay = Fire();
        }

        if (enragedSprite != null)
        {
            bool enraged = Actor.CurrentAiState != CrashdownEnemyActor.EAiState.Dying
                        && Actor.IsEnraged();
            enragedSprite.SetActive(enraged);
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

    private string Fire()
    {
        if (string.IsNullOrEmpty(animationFiringLeft) || string.IsNullOrEmpty(animationFiringRight))
        {
            return WalkOrIdleAnimation();
        }

        _isFiring = true;
        _timeFiring = 0f;

        switch (GetFacing())
        {
            case Facing.Left:
                return animationFiringLeft;
            case Facing.Right:
                return animationFiringRight;
            default:
                return animationFiringRight;
        }
    }

    private Facing GetFacing()
    {
        float x = Actor.CurrentFacing.x;
        
        if (x < 0) return Facing.Left;
        else return Facing.Right;
    }
}
