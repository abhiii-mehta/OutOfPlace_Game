using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int maxBullets = 20;
    public float shootCooldown = 1f;
    private float shootTimer = 0f;

    private int currentBullets;
    private GameObject activeBullet;

    void Start()
    {
        currentBullets = maxBullets;
    }

    void Update()
    {
        shootTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && currentBullets > 0 && activeBullet == null && shootTimer >= shootCooldown)
        {
            Shoot();
        }
    }


    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Debug.DrawLine(transform.position, transform.position + (Vector3)direction * 2f, Color.red, 1f);

        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 10f;

        activeBullet = bullet;
        currentBullets--;
        shootTimer = 0f;

    }

    public void ClearBullet()
    {
        activeBullet = null;
    }
}
