using TMPro;
using UnityEngine;

public class EndGameDisplay : MonoBehaviour
{
    [Header("Title RectTransforms")]
    [SerializeField] private RectTransform goodTitle; // RectTransform for the good title
    [SerializeField] private RectTransform midTitle;  // RectTransform for the mid title
    [SerializeField] private RectTransform badTitle;  // RectTransform for the bad title

    [Header("Fairy Text Box")]
    [SerializeField] private TextMeshProUGUI fairyTextBox; // The fairy's text box

    private void Start()
    {
        // Retrieve the total number of souls collected from PlayerPrefs
        int totalSouls = PlayerPrefs.GetInt("TotalSouls", 0);

        // Hide all titles by default
        goodTitle.gameObject.SetActive(false);
        midTitle.gameObject.SetActive(false);
        badTitle.gameObject.SetActive(false);

        // Change the title and fairy message based on the number of souls
        if (totalSouls < 10)
        {
            // Show the bad title and set the message
            badTitle.gameObject.SetActive(true);
            fairyTextBox.text = "You barely scratched the surface. Try harder next time!";
        }
        else if (totalSouls >= 10 && totalSouls < 20)
        {
            // Show the mid title and set the message
            midTitle.gameObject.SetActive(true);
            fairyTextBox.text = "You're getting there! With a bit more effort, you'll do great.";
        }
        else
        {
            // Show the good title and set the message
            goodTitle.gameObject.SetActive(true);
            fairyTextBox.text = "Amazing work! You've mastered the art of soul collecting!";
        }
    }
}
