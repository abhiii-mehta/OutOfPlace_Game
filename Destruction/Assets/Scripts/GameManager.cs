using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalRealProps = 0;
    public bool gameOver = false;

    [Header("Endgame UI")]
    public GameObject youWinPanel;
    public GameObject gameOverPanel;

    [Header("Level Exit")]
    public LevelExit exit;

    [Header("UI")]
    public TextMeshProUGUI propCounterText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterRealProp()
    {
        totalRealProps++;
        Debug.Log("Registered Real Prop. Total = " + totalRealProps);
        UpdatePropUI();
    }


    public void OnRealPropReachedPlayer()
    {
        if (gameOver) return;

        Debug.Log(" A real prop touched the player — YOU LOSE!");
        gameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void OnRealPropDestroyed()
    {
        if (gameOver) return;

        totalRealProps--;
        Debug.Log("Real prop destroyed. Remaining: " + totalRealProps);
        UpdatePropUI();

        if (totalRealProps <= 0)
        {
            gameOver = true;
            if (exit != null) exit.UnlockExit();

            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "Level03")
            {
                Debug.Log("Final level complete — YOU WIN!");
                if (youWinPanel != null)
                    youWinPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Debug.Log("Level cleared. Find the stairs to go to the next floor...");
                StartCoroutine(ShowLevelCompleteMessage());
            }
        }
    }

    public TextMeshProUGUI levelMessageText;

    IEnumerator ShowLevelCompleteMessage()
    {
        if (levelMessageText != null)
        {
            levelMessageText.text = "Find the stairs to go to the next floor...";
            levelMessageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            levelMessageText.gameObject.SetActive(false);
        }
    }

    void UpdatePropUI()
    {
        if (propCounterText != null)
        {
            propCounterText.text = "Props Left: " + totalRealProps;
        }
    }
}
