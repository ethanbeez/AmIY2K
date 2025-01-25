using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class WriteEmail : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI emailTextBox; // The text box where the email is written.
    public Button submitButton;         // The submit button to send the email.
    public Button newButton;            // The button to clear and load a new email.

    [Header("Email Content")]
    [TextArea(5, 10)]
    public List<string> prewrittenEmails; // List of prewritten email contents.

    private string currentEmail; // Holds the currently selected email.
    private int currentLetterIndex = 0; // Tracks the current position in the email.

    void Start()
    {
        // Ensure the submit button is initially disabled.
        if (submitButton != null)
            submitButton.interactable = false;

        // Ensure the new button is properly set up.
        if (newButton != null)
            newButton.onClick.AddListener(LoadNewEmail);

        // Clear the email text box at the start and load a new email.
        if (emailTextBox != null)
            emailTextBox.text = "";

        LoadNewEmail();
    }

    void Update()
    {
        // Check if any key is pressed and there are letters left to display.
        if (Input.anyKeyDown && currentLetterIndex < currentEmail.Length)
        {
            RevealNextLetter();
        }
    }

    private void RevealNextLetter()
    {
        // Add the next letter to the text box.
        emailTextBox.text += currentEmail[currentLetterIndex];
        currentLetterIndex++;

        // Enable the submit button once the email is fully written.
        if (currentLetterIndex >= currentEmail.Length && submitButton != null)
        {
            submitButton.interactable = true;
        }
    }

    private void LoadNewEmail()
    {
        if (prewrittenEmails != null && prewrittenEmails.Count > 0)
        {
            // Select a random email from the list.
            currentEmail = prewrittenEmails[Random.Range(0, prewrittenEmails.Count)];
            
            // Reset the email text box and related variables.
            emailTextBox.text = "";
            currentLetterIndex = 0;

            // Disable the submit button as the email isn't fully written yet.
            if (submitButton != null)
                submitButton.interactable = false;
        }
    }
}
