using UnityEngine;

public class PropBehavior : MonoBehaviour
{
    public bool isFake = false;
    public float moveSpeed = 1f;
    public float aggroRange = 5f;
    public Collider2D roomBoundary;
    public Sprite destroyedSprite;

    private SpriteRenderer sr;
    private Transform player;
    private bool playerInRoom = false;
    private bool isLit = false;
    private Transform flashlightTransform;
    private bool hasDodged = false;
    private float dodgeCooldown = 2f;
    private float dodgeTimer = 0f;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
        {
            flashlightTransform = player.Find("Flashlight");
        }

        if (isFake)
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<Collider2D>().isTrigger = false;
            GameManager.instance?.RegisterRealProp();
        }
    }

    void Update()
    {
        if (isFake || GameManager.instance == null || player == null || !playerInRoom || isLit)
            return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > aggroRange) return;

        Vector2 toProp = (transform.position - player.position).normalized;
        Vector2 direction;

        // Smart flanking if directly in flashlight cone
        if (flashlightTransform != null)
        {
            Vector2 flashlightDir = flashlightTransform.right;
            float dot = Vector2.Dot(toProp, flashlightDir);

            if (dot > 0.7f)
            {
                Vector2 flankDir = Vector2.Perpendicular(flashlightDir) * (Random.value > 0.5f ? 1 : -1);
                direction = (flankDir + (Vector2)(player.position - transform.position)).normalized;
            }
            else
            {
                direction = (player.position - transform.position).normalized;
            }
        }
        else
        {
            direction = (player.position - transform.position).normalized;
        }

        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        if (distance < 2f)
        {
            CameraShake.instance?.Shake(0.15f, 0.05f);
        }

        if (hasDodged)
        {
            dodgeTimer += Time.deltaTime;
            if (dodgeTimer >= dodgeCooldown)
            {
                hasDodged = false;
                dodgeTimer = 0f;
            }
        }
    }

    public void SetLit(bool value)
    {
        if (!isFake && value && !hasDodged && Random.value < 0.5f)
        {
            // Perform dodge
            Vector2 dodgeDir = Vector2.Perpendicular((transform.position - player.position).normalized) * (Random.value > 0.5f ? 1 : -1);
            transform.position += (Vector3)(dodgeDir * 0.5f);
            hasDodged = true;
            Debug.Log(name + " dodged the light!");
        }

        isLit = value;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isFake ? Color.yellow : Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Flashlight"))
        {
            SetLit(true);
            Debug.Log($"{name} entered flashlight");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Flashlight"))
        {
            SetLit(false);
            Debug.Log($"{name} exited flashlight");
        }
    }
}
