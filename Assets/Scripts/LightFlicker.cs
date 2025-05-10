using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
    public Light lightToFlicker; // Assign the Light component in the Inspector
    [Tooltip("Minimum time between flickers (in seconds)")]
    public float minFlickerInterval = 0.1f;
    [Tooltip("Maximum time between flickers (in seconds)")]
    public float maxFlickerInterval = 0.5f;
    [Tooltip("Minimum intensity the light can flicker to")]
    public float minIntensity = 0f;
    [Tooltip("Maximum intensity the light can flicker to")]
    public float maxIntensity = 1f;

    private float originalIntensity;
    private Coroutine flickerCoroutine;

    void Start()
    {
        // Ensure a Light component is assigned
        if (lightToFlicker == null)
        {
            Debug.LogError("LightFlicker script needs a Light component assigned to the 'lightToFlicker' variable!");
            enabled = false; // Disable the script if no light is assigned
            return;
        }

        originalIntensity = lightToFlicker.intensity;
        StartFlickering();
    }

    public void StartFlickering()
    {
        if (flickerCoroutine == null)
        {
            flickerCoroutine = StartCoroutine(Flicker());
        }
    }

    public void StopFlickering()
    {
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            lightToFlicker.intensity = originalIntensity; // Restore original intensity
            flickerCoroutine = null;
        }
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            // Turn the light off
            lightToFlicker.intensity = 0f;
            yield return new WaitForSeconds(Random.Range(minFlickerInterval, maxFlickerInterval));

            // Turn the light back on with a random intensity (optional)
            lightToFlicker.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(Random.Range(minFlickerInterval, maxFlickerInterval));
        }
    }
}