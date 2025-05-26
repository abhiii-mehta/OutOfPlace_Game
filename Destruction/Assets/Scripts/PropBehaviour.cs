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
    private bool isLit = false;
    private bool initialized = false;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
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

        Invoke(nameof(EnableMovement), 0.1f); // small delay
    }

    void EnableMovement()
    {
        initialized = true;
    }

    void Update()
    {
        if (!initialized || isFake || GameManager.instance == null || player == null || !playerInRoom || isLit)
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
            Destroy(gameObject, 0.3f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Flashlight"))
        {
            isLit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Flashlight"))
        {
            isLit = false;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isFake ? Color.yellow : Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
