using UnityEngine;

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
        Debug.Log(" Real prop destroyed. Remaining: " + totalRealProps);

        if (totalRealProps <= 0)
        {
            Debug.Log(" All real props eliminated — YOU WIN!");
            gameOver = true;

            if (youWinPanel != null)
                youWinPanel.SetActive(true);

            Time.timeScale = 0f;
        }
    }

}
