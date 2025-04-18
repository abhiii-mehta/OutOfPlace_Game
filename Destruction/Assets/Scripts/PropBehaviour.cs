using UnityEngine;

public class PropBehavior : MonoBehaviour
{
    public bool isFake = false;
    public float moveSpeed = 1f;
    private Transform defenseLine;

    void Start()
    {
        defenseLine = GameObject.FindGameObjectWithTag("DefenseLine")?.transform;
    }

    void Update()
    {
        if (isFake || !LightController.lightsOff) return;

        if (defenseLine != null)
        {
            Vector3 direction = (defenseLine.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
