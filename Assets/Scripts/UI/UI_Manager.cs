using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private RectTransform healthBar;

    [SerializeField] private Image healthPoint;

    [SerializeField] private List<Image> healthPointInstances = new List<Image>();
    [SerializeField] private Image lightImage;
    private RectTransform manaBar;
    [SerializeField] private Image manaPoint;
    [SerializeField] private List<Image> manaPointInstances = new List<Image>();

    private int offsetHealthSegments;
    private int offsetManaSegments;
    private bool pause;

    private PlayerResources playerResources;

    private void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<RectTransform>();
        manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<RectTransform>();
        playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        for (var i = 0; i < playerResources.maxHealth; i++)
            AddLife();
        for (var i = 0; i < playerResources.maxMana; i++)
            AddMana();
    }

    public void Pause()
    {
        pause = !pause;
    }

    public void AddLife()
    {
        var instance = Instantiate(healthPoint, Vector3.zero, Quaternion.identity, healthBar);
        instance.rectTransform.anchoredPosition = new Vector2(offsetHealthSegments, 0);
        instance.rectTransform.localScale = Vector3.one;
        healthPointInstances.Add(instance);
        offsetHealthSegments += 30;
    }

    public void TakeLives(int number)
    {
        for (var i = 0; i < number; i++)
        {
            Destroy(healthPointInstances[healthPointInstances.Count - 1].gameObject);
            healthPointInstances.Remove(healthPointInstances[healthPointInstances.Count - 1]);
            offsetHealthSegments -= 30;
        }

        if (offsetHealthSegments == 0)
            lightImage.enabled = false;
    }

    public void AddMana()
    {
        var instance = Instantiate(manaPoint, Vector3.zero, Quaternion.identity, manaBar);
        instance.rectTransform.anchoredPosition = new Vector2(offsetManaSegments, 0);
        instance.rectTransform.localScale = Vector3.one;
        manaPointInstances.Add(instance);
        offsetManaSegments += 30;
    }

    public void UseMana()
    {
        Destroy(manaPointInstances[manaPointInstances.Count - 1].gameObject);
        manaPointInstances.Remove(manaPointInstances[manaPointInstances.Count - 1]);
        offsetManaSegments -= 30;
    }
}