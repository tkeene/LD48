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

    public string animationIdleUp;
    public string animationWalkUp;
    public string animationDodgeUp;

    public string animationIdleLeft;
    public string animationWalkLeft;
    public string animationDodgeLeft;

    public string animationIdleRight;
    public string animationWalkRight;
    public string animationDodgeRight;

    public float dashAnimLength;

    private float _timeForCurrentState = 0f;
    private float _timeInCurrentState = 0f;
    private bool _inActionAnimation = false;
    
    void Update()
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
        Debug.Log("Dashing?");

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
