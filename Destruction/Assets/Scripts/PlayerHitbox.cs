using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<PropBehavior>(out var prop))
        {
            if (!prop.isFake)
            {
                Debug.Log($"Player touched by real prop: {prop.name}");
                GameManager.instance.OnRealPropReachedPlayer();
            }
        }
    }
}
