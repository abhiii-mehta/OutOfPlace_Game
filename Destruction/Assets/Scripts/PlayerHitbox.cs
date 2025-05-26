using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Only care about real props, not flashlight
        if (!other.CompareTag("Prop")) return;

        PropBehavior prop = other.GetComponent<PropBehavior>();
        if (prop != null && !prop.isFake)
        {
            Debug.Log($"Player touched by real prop: {prop.name}");
            GameManager.instance?.OnRealPropReachedPlayer();
        }
    }

}
