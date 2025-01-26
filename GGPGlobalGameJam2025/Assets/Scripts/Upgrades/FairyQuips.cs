using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FairyQuips : MonoBehaviour
{
    [Header("Quip Lists")]
    public List<string> gameQuips;
    public List<string> pauseQuips;
    public List<string> finalQuips;
    public string badQuip;  // Ending quip for <= 100 souls
    public string midQuip; // Ending quip for 101–150 souls
    public string goodQuip; // Ending quip for > 150 souls

    [Header("Scene Hooks")]
    public TMP_Text quipText; // Reference to the TMPro text component
    public CountdownTimer countdownTimer; // Reference to the CountdownTimer script
    public SoulManager soulManager; // Reference to the SoulManager script

    private int currentGameQuipIndex = 0;
    private bool isPaused = false;

    private void Start()
    {
        StartCoroutine(DisplayGameQuips());
    }

    private void Update()
    {
        if (!isPaused)
        {
            float timeLeft = countdownTimer.timeLeft;

            if (timeLeft <= 60f && quipText.text != finalQuips[0])
            {
                // Show a random final quip when the timer is in the last 60 seconds
                quipText.text = finalQuips[Random.Range(0, finalQuips.Count)];
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
        quipText.text = pauseQuips[Random.Range(0, pauseQuips.Count)];
    }

    public void ResumeGame()
    {
        isPaused = false;
    }

    private void EndGame()
    {
        isPaused = true;

        // Determine the ending quip based on total souls collected
        int totalSouls = soulManager.GetTotalSouls();

        if (totalSouls <= 100)
        {
            quipText.text = badQuip;
        }
        else if (totalSouls <= 150)
        {
            quipText.text = midQuip;
        }
        else
        {
            quipText.text = goodQuip;
        }

        Debug.Log($"Game Over. Total Souls: {totalSouls}. Ending Quip: {quipText.text}");
    }

    private IEnumerator DisplayGameQuips()
    {
        while (countdownTimer.timeLeft > 60f)
        {
            quipText.text = gameQuips[currentGameQuipIndex];
            currentGameQuipIndex = (currentGameQuipIndex + 1) % gameQuips.Count;
            yield return new WaitForSeconds(180f); // Wait for 3 minutes
        }
    }
}
