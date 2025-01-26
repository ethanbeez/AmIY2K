using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FairyQuips : MonoBehaviour
{
    [Header("Quip Lists")]
    public List<string> gameQuips;  // Quips displayed during gameplay
    public List<string> pauseQuips; // Quips displayed when the game is paused
    public List<string> finalQuips; // Quips displayed in the last 60 seconds

    [Header("Scene Hooks")]
    public TMP_Text quipText; // Reference to the TMPro text component
    public CountdownTimer countdownTimer; // Reference to the CountdownTimer script

    [Header("Timing Settings")]
    [SerializeField] private float gameQuipInterval = 10f; // Time interval for game quips, adjustable in the Inspector

    private int currentGameQuipIndex = 0; // Tracks the current index for gameQuips
    private bool isPaused = false; // Tracks whether the game is paused

    private void Start()
    {
        if (quipText == null)
        {
            Debug.LogError("quipText is not assigned in the Inspector!");
        }

        if (countdownTimer == null)
        {
            Debug.LogError("countdownTimer is not assigned in the Inspector!");
        }

        if (gameQuips == null || gameQuips.Count == 0)
        {
            Debug.LogError("gameQuips list is empty or null!");
        }

        StartCoroutine(DisplayGameQuips());
    }

    private void Update()
    {
        if (!isPaused)
        {
            float timeLeft = countdownTimer.timeLeft;
            Debug.Log($"Time Left: {timeLeft}");

            if (timeLeft <= 60f && finalQuips.Count > 0)
            {
                // Show a random final quip during the last 60 seconds
                quipText.text = finalQuips[Random.Range(0, finalQuips.Count)];
                Debug.Log($"Displaying final quip: {quipText.text}");
            }

            if (timeLeft <= 0f)
            {
                EndGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        if (pauseQuips.Count > 0)
        {
            quipText.text = pauseQuips[Random.Range(0, pauseQuips.Count)];
            Debug.Log($"Game paused. Displaying pause quip: {quipText.text}");
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Debug.Log("Game resumed.");
    }

    private void EndGame()
    {
        isPaused = true;
        Debug.Log("Game Over. Timer has ended.");
        // You can add additional logic here for game-over actions.
    }

    private IEnumerator DisplayGameQuips()
    {
        Debug.Log("DisplayGameQuips started");

        while (countdownTimer != null && countdownTimer.timeLeft > 60f)
        {
            if (gameQuips.Count > 0)
            {
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

        Debug.Log("DisplayGameQuips stopped because timeLeft <= 60f");
    }
}
