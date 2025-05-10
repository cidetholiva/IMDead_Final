using UnityEngine;

public class CornerTrigger : MonoBehaviour
{
    public string requiredTag; // The tag of the object required in this corner
    private bool isCorrectObjectPlaced = false;

    public bool IsCorrectObjectPlaced => isCorrectObjectPlaced;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            isCorrectObjectPlaced = true;
            Debug.Log($"Correct object placed in {gameObject.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            isCorrectObjectPlaced = false;
            Debug.Log($"Object removed from {gameObject.name}");
        }
    }
}