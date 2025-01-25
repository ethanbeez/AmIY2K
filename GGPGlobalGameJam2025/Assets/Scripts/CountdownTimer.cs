using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour {
    [SerializeField] private int gameLengthMinutes = 30;
    [Header("Scene Hooks")]
    [SerializeField] TextMeshProUGUI timerTextComponent;

    private float timeLeft;
    private bool timerActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        timerActive = false;
        timeLeft = gameLengthMinutes * 60;
    }

    // Update is called once per frame
    void Update() {
        if (timerActive && timeLeft > 0) { 
            timeLeft -= Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay() {
        float minutesLeft = Mathf.FloorToInt(timeLeft / 60);
        float secondsLeft = Mathf.FloorToInt(timeLeft % 60);
        timerTextComponent.text = string.Format("{0:00}:{1:00}", minutesLeft, secondsLeft);
    }

    public void Activate() { 
        timerActive = true;
    }
}
