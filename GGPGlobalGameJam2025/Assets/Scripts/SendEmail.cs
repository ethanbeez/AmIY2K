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

        // Add a listener to the new button's onClick event to reset emailCount
        if (writeEmailScript != null && writeEmailScript.newButton != null)
        {
            writeEmailScript.newButton.onClick.AddListener(ResetEmailCount);
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

    private void ResetEmailCount()
    {
        // Reset the email count and log the reset
        emailCount = 0;
        Debug.Log("Email count has been reset to 0.");
    }
}
