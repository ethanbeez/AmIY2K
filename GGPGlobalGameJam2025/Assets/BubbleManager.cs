using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour {
    private const string emptyVariantName = "Empty_Bubble_";
    [SerializeField] private List<GameObject> bubbleVariants;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // InstantiateBubbleAnimations();
    }

    // Update is called once per frame
    void Update() {

    }

    /*private void InstantiateBubbleAnimations() { 
        foreach (GameObject bubble in bubbleVariants) {
            Bubble bubbleComponent = bubble.GetComponent<Bubble>();

            bubbleComponent.InitializeEmptyAnimation(GetEmptySprites(emptyVariantName + $"{bubbleComponent.bubbleColor}_"));
        }
    }*/
}
