using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private bool includeOnboard = false;
    [SerializeField] private int scoreThreshold0 = 100;
    [SerializeField] private int scoreThreshold1 = 300;

    [Header("Scene Hooks")]
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private SoulManager soulManager;
    [SerializeField] private SendEmail sendEmail;
    [SerializeField] private WriteEmail writeEmail;

    [SerializeField] private TextMeshProUGUI totalEmailsSent;
    [SerializeField] private TextMeshProUGUI totalSoulsCollected;
    [SerializeField] private TextMeshProUGUI endGameFairyText;
    [SerializeField] private GameObject mayhem;
    [SerializeField] private GameObject havoc;
    [SerializeField] private GameObject lame;
    // This refers to the GameObject containing the onboard UI.
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
        sendEmail.ResetState();
        writeEmail.ResetState();
        soulManager.ResetState();
        upgrades.ResetState();
    }

    public void EndGame() {
        countdownTimer.Deactivate();
        audioManager.PlaySoundClip("EndGame");
    }

    private void SetEndScreenScoreValues() {
        int soulsCaptured = soulManager.GetTotalSouls();
        int emailsSent = sendEmail.GetTotalEmailsSent();
        totalEmailsSent.text = emailsSent.ToString();
        totalSoulsCollected.text = soulsCaptured.ToString();
        DisableEndScrenScoreWords();
        if (soulsCaptured < scoreThreshold0) {
            lame.SetActive(true);
        } else if (soulsCaptured < scoreThreshold1) {
            havoc.SetActive(true);
        } else {
            mayhem.SetActive(true);
        }
    }

    private void DisableEndScrenScoreWords() {
        lame.SetActive(false);
        havoc.SetActive(false);
        mayhem.SetActive(false);
    }

    /// <summary>
    /// Start a new instance of the game. Here, game refers to the main gameplay loop of email-writing.
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
