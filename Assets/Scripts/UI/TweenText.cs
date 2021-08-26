using TMPro;
using UnityEngine;

public class TweenText : MonoBehaviour
{
    [SerializeField] private float tweenTime;
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    public void Tween()
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;
        LeanTween.scale(gameObject, Vector2.one * 1.5f, tweenTime).setEasePunch().setIgnoreTimeScale(true);
    }

    public void FadeOut()
    {
        LeanTween.cancel(gameObject);
        var color = text.color;
        var fadeoutcolor = color;
        color.a = 0f;
        LeanTween.value(gameObject, updateValueExampleCallback, fadeoutcolor, color, 0.5f).setEase(LeanTweenType.linear);
    }

    void updateValueExampleCallback(Color val)
    {
        text.color = val;
    }
}
