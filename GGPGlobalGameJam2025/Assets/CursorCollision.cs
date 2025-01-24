using UnityEditor;
using UnityEngine;

public class CursorCollision : MonoBehaviour {
    private Vector3 lastMousePosition;
    [SerializeField] private float mouseVelocityMultiplier = 100_000f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.position = new(Input.mousePosition.x, Input.mousePosition.y);
    }

    private void LateUpdate() {
        lastMousePosition = Input.mousePosition;    
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.rigidbody == null) { 
            return;
        }
        Vector3 mousePosition = new(Input.mousePosition.x, Input.mousePosition.y, 0); // Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - collision.transform.position).normalized;
        collision.rigidbody.AddForce(direction * -GetMouseVelocity(), ForceMode2D.Impulse);
    }

    private float GetMouseVelocity() { 
        Vector3 delta = Input.mousePosition - lastMousePosition;
        return delta.magnitude * mouseVelocityMultiplier;
    }
}
