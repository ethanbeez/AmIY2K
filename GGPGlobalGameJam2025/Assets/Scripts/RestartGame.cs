using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void PlayAgain() {
        Debug.Log("Restart");
        SceneManager.LoadScene("MainMenu");
    }
}
