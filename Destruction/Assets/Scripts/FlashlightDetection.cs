using UnityEngine;

public class FlashlightDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var prop = other.GetComponent<PropBehavior>();
        if (prop != null && !prop.isFake)
            prop.SetLit(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var prop = other.GetComponent<PropBehavior>();
        if (prop != null && !prop.isFake)
            prop.SetLit(false);
    }
}
