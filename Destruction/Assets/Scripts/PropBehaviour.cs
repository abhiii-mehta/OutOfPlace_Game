using UnityEngine;

public class PropBehavior : MonoBehaviour
{
    public bool isFake = false;
    public float moveSpeed = 1f;
    public Collider2D roomBoundary;
    public float aggroRange = 5f;

    public Sprite destroyedSprite;
    private SpriteRenderer sr;
    private Transform player;
    private bool playerInRoom = false;
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
        if (isFake || GameManager.instance == null || player == null || !LightController.lightsOff || !playerInRoom)
            return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= aggroRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

            if (distance < 2f)
            {
                CameraShake.instance?.Shake(0.15f, 0.05f);
            }
        }
    }
    public void SetPlayerInside(bool inside)
    {
        playerInRoom = inside;
    }
    public void DestroyWithSpriteSwap()
    {
        if (destroyedSprite != null)
        {
            sr.sprite = destroyedSprite;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 1.5f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isFake ? Color.yellow : Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
