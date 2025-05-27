using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PropBehavior : MonoBehaviour
{
    public bool isFake = false;
    public float moveSpeed = 1f;
    public float aggroRange = 5f;
    public Collider2D roomBoundary;
    public Sprite destroyedSprite;

    public GameObject eyes;
    public Light2D leftEyeLight;
    public Light2D rightEyeLight;

    private SpriteRenderer sr;
    private Transform player;
    private Transform flashlightTransform;
    private bool playerInRoom = false;
    private bool isLit = false;

    private float eyeTimer = 0f;
    private float eyeCooldown = 0f;
    private bool eyeVisible = false;

    private bool dashing = false;
    private float dashCooldown = 0f;
    private Vector2 dashDirection;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player != null)
        {
            flashlightTransform = player.Find("Flashlight");
        }

        eyes = transform.Find("eye")?.gameObject;
        if (eyes != null)
        {
            eyes.SetActive(false);
            if (leftEyeLight != null) leftEyeLight.enabled = false;
            if (rightEyeLight != null) rightEyeLight.enabled = false;
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
        if (isFake || GameManager.instance == null || player == null || !playerInRoom)
            return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > aggroRange) return;

        HandleEyes();
        HandleDash();
        HandleMovement(distance);
    }

    void HandleEyes()
    {
        if (eyes == null) return;

        if (!isLit && eyeCooldown <= 0f && !eyeVisible && Random.value < 0.005f)
        {
            SetEyes(true);
            eyeTimer = 0.5f;
        }

        if (eyeVisible)
        {
            eyeTimer -= Time.deltaTime;
            if (eyeTimer <= 0f || isLit)
            {
                SetEyes(false);
                eyeCooldown = 2f;
            }
        }
        else
        {
            eyeCooldown -= Time.deltaTime;
        }
    }

    void SetEyes(bool state)
    {
        eyes.SetActive(state);
        if (leftEyeLight != null) leftEyeLight.enabled = state;
        if (rightEyeLight != null) rightEyeLight.enabled = state;
        eyeVisible = state;
    }

    void HandleDash()
    {
        if (isLit && !dashing && dashCooldown <= 0f)
        {
            dashDirection = (Vector2)(transform.position - flashlightTransform.position).normalized;
            dashing = true;
            dashCooldown = 2f;
        }

        if (dashing)
        {
            transform.position += (Vector3)(dashDirection * moveSpeed * 3f * Time.deltaTime);
            dashing = false;
        }
        else
        {
            dashCooldown -= Time.deltaTime;
        }
    }

    void HandleMovement(float distance)
    {
        if (!isLit && !dashing)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

            if (distance < 2f)
            {
                CameraShake.instance?.Shake(0.15f, 0.05f);
            }
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
