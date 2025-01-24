using UnityEngine;

public class Bound : MonoBehaviour {
    private BoxCollider2D boxCollider;
    private RectTransform rectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        boxCollider.size = new(rectTransform.rect.width, rectTransform.rect.height);
    }
}
