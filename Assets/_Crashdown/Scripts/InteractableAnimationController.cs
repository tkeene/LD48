using UnityEngine;

public class InteractableAnimationController : MonoBehaviour
{
    public PlayerInteraction playerInteraction;
    
    public Animator spriteAnimator;

    public string animationIdle;
    public string animationReact;
    public float reactTime = .5f;

    private float _timeReacting = 0f;
    private bool _isReacting = false;

    public void Update()
    {
        if (_isReacting)
        {
            _timeReacting += Time.deltaTime;
            if (_timeReacting >= reactTime)
            {
                _isReacting = false;
                _timeReacting = 0f;
            }
            else
            {
                return;
            }
        }

        string animToPlay = animationIdle;

        if (playerInteraction.interactedWithThisFrame)
            animToPlay = React();

        spriteAnimator.CrossFade(animToPlay, 0f);
    }

    private string React()
    {
        _isReacting = true;
        _timeReacting = 0f;

        return animationReact;
    }
}
