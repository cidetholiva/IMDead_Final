using UnityEngine;

public class TriggerPasswordGame : MonoBehaviour
{
    [Header("Mini-Game UI")]
    public GameObject passwordGameCanvas;

    [Header("Audio")]
    public AudioSource pickupSound;

    private bool hasStarted = false;

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Entered trigger by: " + other.name);

        if (other.CompareTag("PlayerHand")) //checks if player has picked up the object with the tag "Playerhand" -aka the vr hands.
        {
            Debug.Log("Correct tag detected.");

            if (!hasStarted) //makes sure game starts
            {
                StartMiniGame();
            }
            else
            {
                Debug.Log("Mini-game already started.");
            }
        }
        else
        {
            Debug.Log("Incorrect tag: " + other.tag); //logs if something is wrong
        }
    }

    private void StartMiniGame()
    {
        if (passwordGameCanvas != null) //shows game
        {
            passwordGameCanvas.SetActive(true);
            Debug.Log("Mini-game canvas activated.");
        }
        else
        {
            Debug.LogWarning("Password game canvas is not assigned!");
        }

        if (pickupSound != null)
        {
            pickupSound.Play();
            Debug.Log("Pickup sound played.");
        }
        else
        {
            Debug.LogWarning("Pickup sound is not assigned!");
        }

        hasStarted = true;
    }
}
