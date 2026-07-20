using UnityEngine;

public class ShineEffect : MonoBehaviour
{
    public float speed = 200f;
    public float range = 300f;

    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(Mathf.PingPong(Time.time * speed, range), rectTransform.anchoredPosition.y);
    }
}