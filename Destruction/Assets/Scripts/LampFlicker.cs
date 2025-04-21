using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightFlicker : MonoBehaviour
{
    public Light2D globalLight;
    public float flickerInterval = 3f;
    public float flickerDuration = 0.2f;
    public float dimIntensity = 0.2f;

    private float originalIntensity;

    void Start()
    {
        if (globalLight == null)
            globalLight = GetComponent<Light2D>();

        originalIntensity = globalLight.intensity;
        InvokeRepeating(nameof(Flicker), flickerInterval, flickerInterval + Random.Range(0f, 1f));
    }

    void Flicker()
    {
        StartCoroutine(FlickerRoutine());
    }

    System.Collections.IEnumerator FlickerRoutine()
    {
        globalLight.intensity = dimIntensity;
        yield return new WaitForSeconds(flickerDuration);
        globalLight.intensity = originalIntensity;
    }
}
