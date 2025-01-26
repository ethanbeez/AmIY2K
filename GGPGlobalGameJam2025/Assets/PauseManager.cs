using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private GameObject timerBox;
    [SerializeField] private TextMeshPro timerText;
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private WriteEmail writeEmail;
    [SerializeField] private Color timerBoxColor;
    private bool paused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        paused = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused) {
                paused = true;
                pauseOverlay.SetActive(true);
                timerBox.GetComponent<SpriteRenderer>().color = timerBoxColor;
                timerText.color = timerBoxColor;
                countdownTimer.Pause();
                writeEmail.SetGamePaused(true);
            } else {
                paused = false;
                pauseOverlay.SetActive(false);
                timerBox.GetComponent<SpriteRenderer>().color = Color.white;
                timerText.color = Color.white;
                countdownTimer.Unpause();
                writeEmail.SetGamePaused(false);
            }
        }
    }

    public void Resume() {
        paused = false;
        pauseOverlay.SetActive(false);
        timerBox.GetComponent<SpriteRenderer>().color = Color.white;
        timerText.color = Color.white;
        countdownTimer.Unpause();
        writeEmail.SetGamePaused(false);
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
