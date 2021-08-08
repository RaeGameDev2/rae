using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public static void Create(Vector3 position, int damageAmount, bool isCrit)
    {
        GameObject damagePopup = Instantiate(UnityEngine.Resources.Load("DamageText", typeof(GameObject)) as GameObject, position, Quaternion.identity);
        damagePopup.AddComponent<DamagePopup>().Setup(damageAmount, isCrit);
    }
    private static int sortingOrder;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * .5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer <= 0)
        {
            float disappearSpeed = 30f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            Destroy(gameObject, .5f);
        }
    }

    public void Setup(int damageAmount, bool isCrit)
    {
        textMesh.SetText(damageAmount.ToString());
        textMesh.fontSize = isCrit ? 50 : 25;
        textColor = isCrit ? Color.red : Color.yellow;
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;
        moveVector = new Vector3(1, 1) * 30f;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }
}
