using TMPro;
using UnityEngine;

public class EndQuips : MonoBehaviour
{
    [Header("Quips")]
    [SerializeField] private string badQuip = "You can do better next time!";
    [SerializeField] private string midQuip = "Not bad, keep it up!";
    [SerializeField] private string goodQuip = "Excellent work, superstar!";

    [Header("Scene Hooks")]
    [SerializeField] private TMP_Text quipText;

    [Header("Title RectTransforms")]
    [SerializeField] private RectTransform goodTitle;
    [SerializeField] private RectTransform midTitle;
    [SerializeField] private RectTransform badTitle;

    [Header("Display Results Reference")]
    [SerializeField] private DisplayResults displayResults;

    private int totalSoulsCollected = 0;

    private void Start()
    {
        if (displayResults != null)
        {
            totalSoulsCollected = displayResults.GetTotalSouls(); // Use the public method in DisplayResults
            DisplayResultsData();
        }
        else
        {
            Debug.LogError("DisplayResults reference is missing in EndQuips.");
        }
    }

    private void DisplayResultsData()
    {
        // Hide all titles by default
        goodTitle.gameObject.SetActive(false);
        midTitle.gameObject.SetActive(false);
        badTitle.gameObject.SetActive(false);

        // Update quip text and title based on souls collected
        if (totalSoulsCollected <= 100)
        {
            quipText.text = badQuip;
            badTitle.gameObject.SetActive(true);
        }
        else if (totalSoulsCollected > 100 && totalSoulsCollected <=200)
        {
            quipText.text = midQuip;
            midTitle.gameObject.SetActive(true);
        }
        else
        {
            quipText.text = goodQuip;
            goodTitle.gameObject.SetActive(true);
        }

        Debug.Log($"Souls Collected: {totalSoulsCollected}, Quip: {quipText.text}");
    }
}
