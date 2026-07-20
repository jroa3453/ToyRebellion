using UnityEngine;

public class WordBob : MonoBehaviour
{
    public float speed = 2f;
    public float amplitude = 10f;
    public float timeOffset = 0f;

    public RectTransform rectTransform;
    public float startY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;
    }

    void Update()
    {
        float newY = startY + Mathf.Sin((Time.time + timeOffset) * speed) * amplitude;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
    }
}