using TMPro;
using UnityEngine;

public class EndQuips : MonoBehaviour
{
    [Header("Quips")]
    [SerializeField] private string badQuip = "You can do better next time!";  // Quip for low performance
    [SerializeField] private string midQuip = "Not bad, keep it up!";          // Quip for average performance
    [SerializeField] private string goodQuip = "Excellent work, superstar!";  // Quip for high performance

    [Header("Scene Hooks")]
    [SerializeField] private TMP_Text quipText; // Reference to the TMPro text component

    [SerializeField] private string soulManagerObjectName = "SoulManager"; // Name of the SoulManager GameObject

    private int totalSoulsCollected = 0; // Tracks souls collected dynamically

    private void Start()
    {
        FetchSoulManagerData();
        if (totalSoulsCollected == 0) // Double-check and try finding again
        {
            Debug.LogWarning("SoulManager not found initially. Retrying...");
            FetchSoulManagerData(); // Retry fetching the data
        }

        DisplayQuip();
    }

    private void FetchSoulManagerData()
    {
        GameObject soulManagerObject = GameObject.Find(soulManagerObjectName);
        if (soulManagerObject == null)
        {
            Debug.LogError("SoulManager GameObject not found. Check the name in the scene and EndQuips.");
        }

        if (soulManagerObject != null)
        {
            SoulManager soulManager = soulManagerObject.GetComponent<SoulManager>();
            if (soulManager != null)
            {
                totalSoulsCollected = soulManager.GetTotalSouls();
                Debug.Log($"Total Souls Collected: {totalSoulsCollected}");
            }
            else
            {
                Debug.LogError("SoulManager component not found on the specified GameObject.");
            }
        }
        else
        {
            Debug.LogError("SoulManager GameObject not found in the scene.");
        }
    }

    public void DisplayQuip()
    {
        if (totalSoulsCollected <= 5)
        {
            quipText.text = badQuip;
        }
        else if (totalSoulsCollected > 5 && totalSoulsCollected <= 10)
        {
            quipText.text = midQuip;
        }
        else
        {
            quipText.text = goodQuip;
        }

        Debug.Log($"Souls Collected: {totalSoulsCollected}, Quip: {quipText.text}");
    }
}
