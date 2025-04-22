using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject soundPanel;
    public GameObject creditsPanel;
    public GameObject levelSelectPanel;
    public GameObject loadingScreen;
    [Header("Gameplay UI")]
    public GameObject pausePanel;
    [Header("Endgame UI")]
    public GameObject winPanel;
    public GameObject losePanel;

    private bool isPaused = false;

    private bool soundPanelVisible = false;
    private bool creditsPanelVisible = false;
    public static MenuManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

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

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
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

    public void RestartCurrentLevel()
    {
        Time.timeScale = 1f;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
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
        
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
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
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }


    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }


}
