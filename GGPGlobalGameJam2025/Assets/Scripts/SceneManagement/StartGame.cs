using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button startButton; // Reference to the button in the scene
    [SerializeField] private bool anyInputPressed;
    private bool zoomInAnimationCompleted;
    [SerializeField] private AudioSource mainMenuMusic;
    [SerializeField] private AnimationCurve zoomInCurve;
    [SerializeField] Vector3 cameraStartingPosition;
    [SerializeField] Vector3 cameraEndingPosition;
    [SerializeField] private float cameraStartingZoom;
    [SerializeField] private float cameraEndingZoom;
    [SerializeField] private float zoomInDuration;
    [SerializeField] private Camera zoomCamera;
    [SerializeField] private Camera uiMainCamera;
    [SerializeField] private float startingVolume;
    [SerializeField] private float endingVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zoomCamera = GameObject.Find("ZoomCamera").GetComponent<Camera>();
        uiMainCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        uiMainCamera.gameObject.SetActive(false);
        anyInputPressed = false;
        zoomInAnimationCompleted = false;
        mainMenuMusic.volume = startingVolume;
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

    void Update() 
    {
        if (!anyInputPressed && Input.anyKeyDown) {
            anyInputPressed = true;
            StartCoroutine(ZoomInCamera());
        }
    }

    public void Play()
    {
        if (!zoomInAnimationCompleted) {
            return;
        }
        Debug.Log("Start button clicked. Loading MainGame scene...");
        SceneManager.LoadScene("MainGame"); // Replace "MainGame" with the name of your game scene
    }

    public IEnumerator ZoomInCamera() 
    {
        float elapsed = 0f;
        while (elapsed < zoomInDuration) {
            float time = Mathf.Clamp01(elapsed / zoomInDuration);
            float progress = zoomInCurve.Evaluate(time);
            zoomCamera.transform.position = Vector3.Lerp(cameraStartingPosition, cameraEndingPosition, progress);
            zoomCamera.orthographicSize = Mathf.Lerp(cameraStartingZoom, cameraEndingZoom, progress);
            mainMenuMusic.volume = Mathf.Lerp(startingVolume, endingVolume, progress);
            elapsed += Time.deltaTime;
            yield return null;
        }

        zoomCamera.transform.position = cameraEndingPosition;
        zoomCamera.orthographicSize = cameraEndingZoom;

        uiMainCamera.gameObject.SetActive(true);
        zoomCamera.gameObject.SetActive(false);
        zoomInAnimationCompleted = true;
    }
}
