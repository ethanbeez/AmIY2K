using UnityEngine;

public enum CurrentGameContext { 
    MainMenu,
    InGame,
    EndScreen
}

public class GameManager : MonoBehaviour {
    [Header("Scene Hooks")]
    [SerializeField] private GameObject countdownTimer;
    // This refers to the GameObject containing the onboard UI.
    [SerializeField] private GameObject onboardUI;

    private bool includeOnboard;
    private GameInstance currentGameInstance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        includeOnboard = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Start a new instance of the game. Here, game refers to the main gameplay loop of email-writing.
    /// </summary>
    public void StartNewGame(bool includeOnboard) {
        if (includeOnboard) {
            // Include the onboard tutorial if the player hasn't completed it before.
            InitializeOnboard();
        } else {

        }
    }

    private void InitializeOnboard() { 
        
    }
}
