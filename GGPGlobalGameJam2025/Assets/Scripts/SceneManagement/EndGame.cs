using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private CountdownTimer countdownTimer; // Reference to the CountdownTimer script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (countdownTimer != null)
        {
            // Subscribe to the TimerFinished event
            CountdownTimer.TimerFinished += OnTimerFinished;
        }
        else
        {
            Debug.LogError("CountdownTimer is not assigned in the inspector!");
        }
    }

    // This method is called when the timer finishes
    private void OnTimerFinished(object sender)
    {
        // Save total souls before transitioning
        FindObjectOfType<SoulManager>()?.SaveTotalSouls();
        // Scene Handler Script
        FindObjectOfType<SendEmail>()?.SaveEmailData();

        
        Debug.Log("Timer hit zero! Transitioning to EndScene...");
        SceneManager.LoadScene("EndScreen"); // Replace "EndScene" with the actual name of your scene
    }

    private void OnDestroy()
    {
        // Unsubscribe from the TimerFinished event to avoid potential issues
        if (countdownTimer != null)
        {
            CountdownTimer.TimerFinished -= OnTimerFinished;
        }
    }
}
