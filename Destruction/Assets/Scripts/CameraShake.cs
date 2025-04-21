using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10f);
    public bool followX = true;
    public bool followY = true;

    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.4f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 newPos = transform.position;

        if (followX) newPos.x = target.position.x;
        if (followY) newPos.y = target.position.y;
        newPos.z = offset.z;

        if (shakeDuration > 0)
        {
            Vector2 shakeOffset = Random.insideUnitCircle * shakeMagnitude;
            newPos += new Vector3(shakeOffset.x, shakeOffset.y, 0);
            shakeDuration -= Time.unscaledDeltaTime;
        }

        transform.position = newPos;
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
