using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private float durationEffectChangingScene = 1f;

    private RectTransform healthBar;
    [SerializeField] private Image healthPoint;
    [SerializeField] private List<Image> healthPointInstances = new List<Image>();
    [SerializeField] private Image lightImage;
    private GameObject levelLoader;

    private RectTransform manaBar;
    [SerializeField] private Image manaPoint;
    [SerializeField] private List<Image> manaPointInstances = new List<Image>();

    private int offsetHealthSegments;
    private int offsetManaSegments;
    private bool pause;

    private PlayerResources playerResources;
    private GameObject uiBars;
    private GameObject skillHUDCanvas;

    private bool stUI;
    private UI_SkillTree uiSkillTree;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "HubWorld")
        {
            healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<RectTransform>();
            manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<RectTransform>();
            playerResources = FindObjectOfType<PlayerResources>();
            uiSkillTree = FindObjectOfType<UI_SkillTree>();
            InitUiBars();

            uiBars = GameObject.Find("UI Bars");
            skillHUDCanvas = GameObject.Find("SkillHUDCanvas");

            HideUI();
        }

        levelLoader = GameObject.Find("LevelLoader");

        GameManager.instance.fadingFromBlackAnimation = true;
        StartCoroutine(FadeFromBlack());
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Tab)) return;

        if (stUI == false)
        {
            ShowUI();
            stUI = true;
            GameManager.instance.Pause();
        }
        else
        {
            HideUI();
            stUI = false;
            GameManager.instance.Pause();
        }
    }

    private void HideUI()
    {
        uiSkillTree.gameObject.SetActive(false);
        uiBars.SetActive(true);
        skillHUDCanvas.SetActive(true);
    }

    private void ShowUI()
    {
        uiSkillTree.gameObject.SetActive(true);
        uiBars.SetActive(false);
        skillHUDCanvas.SetActive(false);
    }

    public void InitUiBars()
    {
        for (var i = 0; i < playerResources.currentHealth; i++) {
            AddLife();
            Debug.Log($"{i} {offsetHealthSegments}");
        }
        for (var i = 0; i < playerResources.currentMana; i++)
            AddMana();

        foreach (var healthPointInstance in healthPointInstances)
        {
            Debug.Log(healthPointInstance.transform.position);
        }
    }

    public IEnumerator FadeFromBlack()
    {
        var image = levelLoader.GetComponent<Image>();
        var color = image.color;
        var albedo = 1f;
        var time = durationEffectChangingScene;

        while (time > 0f)
        {
            yield return new WaitForFixedUpdate();
            time -= durationEffectChangingScene * Time.fixedDeltaTime;
            albedo -= Time.fixedDeltaTime;
            color.a = albedo;
            image.color = color;
        }

        image.color = new Color(color.r, color.g, color.b, 0);
        levelLoader.SetActive(false);
        GameManager.instance.fadingFromBlackAnimation = false;
    }

    public IEnumerator FadeToBlack()
    {
        levelLoader.SetActive(true);
        var image = levelLoader.GetComponent<Image>();
        var color = image.color;
        var albedo = 0f;
        var time = durationEffectChangingScene;

        while (time > 0f)
        {
            time -= durationEffectChangingScene * Time.fixedDeltaTime;
            albedo += Time.fixedDeltaTime;
            color.a = albedo;
            image.color = color;
            yield return new WaitForFixedUpdate();
        }

        image.color = new Color(color.r, color.g, color.b, 1);
        GameManager.instance.fadingToBlackAnimation = false;
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
        lightImage.gameObject.SetActive(true);
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
            lightImage.gameObject.SetActive(false);
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