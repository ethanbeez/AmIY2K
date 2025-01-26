using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class SendEmail : MonoBehaviour
{
    private WriteEmail writeEmailScript; // Reference to the writeEmail script
    private SoulManager soulManager;    // Reference to the SoulManager script
    private int emailCount = 0;         // Tracks the number of emails sent
    private int totalEmailsSent = 0; // Tracks the total number of emails ever sent (added by Ethan 1/25)
    [SerializeField] private AudioManager audioManager; // Added by Ethan 1/25
    [Header("UI Elements")]
    public TextMeshProUGUI countTextBox; // The text box where the number of sent emails is written.

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
        totalEmailsSent++;
        UpdateEmailCountDisplay();
        Debug.Log($"You sent {emailCount} email(s).");
        countTextBox.text = $"{emailCount} Emails Sent";
        // Call the effectiveness function
        Effectiveness();
        audioManager.PlaySoundClip("SendEmail");
    }

    private void ResetEmailCount()
    {
        // Reset the email count and log the reset
        emailCount = 0;
        Debug.Log("Email count has been reset to 0.");
        UpdateEmailCountDisplay();
    }

    private void UpdateEmailCountDisplay()
    {
        // Update the TMPro text box with the current email count
        if (countTextBox != null)
        {
            countTextBox.text = $"{emailCount} Email(s) Sent";
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

    public int GetTotalEmailsSent() {
        return totalEmailsSent;
    }

    public void ResetState() 
    {
        emailCount = 0;
        totalEmailsSent = 0;
        countTextBox.text = "0 Emails Sent";
    }
}
