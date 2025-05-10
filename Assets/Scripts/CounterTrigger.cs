using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class CounterTrigger : MonoBehaviour
{
    private int counter = 0;

    [Header("Scene Management")]
    public string nextSceneName = "NextScene";

    [Header("Reset Position")]
    public Vector3 resetPosition;

    [Header("Player Position Check")]
    public Transform player;
    public BoxCollider playerArea;

    [Header("UI Elements")]
    public TextMeshProUGUI counterText;
    public TextMeshProUGUI statusText;
    public Image canvasBackground;

    [Header("Audio")]
    public AudioSource warningAudioSource;

    [Header("Basket Target")]
    public Transform wasteBin;


    private Coroutine fadeCoroutine;
    private Coroutine flickerCoroutine;
    private Coroutine fadeBackgroundCoroutine;

    private void Start()
    {
        UpdateCounterUI();

        SetCanvasBackgroundColor(Color.black);

        // Shows intro and fade after 3 seconds
        UpdateStatusUI("Shoot for your life....");
        fadeBackgroundCoroutine = StartCoroutine(FadeOutCanvasBackground(3f));
    }

    private void OnTriggerEnter(Collider other)
    {
        TargetObject target = other.GetComponent<TargetObject>();
        if (target != null)
        {
            Debug.Log("Headset position: " + player.position);
            Debug.Log("Shooting area bounds: " + playerArea.bounds);
            Debug.Log("Player inside area? " + playerArea.bounds.Contains(player.position));

            float tooCloseDistance = 3f; //can adjust
            float distanceToBin = Vector3.Distance(player.position, wasteBin.position);
            bool playerTooClose = distanceToBin < tooCloseDistance;

            Debug.Log("Distance to bin: " + distanceToBin);
            
            if (!playerTooClose)
            {
                counter++;
                UpdateCounterUI();
                UpdateStatusUI("You're in the shooting area. Shot made!");
                
                Destroy(other.gameObject);
                
                if (counter >= 5)
                {
                    UpdateStatusUI("5 shots made! Loading next scene...");
                    SceneManager.LoadScene(nextSceneName);
                }
            }
            else
            {
                UpdateStatusUI("YOU'RE NOT IN THE SHOOTING AREA. Basketball location reset");
                if (flickerCoroutine != null) StopCoroutine(flickerCoroutine);
                flickerCoroutine = StartCoroutine(FlickerCanvasBackground(Color.black, 3f));
                
                if (warningAudioSource != null && !warningAudioSource.isPlaying)
                    warningAudioSource.Play();
                    
                other.transform.position = resetPosition;
            }

        }
    }

    private void SetCanvasBackgroundColor(Color color)
    {
        if (canvasBackground != null)
            canvasBackground.color = color;
    }

    private IEnumerator FlickerCanvasBackground(Color flickerColor, float duration)
    {
        float elapsed = 0f;
        bool flickerOn = true;
        float flickerInterval = 0.5f;

        while (elapsed < duration)
        {
            elapsed += flickerInterval;

            canvasBackground.color = flickerOn
                ? flickerColor
                : new Color(flickerColor.r, flickerColor.g, flickerColor.b, 0f);

            flickerOn = !flickerOn;
            yield return new WaitForSeconds(flickerInterval);
        }

        // Resets to fully transparent after flicker ends
        canvasBackground.color = new Color(flickerColor.r, flickerColor.g, flickerColor.b, 0f);
    }

    private IEnumerator FadeOutCanvasBackground(float delay)
    {
        yield return new WaitForSeconds(delay);

        float fadeDuration = 1.5f;
        float elapsed = 0f;
        Color originalColor = canvasBackground.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            canvasBackground.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        canvasBackground.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private void UpdateCounterUI()
    {
        if (counterText != null)
            counterText.text = $"Shots Made: {counter}/5";
    }

    private void UpdateStatusUI(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
            statusText.alpha = 1f;

            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeOutStatusText(3f)); // Always fade out after 3 seconds
        }
    }

    private IEnumerator FadeOutStatusText(float delay)
    {
        yield return new WaitForSeconds(delay);

        float fadeDuration = 1.5f;
        float elapsed = 0f;
        Color originalColor = statusText.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            statusText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        statusText.text = "";
    }
}