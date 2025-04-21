using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int maxBullets = 20;
    public float shootCooldown = 0.5f;
    public TextMeshProUGUI bulletCounterText;

    private int currentBullets;
    private float shootTimer = 0f;
    private GameObject activeBullet;

    void Start()
    {
        currentBullets = maxBullets;
        UpdateBulletUI();
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && currentBullets > 0 && shootTimer >= shootCooldown)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 shootDirection = (mousePos - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = shootDirection * 10f;

        activeBullet = bullet;
        currentBullets--;
        shootTimer = 0f;

        UpdateBulletUI();
    }

    public void ClearBullet()
    {
        activeBullet = null;
    }

    void UpdateBulletUI()
    {
        if (bulletCounterText != null)
        {
            bulletCounterText.text = $"Bullets: {currentBullets}";
        }
    }
}
