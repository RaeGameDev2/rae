using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private bool pause;

    public Image healthPoint;
    public Image manaPoint;
    public Image lightImage;

    public List<Image> healthPointInstances = new List<Image>();
    public List<Image> manaPointInstances = new List<Image>();

    private int offsetHealthSegments;
    private int offsetManaSegments;

    private RectTransform healthBar;
    private RectTransform manaBar;

    private Resources resources;

    private void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<RectTransform>();
        manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<RectTransform>();
        resources = GameObject.FindGameObjectWithTag("Player").GetComponent<Resources>();
        for (var i = 0; i < resources.maxHealth; i++)
            AddLife();
        for (var i = 0; i < resources.maxMana; i++)
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
