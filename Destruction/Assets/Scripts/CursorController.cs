using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public Vector2 hotspot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(crosshairTexture, hotspot, CursorMode.Auto);
    }
}
