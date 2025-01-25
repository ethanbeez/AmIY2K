using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Bubble : MonoBehaviour {
    public static int nextBubbleID = 0;
    private float timeAlive;
    [SerializeField] private float duration;
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites;

    private int index = 0;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        timeAlive = 0f;
    }

    // Update is called once per frame
    void Update() {
        timeAlive += Time.deltaTime;
        if ((timer += Time.deltaTime) >= (duration / sprites.Length)) {
            timer = 0;
            image.sprite = sprites[index];
            index = (index + 1) % sprites.Length;
        }
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
