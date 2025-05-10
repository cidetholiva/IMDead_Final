using UnityEngine;

public class HideCursor : MonoBehaviour
{
    [Tooltip("Hide the cursor on start?")]
    public bool hideCursorOnStart = true;

    void Start()
    {
        if (hideCursorOnStart)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; // Optional: Locks cursor to the center of the game window.
        }
    }

    void OnDisable()
    {
        // Show the cursor again when the script is disabled.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor.
    }

    void Reset()
    {
        hideCursorOnStart = true;
    }
}
