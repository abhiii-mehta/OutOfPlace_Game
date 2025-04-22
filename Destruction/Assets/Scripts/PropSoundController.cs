using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PropSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    private Vector3 lastPosition;
    private bool isMoving;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    void Update()
    {
        isMoving = Vector3.Distance(transform.position, lastPosition) > 0.01f;

        if (isMoving)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }

        lastPosition = transform.position;
    }
}
