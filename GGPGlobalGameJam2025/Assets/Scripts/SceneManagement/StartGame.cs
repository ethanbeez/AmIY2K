using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button startButton; // Reference to the button in the scene

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (startButton != null)
        {
            // Add listener to the button's onClick event
            startButton.onClick.AddListener(Play);
        }
        else
        {
            Debug.LogError("Start Button is not assigned in the Inspector!");
        }
    }

    public void Play()
    {
        Debug.Log("Start button clicked. Loading MainGame scene...");
        SceneManager.LoadScene("MainGame"); // Replace "MainGame" with the name of your game scene
    }
}
