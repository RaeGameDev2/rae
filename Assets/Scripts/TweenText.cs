using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenText : MonoBehaviour
{
    public float tweenTime;

    public void Tween()
    {
        LeanTween.cancel(gameObject);
        transform.localScale = Vector3.one;
        LeanTween.scale(gameObject, Vector2.one * 1.5f, tweenTime)
        .setEasePunch().setIgnoreTimeScale(true);

    }


}
