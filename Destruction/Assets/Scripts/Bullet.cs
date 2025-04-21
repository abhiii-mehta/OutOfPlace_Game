using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Prop"))
        {
            PropBehavior prop = other.GetComponent<PropBehavior>();
            if (prop != null)
            {
                prop.DestroyWithSpriteSwap();

                if (!prop.isFake)
                {
                    GameManager.instance?.OnRealPropDestroyed();
                }
            }

            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
