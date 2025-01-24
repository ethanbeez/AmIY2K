using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoulManager : MonoBehaviour {
    // The number of souls that the player currently posseses.
    [SerializeField] private int soulsStored;
    [SerializeField] private int totalSouls;
    // Souls that have been 'captured'; i.e., released by sending emails.
    [SerializeField] private float capturedSouls;

    [Header("Scene Hooks")]
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private GameObject bubbleContainer;
    [SerializeField] private TextMeshProUGUI soulsStoredCounter;

    private Dictionary<int, GameObject> spawnedBubbles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        soulsStored = 0;
        spawnedBubbles = new();
    }

    // Update is called once per frame
    void Update() {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Tab)) {
            capturedSouls++;
        }
#endif
        HandleSpawnedBubbles();
    }

    /// <summary>
    /// Handle one email being sent, updating the number of captured souls depending on the overall count of emails sent.
    /// Note that here, count does not mean all emails ever sent by the player; it is referring to the number of times the
    /// current email has been sent.
    /// </summary>
    /// <param name="count">The number of times the current email has been sent</param>
    public void HandleEmailSent(int count) {
        float returnRate;
        if (count < 20) {
            returnRate = 0.25f;
        } else if (count < 40) {
            returnRate = 0.125f;
        } else if (count < 60) {
            returnRate = 0.0625f;
        } else {
            returnRate = 0.0313f;
        }
        capturedSouls += returnRate;
    }

    public void CollectSouls() {
        foreach (GameObject spawnedBubble in spawnedBubbles.Values) {
            int soulValue = spawnedBubble.GetComponent<Bubble>().GetSoulValue();
            soulsStored += soulValue;
            totalSouls += soulValue;
            Destroy(spawnedBubble); // TODO: After animations are done, only destroyed after it's sucked up
        }
        capturedSouls = 0;
        spawnedBubbles.Clear();
        UpdateSoulDisplay();
    }

    public void UpdateSoulDisplay() {
        soulsStoredCounter.text = soulsStored.ToString();
    }

    private void HandleSpawnedBubbles() {
        int targetBubbleCount = (int) capturedSouls;
        int bubblesToSpawn = targetBubbleCount - spawnedBubbles.Count;
        for (int i = 0; i < bubblesToSpawn; i++) {
            GameObject newBubble = Instantiate(bubblePrefab, bubbleContainer.transform, false);
            newBubble.transform.localPosition = GenerateBubbleSpawn();
            // newBubble.GetComponent<Rigidbody2D>().AddForce(new(Random.Range(-2, 2), Random.Range(-2, 2)));
            newBubble.GetComponent<Rigidbody2D>().linearVelocityX = Random.Range(10, 15) * ((Random.value >= 0.5) ? -1 : 1);
            newBubble.GetComponent<Rigidbody2D>().linearVelocityY = Random.Range(10, 15) * ((Random.value >= 0.5) ? -1 : 1);

            spawnedBubbles.Add(Bubble.nextBubbleID++, newBubble);
        }
    }

    private Vector3 GenerateBubbleSpawn() {
        Rect rect = bubbleContainer.GetComponent<RectTransform>().rect;
        float x = Random.Range(-rect.width / 2, rect.width / 2);
        float y = Random.Range(-rect.height / 2, rect.height / 2);
        return new(x, y, 1);
    }
}
