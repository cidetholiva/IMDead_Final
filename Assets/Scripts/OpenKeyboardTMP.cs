using TMPro;
using UnityEngine;

public class OpenKeyboardTMP : MonoBehaviour
{
    public GameObject keyboardObject;
    public TMP_InputField inputField;

    public void ShowKeyboard()
    {
        Debug.Log("ShowKeyboard() called");

        if (keyboardObject != null)
        {
            keyboardObject.SetActive(true);
            Debug.Log("Keyboard set active");
        }
        else
        {
            Debug.LogWarning("Keyboard object is null");
        }

        if (inputField != null)
        {
            inputField.Select();
            inputField.ActivateInputField();
            Debug.Log("Input field selected and activated");
        }
        else
        {
            Debug.LogWarning("Input field is null");
        }
    }
}
