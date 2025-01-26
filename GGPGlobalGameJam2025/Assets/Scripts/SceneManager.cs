using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [Header("Button Setup")]
    public GameObject startButton; // Reference to the Start Button
    public CountdownTimer countdownTimer; // Reference to the CountdownTimer script

    // Start is called before the first frame update
    void Start()
    {
        if (startButton != null)
        {
            // Add a listener to the Start Button
            startButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            Debug.LogError("Start Button is not assigned in the inspector!");
        }

        if (countdownTimer != null)
        {
            CountdownTimer.TimerFinished += OnTimerFinished; // Subscribe to the TimerFinished event
        }
        else
        {
            Debug.LogError("CountdownTimer is not assigned in the inspector!");
        }
    }

    // Called when the Start Button is clicked
    public void OnStartButtonClicked()
    {
        Debug.Log("Start Button clicked! Attempting to load MainGame scene...");
        LoadMainGameScene();
    }

    // This method loads the MainGame scene
    public void LoadMainGameScene()
    {
        SceneManager.LoadScene("MainGame"); // Replace "MainGame" with the exact name of your scene
    }

    // Called when the timer reaches zero
    public void OnTimerFinished(object sender)
    {
        Debug.Log("Timer hit zero! Loading EndScene...");
        LoadEndScene();
    }

    // This method loads the EndScene
    public void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene"); // Replace "EndScene" with the exact name of your scene
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
