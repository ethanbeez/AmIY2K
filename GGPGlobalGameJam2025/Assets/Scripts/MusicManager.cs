using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private SoulManager soulManager;
    public AudioMixerSnapshot layerOn;          
    public int currentBubbleCount = 0;
    public int switchBubbleCount;

    // Start is called before the first frame update
    void Start()
    {
        if (soulManager == null)
        {
            Debug.LogError("SoulManager reference not assigned in BubbleTracker!");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (soulManager != null)
        {
            currentBubbleCount = soulManager.GetSpawnedBubbleCount();
        }

        if (currentBubbleCount == switchBubbleCount)
        {
            layerOn.TransitionTo(5f);
        }

    }
}