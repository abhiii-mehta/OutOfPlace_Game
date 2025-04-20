using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalRealProps = 0;
    public bool gameOver = false;

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
    }

    public void OnRealPropDestroyed()
    {
        if (gameOver) return;

        totalRealProps--;
        Debug.Log(" Real prop destroyed. Remaining: " + totalRealProps);

        if (totalRealProps <= 0)
        {
            Debug.Log("All real props eliminated — YOU WIN!");
            gameOver = true;
        }
    }
}
