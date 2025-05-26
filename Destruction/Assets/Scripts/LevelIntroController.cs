using UnityEngine;
using TMPro;

public class LevelIntroController : MonoBehaviour
{
    public TextMeshProUGUI instructionText;
    public float displayTime = 4.5f;
    public UnityEngine.Rendering.Universal.Light2D globalLight;

    void Start()
    {
        instructionText.gameObject.SetActive(true);
        globalLight.intensity = 0.01f; // Keep dim
        Invoke(nameof(HideInstructions), displayTime);
    }

    void HideInstructions()
    {
        instructionText.gameObject.SetActive(false);
        // Do not reset the light to 1.0
        // If needed: globalLight.intensity = 0.3f;
    }
}
