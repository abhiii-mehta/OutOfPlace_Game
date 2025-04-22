using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class LevelExit : MonoBehaviour
{
    private bool isUnlocked = false;
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUnlocked) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached exit!");
            GameManager.instance?.StartCoroutine(FadeAndLoadNextLevel(nextSceneName));
        }
    }

    public void UnlockExit()
    {
        isUnlocked = true;
        Debug.Log("Exit unlocked!");
    }

    IEnumerator FadeAndLoadNextLevel(string sceneName)
    {
        if (FadeController.instance != null)
            yield return FadeController.instance.FadeOut();

        SceneManager.LoadScene(sceneName);
    }
}
