using UnityEngine;

public class PropBehavior : MonoBehaviour
{
    public bool isFake = false;
    public float moveSpeed = 1f;

    private Transform player;
    public Sprite destroyedSprite;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning($"{name}: Player not found in scene.");
        }
        if (!isFake && GameManager.instance != null)
        {
            GameManager.instance.RegisterRealProp();
        }
    }

    void Update()
    {
        if (!isFake && LightController.lightsOff && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            Debug.DrawLine(transform.position, player.position, Color.green);
            float dist = Vector2.Distance(transform.position, player.position);
            if (dist < 2f) // adjust sensitivity
            {
                CameraShake.instance?.Shake(0.15f, 0.05f);
            }
        }
    }

    public void DestroyWithSpriteSwap()
    {
        if (destroyedSprite != null)
        {
            sr.sprite = destroyedSprite;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 1.5f); // Let player see the broken version for a moment
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
