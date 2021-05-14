using UnityEngine;

public enum Facing
{
    Up,
    Down,
    Left,
    Right
}

public class PlayerAnimationController : MonoBehaviour
{
    public CrashdownPlayerController playerController;

    public Animator spriteAnimator;
    
    public string animationIdleDown;
    public string animationWalkDown;
    public string animationDodgeDown;
    public string animationAttackDown;
    public string animationHurtDown;

    public string animationIdleUp;
    public string animationWalkUp;
    public string animationDodgeUp;
    public string animationAttackUp;
    public string animationHurtUp;

    public string animationIdleLeft;
    public string animationWalkLeft;
    public string animationDodgeLeft;
    public string animationAttackLeft;
    public string animationHurtLeft;

    public string animationIdleRight;
    public string animationWalkRight;
    public string animationDodgeRight;
    public string animationAttackRight;
    public string animationHurtRight;

    public string animationCrashdown;

    public float dashAnimLength;
    public float attackAnimLength;
    public float hurtAnimLength;
    public float crashdownLength;

    private float _timeForCurrentState = 0f;
    private float _timeInCurrentState = 0f;
    private bool _inActionAnimation = false;
    
    public void Update()
    {
        if (_inActionAnimation)
        {
            _timeInCurrentState += Time.deltaTime;
            if (_timeInCurrentState >= _timeForCurrentState)
            {
                _inActionAnimation = false;
                _timeForCurrentState = 0f;
                _timeInCurrentState = 0f;
            }
            else
            {
                return;
            }
        }

        string animToPlay = WalkOrIdleAnimation();

        if (playerController.InputDodgeDownThisFrame)
            animToPlay = DashAnimation();

        if (playerController.InputAttackDownThisFrame)
            animToPlay = AttackAnimation();

        if (playerController.WasDamagedThisFrame)
            animToPlay = HurtAnimation();

        if (playerController.InputCrashdownDownThisFrame)
            animToPlay = CrashdownAnimation();
        
        spriteAnimator.CrossFade(animToPlay, 0f);
    }

    private string WalkOrIdleAnimation()
    {
        if (playerController.InputMovementThisFrame.magnitude >= .05f)
        {
            switch (GetFacing())
            {
                case Facing.Down:
                    return animationWalkDown;
                case Facing.Up:
                    return animationWalkUp;
                case Facing.Left:
                    return animationWalkLeft;
                case Facing.Right:
                    return animationWalkRight;
                default:
                    return animationWalkDown;
            }
        }
        else
        {
            switch (GetFacing())
            {
                case Facing.Down:
                    return animationIdleDown;
                case Facing.Up:
                    return animationIdleUp;
                case Facing.Left:
                    return animationIdleLeft;
                case Facing.Right:
                    return animationIdleRight;
                default:
                    return animationIdleDown;
            }
        }
    }

    private string DashAnimation()
    {
        _inActionAnimation = true;
        _timeInCurrentState = 0f;
        _timeForCurrentState = dashAnimLength;

        switch(GetFacing())
        {
            case Facing.Down:
                return animationDodgeDown;
            case Facing.Up:
                return animationDodgeUp;
            case Facing.Left:
                return animationDodgeLeft;
            case Facing.Right:
                return animationDodgeRight;
            default:
                return animationDodgeDown;
        }
    }

    private string AttackAnimation()
    {
        _inActionAnimation = true;
        _timeInCurrentState = 0f;
        _timeForCurrentState = attackAnimLength;

        switch (GetFacing())
        {
            case Facing.Down:
                return animationAttackDown;
            case Facing.Up:
                return animationAttackUp;
            case Facing.Left:
                return animationAttackLeft;
            case Facing.Right:
                return animationAttackRight;
            default:
                return animationAttackDown;
        }
    }

    private string HurtAnimation()
    {
        _inActionAnimation = true;
        _timeInCurrentState = 0f;
        _timeForCurrentState = hurtAnimLength;

        switch (GetFacing())
        {
            case Facing.Down:
                return animationHurtDown;
            case Facing.Up:
                return animationHurtUp;
            case Facing.Left:
                return animationHurtLeft;
            case Facing.Right:
                return animationHurtRight;
            default:
                return animationHurtDown;
        }
    }

    private string CrashdownAnimation()
    {
        _inActionAnimation = true;
        _timeInCurrentState = 0f;
        _timeForCurrentState = crashdownLength;

        return animationCrashdown;
    }

    private Facing GetFacing()
    {
        float x = playerController.CurrentFacing.x;
        float z = playerController.CurrentFacing.z;

        if (Mathf.Abs(x) > Mathf.Abs(z))
        {
            if (x < 0) return Facing.Left;
            else return Facing.Right;
        }
        else
        {
            if (z < 0) return Facing.Down;
            else return Facing.Up;
        }
    }
}
