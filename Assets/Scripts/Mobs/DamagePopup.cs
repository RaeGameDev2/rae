using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private const float DISAPPEAR_TIMER_MAX = 1f;
    private static int sortingOrder;
    private float disappearTimer;
    private Vector3 moveVector;
    private Color textColor;
    private float rotation = 0f;

    private TextMeshPro textMesh;

    public static void Create(Vector3 position, int damageAmount, bool isCrit)
    {
        var damagePopup = Instantiate(Resources.Load("DamageText", typeof(GameObject)) as GameObject, position,
            Quaternion.identity);
        damagePopup.AddComponent<DamagePopup>().Setup(damageAmount, isCrit);
    }

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
            var increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
            rotation -= 0.4f;
        }
        else
        {
            var decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
            rotation += 0.8f;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer <= 0)
        {
            var disappearSpeed = 30f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            Destroy(gameObject, .5f);
        }
        transform.rotation = Quaternion.Euler(0, 0, rotation);
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