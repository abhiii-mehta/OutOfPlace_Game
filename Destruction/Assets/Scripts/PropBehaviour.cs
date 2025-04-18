using UnityEngine;

public class PropBehavior : MonoBehaviour
{
    public bool isFake = false;
    public float moveAmount = 0.5f;
    private Transform defenseLine;

    void Start()
    {
        defenseLine = GameObject.FindGameObjectWithTag("DefenseLine")?.transform;
    }

    public void Reactivate()
    {
        int direction = Random.Range(0, isFake ? 2 : 3);

        Vector3 moveDir = Vector3.zero;

        switch (direction)
        {
            case 0: moveDir = Vector3.left; break;
            case 1: moveDir = Vector3.right; break;
            case 2:
                if (!isFake) moveDir = Vector3.down;
                break;
        }

        transform.position += moveDir * moveAmount;
    }
}
