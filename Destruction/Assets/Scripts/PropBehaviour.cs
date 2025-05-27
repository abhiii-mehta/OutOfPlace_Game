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

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

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

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        if (distance < 2f)
        {
            CameraShake.instance?.Shake(0.15f, 0.05f);
        }
    }

    public void SetLit(bool value)
    {
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
            isLit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Flashlight"))
        {
            isLit = false;
        }
    }

}
