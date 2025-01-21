using UnityEngine;
using UnityEngine.UI;

public class SendEmail : MonoBehaviour
{
    private WriteEmail writeEmailScript; // Reference to the writeEmail script
    private int emailCount = 0;       // Tracks the number of emails sent

    void Start()
    {
        // Find the WriteEmail script in the scene
        writeEmailScript = FindObjectOfType<WriteEmail>();

        // Add a listener to the button's onClick event
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnSubmitButtonPressed);
        }
    }

    void Update()
    {
        // Ensure the submit button is interactable based on the WriteEmail script
        if (writeEmailScript != null && GetComponent<Button>() != null)
        {
            GetComponent<Button>().interactable = writeEmailScript.submitButton.interactable;
        }
    }

    private void OnSubmitButtonPressed()
    {
        // Increment the email count and log to the console
        emailCount++;
        Debug.Log($"You sent {emailCount} email(s).");
    }
}
