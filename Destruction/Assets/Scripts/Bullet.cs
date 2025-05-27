using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f;
    public AudioClip shootSound;
    public AudioClip shellDropSound;
    [Range(0f, 1f)] public float audioVolume = 1f;
    void Start()
    {
        Destroy(gameObject, lifetime);

        if (shootSound != null)
            PlayDetachedAudio(shootSound, 0f);

        if (shellDropSound != null)
            PlayDetachedAudio(shellDropSound, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Prop"))
        {
            PropBehavior prop = other.GetComponent<PropBehavior>();
            if (prop != null)
            {
                prop.DestroyWithSpriteSwap();

                if (!prop.isFake)
                {
                    GameManager.instance?.OnRealPropDestroyed();
                }
            }

            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void PlayDetachedAudio(AudioClip clip, float delay)
    {
        if (clip == null) return;

        GameObject audioObj = new GameObject("TempAudio_" + clip.name);
        audioObj.transform.position = transform.position; // make sure it's at bullet's position

        AudioSource source = audioObj.AddComponent<AudioSource>();
        source.clip = clip;
        source.playOnAwake = false;
        source.volume = Mathf.Clamp01(audioVolume); // force volume to 0–1 range
        source.spatialBlend = 0f; // make sure it's 2D audio

        if (delay > 0f)
            source.PlayDelayed(delay);
        else
            source.Play();

        Destroy(audioObj, clip.length + delay + 0.1f);
    }

}
