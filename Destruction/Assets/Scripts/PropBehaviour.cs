using UnityEngine;

public class PropBehavior : MonoBehaviour
{
    public bool isFake = false;
    public float moveSpeed = 1f;

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning($"{name}: Player not found in scene.");
        }
    }

    void Update()
    {
        if (!isFake && LightController.lightsOff && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
            Debug.DrawLine(transform.position, player.position, Color.green);
        }
    }
}
