using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartClicked()
    {
        SceneManager.LoadScene("PlayerTestRoom");
    }
}
