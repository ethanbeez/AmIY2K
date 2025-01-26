using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class ScreenShake : MonoBehaviour {
    [SerializeField] private SoulManager soulManager;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private AnimationCurve bloomFlash;
    [SerializeField] private float bloomStartingValue;
    [SerializeField] private float bloomEndingValue;
    private const int bpm = 145;
    [SerializeField] private Volume v;
    private Bloom b;
    private Vignette vg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        InvokeRepeating("StartShake", 0f, (float) 60 / bpm);
        v.profile.TryGet(out b);
        v.profile.TryGet(out vg);
    }

    // Update is called once per frame
    void Update() {

    }

    private void StartShake() {
        StartCoroutine("Shake");
    }

    private IEnumerator Shake() {
        float time = 0f;
        while (time < 0.2) {
            time += Time.deltaTime;
            int spawnedSouls = soulManager.GetSpawnedBubbleCount();
            float strength = 1f * Mathf.Clamp(((float) spawnedSouls / 15), 0, 1);
            float bloomStrength = 1f * Mathf.Clamp(((float) spawnedSouls / 15), 0, 1);
            b.threshold.value = Mathf.Lerp(bloomStartingValue - (bloomEndingValue * bloomStrength), bloomStartingValue, bloomFlash.Evaluate(time / 0.1f));
            b.scatter.value = Mathf.Lerp(0.4f + 0.2f * bloomStrength, 0.4f, bloomFlash.Evaluate(time / 0.1f));
            canvasRect.localPosition = Random.insideUnitCircle * strength;
            yield return null;
        }
    }

    [ContextMenu("test")]
    private void Test() {
        b.scatter.value = 0.1f;
        vg.intensity.value = 0.5f;
    }
}
