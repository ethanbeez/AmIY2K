using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour {
    [SerializeField] public int gameLengthMinutes = 30;
    [SerializeField] private float gameSpeedMultiplier = 3;
    [Header("Scene Hooks")]
    [SerializeField] TextMeshPro timerTextComponent;
    [SerializeField] AudioManager audioManager;

    public float timeLeft;
    [SerializeField] public bool timerActive = true;

    public delegate void TimerFinishedHandler(object sender);
    public static event TimerFinishedHandler TimerFinished;

    public bool finalCountdown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        timeLeft = gameLengthMinutes * 60;
        finalCountdown = false;
        timerActive = true;
    }

    // Update is called once per frame
    void Update() {
        if (timerActive && timeLeft > 0) {
            timeLeft -= Time.deltaTime * gameSpeedMultiplier;
            if (!finalCountdown && timeLeft < 10 * gameSpeedMultiplier) {
                finalCountdown = true;
                InvokeRepeating("FinalCountdown", 0f, 1);
            }
            UpdateTimerDisplay();
        }
        if (timeLeft <= 0) {
            TimerFinished?.Invoke(this);
        }
    }

    private void UpdateTimerDisplay() {
        if (timeLeft < 0) {
            timeLeft = 0;
        }
        float minutesLeft = Mathf.FloorToInt(timeLeft / 60);
        float secondsLeft = Mathf.FloorToInt(timeLeft % 60);
        timerTextComponent.text = string.Format("{0:00}:{1:00}", minutesLeft, secondsLeft);
    }

    public void Activate() {
        timerActive = true;
    }

    public void Deactivate() {
        // CancelInvoke();
        timerActive = false;
    }

    public void ResetState() {       
        timeLeft = gameLengthMinutes * 60;
        gameSpeedMultiplier = 3;
        finalCountdown = false;
    }

    public void FinalCountdown() {
        if (timerActive) {
            audioManager.PlaySoundClip("FinalCountdownBeep");
        }
    }

    public void Pause() {
        timerActive = false;
    }

    public void Unpause() {
        timerActive = true;
    }
}
