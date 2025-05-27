using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class FlashLight : MonoBehaviour
{
    private PolygonCollider2D poly;

    void Awake()
    {
        poly = GetComponent<PolygonCollider2D>();
    }

    void OnDrawGizmosSelected()
    {
        if (poly == null) return;

        Gizmos.color = Color.yellow;
        Vector2 offset = (Vector2)transform.position + poly.offset;

        Vector2[] points = poly.points;
        for (int i = 0; i < points.Length; i++)
        {
            Vector2 p1 = transform.TransformPoint(points[i]);
            Vector2 p2 = transform.TransformPoint(points[(i + 1) % points.Length]);
            Gizmos.DrawLine(p1, p2);
        }
    }
}
