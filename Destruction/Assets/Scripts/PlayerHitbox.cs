using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PropBehavior prop = other.GetComponent<PropBehavior>();
        if (prop != null && !prop.isFake)
        {
            GameManager.instance?.OnRealPropReachedPlayer();
        }
    }
}
