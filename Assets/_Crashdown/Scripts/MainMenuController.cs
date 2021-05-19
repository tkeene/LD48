using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartClicked()
    {
        SceneManager.LoadScene("PlayerTestRoom");
    }

    public void GUI_OpenWebpage(string url)
    {
        Application.OpenURL(url);
    }

    public void GUI_Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
