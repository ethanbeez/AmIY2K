using TMPro;
using UnityEngine;

public class DisplayResults : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalSoulsText; // Displays total souls collected
    [SerializeField] private TextMeshProUGUI totalEmailsText; // Displays total emails sent

    private void Start()
    {
        // Fetch total souls using the prefab fallback method
        int totalSouls = PlayerPrefs.GetInt("TotalSouls", 0);
        totalSoulsText.text = $"{totalSouls}";

        // Fetch total emails using PlayerPrefs (added to SendEmail below)
        int totalEmails = PlayerPrefs.GetInt("TotalEmailsSent", 0);
        totalEmailsText.text = $"{totalEmails}";
    }
}
