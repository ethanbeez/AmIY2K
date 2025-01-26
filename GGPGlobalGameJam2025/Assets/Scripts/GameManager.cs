using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private bool includeOnboard = false;

    [Header("Scene Hooks")]
    [SerializeField] private CountdownTimer countdownTimer;

    [SerializeField] private TextMeshProUGUI endGameFairyText;
    [SerializeField] private GameObject onboardUI;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Upgrade upgrades;

    [Header("Text")]
    [SerializeField] private string endGameLameText = "C'mon, it's almost like you don’t want to watch the world burn.";
    [SerializeField] private string endGameHavocText = "Not bad, not bad. For a normie non-magical, I guess.";
    [SerializeField] private string endGameMayhemText = "OMG ur actually the bestest person everrrrr! Thaaanks bestie!";
    private GameInstance currentGameInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        includeOnboard = true;
        countdownTimer.Deactivate();
        CountdownTimer.TimerFinished += OnTimerFinished;
    }

    private void Update() {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.RightControl)) {
            EndGame();
        }
#endif
    }

    private void OnTimerFinished(object sender) {
        EndGame();
    }

    private void ResetGameState() {
        countdownTimer.ResetState();
        upgrades.ResetState();
    }

    public void EndGame() {
        countdownTimer.Deactivate();
        // audioManager.PlaySoundClip("EndGame");
    }

    /// <summary>
    /// Start a new instance of the game. Here, game refers to the main gameplay loop.
    /// </summary>
    public void StartNewGame() {
        if (includeOnboard) {
            // Include the onboard tutorial if the player hasn't completed it before.
            InitializeOnboard();
        }
        countdownTimer.Activate();
        audioManager.PlaySoundClip("StartGame");
    }

    private void InitializeOnboard() { 
        
    }
}
