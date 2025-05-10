using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public float timer = 300f; // Exposes to Inspector
    public Canvas gameOverCanvas; // Assigns the GameOver Canvas in the Inspector
    public Image screenOverlay; // Assigns an Image component for the red overlay in the Inspector
    private TextMeshProUGUI timerText;
    private bool isGameOver = false;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        if (timerText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on the GameObject.");
        }

        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(false); // Ensures the GameOver Canvas is initially inactive
        }

        if (screenOverlay != null)
        {
            screenOverlay.color = new Color(1, 0, 0, 0); // Ensures the overlay starts fully transparent
        }
    }

    void Update()
    {
        if (isGameOver) return;

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("f2");

            // Starts changing the screen red if 20 seconds or less remain
            if (timer <= 20 && screenOverlay != null)
            {
                float alpha = Mathf.Lerp(0, 0.5f, (20 - timer) / 20); // Gradually increase alpha to 0.5
                screenOverlay.color = new Color(1, 0, 0, alpha);
            }
        }
        else
        {
            StartGameOverSequence();
        }
    }

    private void StartGameOverSequence()
    {
        isGameOver = true;

        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(true); // Activates the GameOver Canvas
        }

        Invoke(nameof(RestartScene), 10f); // Waits 10 seconds before restarting the scene
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restarts the current scene
    }
}