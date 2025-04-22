using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelExit : MonoBehaviour
{
    [Tooltip("Name of the next scene to load")]
    public string nextSceneName;

    private bool canExit = false;

    private void Update()
    {
        if (!canExit && GameManager.instance != null && GameManager.instance.totalRealProps <= 0)
        {
            canExit = true;
            Debug.Log("Exit unlocked!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canExit) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached exit!");
            if (GameManager.instance != null)
                GameManager.instance.StartCoroutine(FadeAndLoadNextLevel(nextSceneName));
            else
                SceneManager.LoadScene(nextSceneName);
        }
    }

    IEnumerator FadeAndLoadNextLevel(string sceneName)
    {
        if (FadeController.instance != null)
            yield return FadeController.instance.FadeOut();

        SceneManager.LoadScene(sceneName);
    }
}
