using UnityEngine;

public class CardHoverHandler : MonoBehaviour {
    public Color hoverColor = Color.red;
    public Color useZoneColor = Color.green;
    public Color originalColor;

    private SpriteRenderer backgroundSpriteRenderer;
    private Vector3 originalPosition;
    private Vector3 offset;
    private bool isDragging = false;
    private float useZoneThreshold;
    private CardManager cardManager;

    void Start() {
        // Find the child object named "CardBackground" and get its SpriteRenderer component
        Transform backgroundTransform = transform.Find("CardBackground");
        if (backgroundTransform != null) {
            backgroundSpriteRenderer = backgroundTransform.GetComponent<SpriteRenderer>();
            if (backgroundSpriteRenderer != null) {
                originalColor = backgroundSpriteRenderer.color;
            }
            else {
                Debug.LogError("SpriteRenderer component not found on CardBackground of " + gameObject.name);
            }
        }
        else {
            Debug.LogError("CardBackground child object not found on " + gameObject.name);
        }

        originalPosition = transform.position;

        // Calculate the Y-coordinate for the use zone threshold (e.g., top half of the screen)
        useZoneThreshold = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height * 0.5f, 0)).y;

        // Cache the CardManager reference
        cardManager = FindObjectOfType<CardManager>();
        if (cardManager == null) {
            Debug.LogError("CardManager not found in the scene.");
        }
    }

    void OnMouseEnter() {
        if (backgroundSpriteRenderer != null) {
            backgroundSpriteRenderer.color = hoverColor;
        }
    }

    void OnMouseExit() {
        if (backgroundSpriteRenderer != null) {
            backgroundSpriteRenderer.color = originalColor;
        }
    }

    void OnMouseDown() {
        originalPosition = transform.position;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        isDragging = true;
    }

    void OnMouseDrag() {
        if (isDragging) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, originalPosition.z) + offset;
            transform.position = newPosition;

            if (transform.position.y >= useZoneThreshold) {
                if (backgroundSpriteRenderer != null) {
                    backgroundSpriteRenderer.color = useZoneColor;
                }
            }
            else {
                if (backgroundSpriteRenderer != null) {
                    backgroundSpriteRenderer.color = hoverColor;
                }
            }
        }
    }

    void OnMouseUp() {
        isDragging = false;

        if (transform.position.y >= useZoneThreshold) {
            // Implement card activation logic here
            Debug.Log("Card activated!");
            if (cardManager != null) {
                cardManager.RemoveCard(transform);
            }
            else {
                Debug.LogError("CardManager reference is null.");
            }
        }
        else {
            ReturnToOriginalPosition();
        }
    }

    private void ReturnToOriginalPosition() {
        transform.position = originalPosition;
        if (backgroundSpriteRenderer != null) {
            backgroundSpriteRenderer.color = originalColor;
        }
    }
}