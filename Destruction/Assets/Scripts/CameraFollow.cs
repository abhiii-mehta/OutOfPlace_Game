using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public bool followX = true;
    public Vector3 offset = new Vector3(0, 0, -10f); 
    public bool followY = true;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPos = transform.position;

            if (followX) newPos.x = target.position.x;
            if (followY) newPos.y = target.position.y;

            newPos.z = offset.z;

            transform.position = newPos;
        }
    }
}
