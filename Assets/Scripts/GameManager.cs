using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Realm
    {
        Ice,
        Fire,
        Jungle
    }

    public Dictionary<Realm, List<bool>> checkpoints = new Dictionary<Realm, List<bool>>();
    private bool pause;
    private PlayerController playerController;
    private PlayerResources playerResources;
    private PlayerSkills playerSkills;
    private PlayerSpells playerSpells;
    private UI_Manager uiManager;
    private WeaponsHandler weaponsHandler;

    private void Awake()
    {
        checkpoints.Add(Realm.Ice, new List<bool>());
        checkpoints[Realm.Ice].Add(true);
        checkpoints[Realm.Ice].Add(false);
        checkpoints[Realm.Ice].Add(false);
        checkpoints[Realm.Ice].Add(false);

        checkpoints.Add(Realm.Fire, new List<bool>());
        checkpoints[Realm.Fire].Add(true);
        checkpoints[Realm.Fire].Add(false);
        checkpoints[Realm.Fire].Add(false);
        checkpoints[Realm.Fire].Add(false);
        var managers = GameObject.FindGameObjectsWithTag("GameManger");
        
        if (managers.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        uiManager = FindObjectOfType<UI_Manager>();
        playerResources = FindObjectOfType<PlayerResources>();
        playerSkills = FindObjectOfType<PlayerSkills>();
        playerSpells = FindObjectOfType<PlayerSpells>();
        playerController = FindObjectOfType<PlayerController>();
        weaponsHandler = FindObjectOfType<WeaponsHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            uiManager.Pause();
            playerResources.Pause();
            playerSpells.Pause();
            playerSkills.Pause();
            playerController.Pause();
            weaponsHandler.Pause();
        }

        // For Testing
        if (pause) return;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            playerResources.AddLife();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            playerResources.AddMana();
        if (Input.GetKeyDown(KeyCode.Alpha5))
            playerResources.AddSkillPoint();
        if (Input.GetKeyDown(KeyCode.Alpha6))
            playerSpells.Shockwave();
        if (Input.GetKeyDown(KeyCode.Alpha7))
            playerResources.TakeDamage(1, Vector3.zero);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            playerResources.UseMana();
    }
}