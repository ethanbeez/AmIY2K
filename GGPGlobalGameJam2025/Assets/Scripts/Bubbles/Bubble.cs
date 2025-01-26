using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Bubble : MonoBehaviour {
    public static int nextBubbleID = 0;
    [Header("Settings")]
    [SerializeField][Range(0.0f, 1.0f)] private float swirlLikelihood;
    [SerializeField] private float emptyDuration;
    [SerializeField] private float swirlDuration;
    [SerializeField] private float suckDuration;
    [SerializeField] private Image image;
    [Header("Animations")]
    [SerializeField] private Sprite[] emptySprites;
    [SerializeField] private Sprite[] swirlSprites;
    [SerializeField] private Sprite[] suckedUpVariant1Sprites;
    [SerializeField] private Sprite[] suckedUpVariant2Sprites;
    private Sprite[] currentAnimation;
    private BubbleState bubbleState;

    private int index = 0;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        Animate();
    }

    public int GetSoulValue() {
        return 1;
    }

    public void Animate() {
        if (currentAnimation == null || index == currentAnimation.Length - 1) {
            if (Random.value < swirlLikelihood) {
                bubbleState = BubbleState.Swirling;
                currentAnimation = swirlSprites;
                index = 0;
            } else {
                bubbleState = BubbleState.Empty;
                currentAnimation = emptySprites;
                index = 0;
            }
        }

        float duration;
        if (bubbleState == BubbleState.Empty) {
            duration = emptyDuration;
        } else if (bubbleState == BubbleState.Swirling) {
            duration = swirlDuration;
        } else {
            duration = suckDuration;
        }

        if ((timer += Time.deltaTime) >= (duration / currentAnimation.Length)) {
            timer = 0;
            image.sprite = currentAnimation[index];
            index = (index + 1) % currentAnimation.Length;
        }
    }

    /*public void InitializeEmptyAnimation(Sprite[] emptySprites) {
        this.emptySprites = emptySprites;
    }

    public void InitializeSwirlAnimation(Sprite[] swirlSprites) {
        this.swirlSprites = swirlSprites;
    }

    public void InitializeSuckedUpVariant2Animation(Sprite[] suckedUpVariant2Sprites) { 
        this.suckedUpVariant2Sprites = suckedUpVariant2Sprites;
    }*/
}

public enum BubbleColor { 
    Aqua,
    Green,
    Purple,
    Pink,
    Yellow,
    Blue
}

public enum BubbleState { 
    Empty,
    Swirling,
    Sucking
}