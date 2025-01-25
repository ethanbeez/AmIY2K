using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class SendEmail : MonoBehaviour
{
    private WriteEmail writeEmailScript; // Reference to the writeEmail script
    private SoulManager soulManager;    // Reference to the SoulManager script
    private int emailCount = 0;         // Tracks the number of emails sent
    public TextMeshProUGUI emailCountText;

    void Start()
    {
        // Find the WriteEmail script in the scene
        writeEmailScript = FindObjectOfType<WriteEmail>();

        // Find the SoulManager script in the scene
        soulManager = FindObjectOfType<SoulManager>();

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
    
        UpdateEmailCountDisplay();
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
        // Increment the email count
        emailCount++;
        UpdateEmailCountDisplay();
        Debug.Log($"You sent {emailCount} email(s).");

        // Call the effectiveness function
        Effectiveness();
    }

    private void ResetEmailCount()
    {
        // Reset the email count and log the reset
        emailCount = 0;
        UpdateEmailCountDisplay();
    }

    private void UpdateEmailCountDisplay()
    {
        // Update the TMPro text box with the current email count
        if (emailCountText != null)
        {
            emailCountText.text = $"{emailCount} Email(s) Sent";
        }
        else
        {
            Debug.LogWarning("EmailCountText reference is missing!");
        }
    }

    private void Effectiveness()
    {
        if (soulManager == null)
        {
            Debug.LogError("SoulManager not found in the scene!");
            return;
        }

        // Call HandleEmailSent on SoulManager to handle the captured souls
        soulManager.HandleEmailSent(emailCount);

        // Log soul capture for debugging
        Debug.Log($"Effectiveness calculated for email count {emailCount}.");
    }
}
