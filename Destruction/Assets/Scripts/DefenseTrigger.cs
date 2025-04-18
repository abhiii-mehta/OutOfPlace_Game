using UnityEngine;

public class DefenseTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something touched the defense line: " + other.name);

        PropBehavior prop = other.GetComponent<PropBehavior>();
        if (prop != null && !prop.isFake)
        {
            Debug.Log(" Real prop triggered loss: " + prop.name);
            GameManager.instance?.OnRealPropReachedDefense();
        }
    }

}
