using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialContextSwitcher : MonoBehaviour
{
    private string _keyboardMouseId = "Keyboard&Mouse";
    
    public PlayerInput playerInput;
    public GameObject keyboardMessage;
    public GameObject controllerMessage;


    public void OnEnable()
    {
        EnableMessage(playerInput.currentControlScheme);
    }
    
    public void OnControlsChanged()
    {
        EnableMessage(playerInput.currentControlScheme);
    }

    private void EnableMessage(string currentScheme)
    {
        if (currentScheme == _keyboardMouseId)
        {
            keyboardMessage.SetActive(true);
            controllerMessage.SetActive(false);
        }
        else
        {
            keyboardMessage.SetActive(false);
            controllerMessage.SetActive(true);
        }
    }
}
