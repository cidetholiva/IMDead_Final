using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Required for coroutines

public class GameManager : MonoBehaviour
{
    public CornerTrigger[] corners; // Assign all 4 corner triggers in the inspector
    public string nextSceneName; // Name of the scene to load when all objects are correctly placed
    public AudioClip successSound; // Assign the sound clip in the Inspector
    private AudioSource audioSource;

    private void Start()
    {
        // Ensure an AudioSource component is attached to the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on GameManager. Please add one.");
        }
    }

    private void Update()
    {
        if (AreAllCornersCorrect())
        {
            Debug.Log("All objects are correctly placed! Resetting position and loading the next scene...");
            
            // Play the success sound
            if (audioSource != null && successSound != null)
            {
                audioSource.PlayOneShot(successSound);
            }

            ResetPosition();
            LoadNextScene();
        }
    }

    private bool AreAllCornersCorrect()
    {
        foreach (var corner in corners)
        {
            if (!corner.IsCorrectObjectPlaced)
            {
                return false;
            }
        }
        return true;
    }

    private void ResetPosition()
    {
        // Reset the position of the GameManager object or any other object
        transform.position = Vector3.zero; // Resets to (0, 0, 0)
        transform.rotation = Quaternion.identity; // Resets rotation to default
    }

    private void LoadNextScene()
    {
        StartCoroutine(LoadNextSceneCoroutine());
    }

    private IEnumerator LoadNextSceneCoroutine()
    {
        yield return new WaitForSeconds(5); // Wait for 5 seconds
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set. Please assign a valid scene name in the Inspector.");
        }
    }
}