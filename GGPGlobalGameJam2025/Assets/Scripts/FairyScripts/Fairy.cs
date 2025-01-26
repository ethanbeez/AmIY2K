using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEvents { 
    
}


public class Fairy : MonoBehaviour {
    [SerializeField] private List<string> onboardingQuips;
    private Dictionary<int, string> countdownQuips;
    private Dictionary<GameEvents, string> gameEventQuips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
