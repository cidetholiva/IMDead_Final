using UnityEngine;

public class TriggerDestroy : MonoBehaviour
{
    // Called when another collider exits the trigger collider attached to this GameObject
    private void OnTriggerExit(Collider other)
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}
