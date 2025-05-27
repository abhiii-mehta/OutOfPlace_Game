using UnityEngine;
using System.Collections;

public class DelayedAudio : MonoBehaviour
{
    public void PlayAfterDelay(float delay)
    {
        StartCoroutine(PlayDelayed(delay));
    }

    IEnumerator PlayDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<AudioSource>()?.Play();
    }
}
