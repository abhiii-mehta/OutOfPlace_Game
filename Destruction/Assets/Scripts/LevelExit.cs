using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private bool isUnlocked = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUnlocked) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log(" You Win! Next level unlocked.");
        }
    }

    public void UnlockExit()
    {
        isUnlocked = true;
        Debug.Log(" Exit unlocked!");
    }
}
