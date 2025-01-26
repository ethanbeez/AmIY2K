using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FairyQuips : MonoBehaviour
{
    [Header("Quip Lists")]
    public List<string> gameQuips;  // Quips displayed during gameplay
    public List<string> finalQuips; // Quips displayed in the last 60 seconds

    [Header("Scene Hooks")]
    public TMP_Text quipText; // Reference to the TMPro text component
    public CountdownTimer countdownTimer; // Reference to the CountdownTimer script
    [SerializeField] private AudioManager audioManager;

    [Header("Timing Settings")]
    [SerializeField] private float gameQuipInterval = 10f; // Time interval for game quips, adjustable in the Inspector

    private int currentGameQuipIndex = 0; // Tracks the current index for gameQuips
    private bool isDisplayingFinalQuip = false; // Tracks if a final quip is being displayed
    

    private void Start()
    {
        if (quipText == null)
        {
            Debug.LogError("quipText is not assigned in the Inspector!");
        }

        if (gameQuips == null || gameQuips.Count == 0)
        {
            Debug.LogError("gameQuips list is empty or null!");
        }

        StartCoroutine(DisplayGameQuips());
    }

    private void Update()
    {
        if (countdownTimer != null)
        {
            float timeLeft = countdownTimer.timeLeft;
            Debug.Log($"Time Left: {timeLeft}");

            if (timeLeft <= 60f && finalQuips.Count > 0 && !isDisplayingFinalQuip)
            {
                // Show a random final quip during the last 60 seconds
                StopCoroutine(DisplayGameQuips()); // Stop displaying game quips
                isDisplayingFinalQuip = true;
                string finalQuip = finalQuips[Random.Range(0, finalQuips.Count)];
                quipText.text = finalQuip;
                Debug.Log($"Displaying final quip: {finalQuip}");
            }
        }
    }

    private IEnumerator DisplayGameQuips()
    {
        Debug.Log("DisplayGameQuips started");

        while (true) // Always display game quips every 10 seconds
        {
            if (!isDisplayingFinalQuip)
            {
                if (gameQuips.Count > 0)
                {
                    audioManager.PlaySoundClip("Fairy" + Random.Range(1, 6));
                    quipText.text = gameQuips[currentGameQuipIndex];
                    Debug.Log($"Displaying game quip: {gameQuips[currentGameQuipIndex]}");

                    // Cycle to the next quip
                    currentGameQuipIndex = (currentGameQuipIndex + 1) % gameQuips.Count;
                }
                else
                {
                    Debug.LogError("gameQuips list is empty or null!");
                }

                yield return new WaitForSeconds(gameQuipInterval); // Use adjustable interval
            }
            else
            {
                yield break; // Stop the coroutine if a final quip is displayed
            }
        }
    }
}
