using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    public static bool lightsOff = false;

    void Start()
    {
        StartCoroutine(FlickerLoop());
    }

    IEnumerator FlickerLoop()
    {
        while (true)
        {
            lightsOff = false;
            yield return new WaitForSeconds(Random.Range(1f, 2f));

            lightsOff = true;
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }
}
