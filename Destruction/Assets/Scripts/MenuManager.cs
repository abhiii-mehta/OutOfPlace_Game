using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject soundPanel;
    public GameObject creditsPanel;
    public GameObject levelSelectPanel;
    public GameObject loadingScreen;

    private bool soundPanelVisible = false;
    private bool creditsPanelVisible = false;

    public void ToggleSoundPanel()
    {
        soundPanelVisible = !soundPanelVisible;
        soundPanel.SetActive(soundPanelVisible);
    }

    public void ShowLevelSelect()
    {
        levelSelectPanel.SetActive(true);
    }

    public void HideLevelSelect()
    {
        levelSelectPanel.SetActive(false);
    }

    public void LoadLevel(string levelName)
    {
        if (Application.CanStreamedLevelBeLoaded(levelName))
        {
            StartCoroutine(LoadLevelAsync(levelName));
        }
        else
        {
            Debug.LogWarning(" Scene '" + levelName + "' is not added to Build Settings!");
        }
    }

    private IEnumerator LoadLevelAsync(string levelName)
    {
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        while (!operation.isDone)
        {
            yield return null;
        }
    }



    public void ExitGame()
    {
        Application.Quit();
    }

    public void CloseSoundPanel()
    {
        soundPanelVisible = false;
        soundPanel.SetActive(false);
    }

    private void Update()
    {
        if (soundPanelVisible && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSoundPanel();
        }
        else if (creditsPanelVisible && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCreditsPanel();
        }
    }

    public void OpenCreditsPanel()
    {
        creditsPanelVisible = true;
        creditsPanel.SetActive(true);
    }

    public void CloseCreditsPanel()
    {
        creditsPanelVisible = false;
        creditsPanel.SetActive(false);
    }
}
