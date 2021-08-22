using UnityEngine;

public class TweenText : MonoBehaviour
{
    [SerializeField] private float tweenTime;

    public void Tween()
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;
        LeanTween.scale(gameObject, Vector2.one * 1.5f, tweenTime).setEasePunch().setIgnoreTimeScale(true);
    }

}
