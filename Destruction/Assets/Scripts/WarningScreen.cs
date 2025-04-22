using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScreen : MonoBehaviour
{
    public float delayBeforeNextScene = 4.5f;
    public string nextSceneName = "MainMenu";

    void Start()
    {
        Invoke("LoadNextScene", delayBeforeNextScene);
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
