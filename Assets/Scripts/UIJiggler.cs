using UnityEngine;

public class UIJiggler : MonoBehaviour
{
    public float minAmount = 5f;  // Minimum amount of jiggle
    public float maxAmount = 15f; // Maximum amount of jiggle
    public float minSpeed = 0.5f; // Minimum jiggle speed
    public float maxSpeed = 2f;   // Maximum jiggle speed

    private RectTransform rectTransform;
    private Vector3 originalPosition;
    private float jiggleAmount;
    private float jiggleSpeed;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.localPosition;

        // Generate random jiggle amount and speed for each UI image
        jiggleAmount = Random.Range(minAmount, maxAmount);
        jiggleSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        float jiggleTimer = Time.time * jiggleSpeed;
        float xOffset = Mathf.Sin(jiggleTimer) * jiggleAmount;
        float yOffset = Mathf.Cos(jiggleTimer) * jiggleAmount;

        // Apply random jiggle to the UI image's local position
        rectTransform.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0f);
    }
}