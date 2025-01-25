using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public GameObject upgradePrefab; // Reference to the prefab to display
    public Button upgradeButton; // Reference to the Upgrade button

    void Start()
    {
        // Ensure the button has an onClick listener
        if (upgradeButton != null)
        {
            upgradeButton.onClick.AddListener(ToggleUpgrade);
        }
        else
        {
            Debug.LogError("Upgrade button is not assigned!");
        }

        // Ensure the prefab is initially disabled
        if (upgradePrefab != null)
        {
            upgradePrefab.SetActive(false);
        }
        else
        {
            Debug.LogError("Upgrade prefab is not assigned!");
        }
    }

    private void ToggleUpgrade()
    {
        if (upgradePrefab != null)
        {
            // Toggle the active state of the prefab
            bool isActive = upgradePrefab.activeSelf;
            upgradePrefab.SetActive(!isActive);

            Debug.Log(isActive ? "Upgrade prefab turned off." : "Upgrade prefab turned on.");
        }
        else
        {
            Debug.LogError("Upgrade prefab is not assigned!");
        }
    }
}
