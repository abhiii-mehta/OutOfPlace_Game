using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public List<PropBehavior> propsInRoom = new List<PropBehavior>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var prop in propsInRoom)
                prop.SetPlayerInside(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var prop in propsInRoom)
                prop.SetPlayerInside(false);
        }
    }
}
