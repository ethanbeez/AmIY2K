
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour {
    [SerializeField] private List<SoundClip> soundClips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        foreach (SoundClip soundClip in soundClips) {
            soundClip.source = gameObject.AddComponent<AudioSource>();
            soundClip.source.clip = soundClip.clip;
            soundClip.source.volume = soundClip.volume;
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void PlaySoundClip(string name) { 
        foreach (SoundClip soundClip in soundClips) {
            if (soundClip.name == name) {
                soundClip.source.Play();
            }
        }
    }
}

[System.Serializable]
public class SoundClip {
    public AudioClip clip;
    public string name;
    public AudioSource source;
    [Range(0, 1)] public float volume = 1.0f;
}
