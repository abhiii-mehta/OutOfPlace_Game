using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Elements")]
    public TextMeshProUGUI levelMessageText;
    public TextMeshProUGUI propCounterText;

    [Header("Gameplay")]
    public int totalRealProps = 0;
    public bool gameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (levelMessageText == null)
        {
            GameObject messageObj = GameObject.Find("LevelExitText");
            if (messageObj != null)
            {
                levelMessageText = messageObj.GetComponent<TextMeshProUGUI>();
            }
        }
    }

    public void RegisterRealProp()
    {
        totalRealProps++;
        UpdatePropUI();
    }

    public void OnRealPropDestroyed()
    {
        if (gameOver) return;

        totalRealProps--;
        UpdatePropUI();

        if (totalRealProps <= 0)
        {
            gameOver = true;
            LevelCompleted();
        }
    }

    void LevelCompleted()
    {
        if (SceneManager.GetActiveScene().name == "Level03")
        {
            global::MenuManager.instance.ShowWinPanel();
        }
        else
        {
            ShowLevelMessage("All real props destroyed. Find the stairs.");
        }
    }

    public void ShowLevelMessage(string msg)
    {
        if (levelMessageText != null)
        {
            levelMessageText.text = msg;
            levelMessageText.gameObject.SetActive(true);
        }
    }

    public void OnRealPropReachedPlayer()
    {
        if (gameOver) return;

        Debug.Log(" A real prop touched the player — YOU LOSE!");
        gameOver = true;
        MenuManager.instance.ShowLosePanel();
    }

    public void UpdatePropUI()
    {
        if (propCounterText != null)
        {
            propCounterText.text = "Props Left: " + totalRealProps;
        }
    }

}
