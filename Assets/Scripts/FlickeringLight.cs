using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light spotlight; // Assigns your spotlight in the inspector
    public float minDelay = 0.05f;
    public float maxDelay = 0.3f;

    private void Start()
    {
        if (spotlight == null)
            spotlight = GetComponent<Light>();

        StartCoroutine(Flicker());
    }

    private System.Collections.IEnumerator Flicker()
    {
        while (true)
        {
            spotlight.enabled = !spotlight.enabled;
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
        }
    }
}
