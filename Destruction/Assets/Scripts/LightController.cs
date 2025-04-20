using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;
public class LightController : MonoBehaviour
{
    public static bool lightsOff = false;

    [Header("Light Settings")]
    public Light2D globalLight;
    public float lightOnIntensity = 1f;
    public float lightOffIntensity = 0.01f;

    [Header("Flicker Timings")]
    public float minOnTime = 1f;
    public float maxOnTime = 2f;
    public float minOffTime = 1f;
    public float maxOffTime = 2f;

    void Start()
    {
        StartCoroutine(FlickerLoop());
    }

    IEnumerator FlickerLoop()
    {
        while (true)
        {
            lightsOff = false;
            if (globalLight != null) globalLight.intensity = lightOnIntensity;

            yield return new WaitForSeconds(Random.Range(minOnTime, maxOnTime));

            lightsOff = true;
            if (globalLight != null) globalLight.intensity = lightOffIntensity;
            yield return new WaitForSeconds(Random.Range(minOffTime, maxOffTime));
        }
    }

}
