using UnityEngine;

public class Bubble : MonoBehaviour {
    public static int nextBubbleID = 0;
    private float timeAlive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        timeAlive = 0f;
    }

    // Update is called once per frame
    void Update() {
        timeAlive += Time.deltaTime;
    }

    public int GetSoulValue() {
        if (timeAlive < 120) {
            return 1;
        } else if (timeAlive < 180) {
            return 2;
        } else {
            return 3;
        }
    }
}
