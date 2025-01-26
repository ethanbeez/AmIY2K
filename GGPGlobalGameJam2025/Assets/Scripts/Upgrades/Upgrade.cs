using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour {
    public GameObject upgradePrefab; // Reference to the prefab to display
    public Button upgradeButton; // Reference to the Upgrade button
    public Button minimizeButton; // Reference to the Minimize button

    [SerializeField] private List<MultiplierLevel> MultiplierLevels;
    [SerializeField] private List<AutoSendUpgrade> AutoSendLevels;
    [SerializeField] private List<SpeedTypeUpgrade> SpeedTypeLevels;
    public MultiplierLevel CurrentEmailMultiplierUpgrade { get; private set; }
    public AutoSendUpgrade CurrentAutoSendUpgrade { get; private set; }
    public SpeedTypeUpgrade CurrentSpeedTypeUpgrade { get; private set; }

    [Header("Scene Hooks")]
    [SerializeField] private TextMeshProUGUI nextMultiplierLevel;
    [SerializeField] private TextMeshProUGUI nextMultiplierDescription;
    [SerializeField] private Button multiplierUpgradeButton;
    [SerializeField] private TextMeshProUGUI nextMultiplierCost;

    [SerializeField] private TextMeshProUGUI nextAutoSendLevel;
    [SerializeField] private TextMeshProUGUI nextAutoSendDescription;
    [SerializeField] private Button autoSendUpgradeButton;
    [SerializeField] private TextMeshProUGUI nextAutoSendCost;

    [SerializeField] private TextMeshProUGUI nextSpeedTypeLevel;
    [SerializeField] private TextMeshProUGUI nextSpeedTypeDescription;
    [SerializeField] private Button speedTypeUpgradeButton;
    [SerializeField] private TextMeshProUGUI nextSpeedTypeCost;

    [SerializeField] private SoulManager soulManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SendEmail sendEmail;
    [SerializeField] private WriteEmail writeEmail;

    private Coroutine autoSendCoroutine;

    void Start() {
        SoulManager.SoulsCollected += OnSoulsCollected;
        // Ensure the buttons have an onClick listener
        if (upgradeButton != null) {
            upgradeButton.onClick.AddListener(ToggleUpgrade);
        } else {
            Debug.LogError("Upgrade button is not assigned!");
        }

        if (upgradeButton != null) {
            minimizeButton.onClick.AddListener(ToggleUpgrade);
        } else {
            Debug.LogError("Minimize button is not assigned!");
        }

        // Ensure the prefab is initially disabled
        if (upgradePrefab != null) {
            upgradePrefab.SetActive(false);
        } else {
            Debug.LogError("Upgrade prefab is not assigned!");
        }
        ResetState();
    }

    private void OnSoulsCollected(object sender) {
        UpdateUpgradeValues();
    }

    public void ResetState() {
        if (autoSendCoroutine != null) {
            StopCoroutine(autoSendCoroutine);
        }
        autoSendCoroutine = null;
        CurrentEmailMultiplierUpgrade = new(0, 1, 0);
        CurrentAutoSendUpgrade = new(0, 100_000, 0, 0);
        CurrentSpeedTypeUpgrade = new(0, 1, 0);
    }

    public void ToggleUpgrade() {
        if (upgradePrefab != null) {
            // Toggle the active state of the prefab
            bool isActive = upgradePrefab.activeSelf;
            upgradePrefab.SetActive(!isActive);
            if (!isActive) {
                UpdateUpgradeValues();
            }
            Debug.Log(isActive ? "Upgrade prefab turned off." : "Upgrade prefab turned on.");
        } else {
            Debug.LogError("Upgrade prefab is not assigned!");
        }
    }

    public void UpdateUpgradeValues() {
        // Handling email multiplier upgrade
        if (CurrentEmailMultiplierUpgrade.level >= MultiplierLevels.Count) {
            nextMultiplierLevel.text = "MAX";
            nextMultiplierDescription.text = $"The Send button sends [{CurrentEmailMultiplierUpgrade.multiplier}] emails per click.";
            nextMultiplierCost.text = "";
            multiplierUpgradeButton.interactable = false;
        } else { 
            nextMultiplierLevel.text = $"Lvl {CurrentEmailMultiplierUpgrade.level + 1}.";
            nextMultiplierDescription.text = $"The Send button will send [{MultiplierLevels[CurrentEmailMultiplierUpgrade.level].multiplier}] emails per click.";
            nextMultiplierCost.text = $"{MultiplierLevels[CurrentEmailMultiplierUpgrade.level].cost} Souls";
            if (soulManager.GetStoredSouls() < MultiplierLevels[CurrentEmailMultiplierUpgrade.level].cost) {
                multiplierUpgradeButton.interactable = false;
            } else {
                multiplierUpgradeButton.interactable = true;
            }
        }

        // Handling autosend upgrade
        if (CurrentAutoSendUpgrade.level >= AutoSendLevels.Count) {
            nextAutoSendLevel.text = "MAX";
            nextAutoSendDescription.text = $"Every [{CurrentAutoSendUpgrade.sendIntervalSeconds}] seconds, a burst of [{CurrentAutoSendUpgrade.sendBurstCount}] emails is sent.";
            nextMultiplierCost.text = "";
            multiplierUpgradeButton.interactable = false;
        } else {
            nextAutoSendLevel.text = $"Lvl {CurrentAutoSendUpgrade.level + 1}.";
            nextAutoSendDescription.text = $"Every [{AutoSendLevels[CurrentAutoSendUpgrade.level].sendIntervalSeconds}] seconds, a burst of [{AutoSendLevels[CurrentAutoSendUpgrade.level].sendBurstCount}] emails is sent.";
            nextMultiplierCost.text = $"{AutoSendLevels[CurrentAutoSendUpgrade.level].cost} Souls";
            if (soulManager.GetStoredSouls() < AutoSendLevels[CurrentAutoSendUpgrade.level].cost) {
                autoSendUpgradeButton.interactable = false;
            } else {
                autoSendUpgradeButton.interactable = true;
            }
        }

        // Handling speed type upgrade
        if (CurrentSpeedTypeUpgrade.level >= SpeedTypeLevels.Count) {
            nextSpeedTypeLevel.text = "MAX";
            nextSpeedTypeDescription.text = $"Type [{CurrentSpeedTypeUpgrade.wordsPerKeystroke}] words per keystroke when writing emails.";
            nextSpeedTypeCost.text = "";
            speedTypeUpgradeButton.interactable = false;
        } else {
            nextSpeedTypeLevel.text = $"Lvl {CurrentSpeedTypeUpgrade.level + 1}.";
            nextSpeedTypeDescription.text = $"Type [{SpeedTypeLevels[CurrentSpeedTypeUpgrade.level].wordsPerKeystroke}] words per keystroke when writing emails.";
            nextSpeedTypeCost.text = $"{SpeedTypeLevels[CurrentSpeedTypeUpgrade.level].cost} Souls";
            if (soulManager.GetStoredSouls() < SpeedTypeLevels[CurrentSpeedTypeUpgrade.level].cost) {
                speedTypeUpgradeButton.interactable = false;
            } else {
                speedTypeUpgradeButton.interactable = true;
            }
        }
    }

    public void UpgradeEmailMultiplier() {
        if (CurrentEmailMultiplierUpgrade.level >= MultiplierLevels.Count || !soulManager.SpendSouls(MultiplierLevels[CurrentEmailMultiplierUpgrade.level].cost)) {
            return;
        }
        CurrentEmailMultiplierUpgrade = MultiplierLevels[CurrentEmailMultiplierUpgrade.level];
        audioManager.PlaySoundClip("Purchase");
        UpdateUpgradeValues();
    }

    public void UpgradeAutoSend() {
        if (CurrentAutoSendUpgrade.level >= AutoSendLevels.Count || !soulManager.SpendSouls(AutoSendLevels[CurrentAutoSendUpgrade.level].cost)) {
            return;
        }
        CurrentAutoSendUpgrade = AutoSendLevels[CurrentAutoSendUpgrade.level];
        audioManager.PlaySoundClip("Purchase");
        UpdateUpgradeValues();
        // Only start the coroutine once; i.e., on level 1.
        if (CurrentAutoSendUpgrade.level == 1) {
            autoSendCoroutine = StartCoroutine(AutoSend());
        }
    }

    public void UpgradeSpeedType() {
        if (CurrentSpeedTypeUpgrade.level >= SpeedTypeLevels.Count || !soulManager.SpendSouls(SpeedTypeLevels[CurrentSpeedTypeUpgrade.level].cost)) {
            return;
        }
        CurrentSpeedTypeUpgrade = SpeedTypeLevels[CurrentSpeedTypeUpgrade.level];
        audioManager.PlaySoundClip("Purchase");
        UpdateUpgradeValues();
    }

    private IEnumerator AutoSend() {
        while (true) {
            sendEmail.Send(CurrentAutoSendUpgrade.sendBurstCount);
            yield return new WaitForSeconds(CurrentAutoSendUpgrade.sendIntervalSeconds);
        }
    }
}
