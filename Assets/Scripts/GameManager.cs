using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Realm
    {
        Ice,
        Fire,
        Jungle
    }

    public Dictionary<Realm, List<bool>> checkpoints = new Dictionary<Realm, List<bool>>();
    public float volume;
    public int[] skillLevel = new int[12];

    private bool pause;
    private PlayerController playerController;
    private PlayerResources playerResources;
    private PlayerSkills playerSkills;
    private PlayerSpells playerSpells;
    private UI_Manager uiManager;
    private WeaponsHandler weaponsHandler;

    private int checkpointId;
    public bool isDontDestroyOnLoad;

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

        checkpoints.Add(Realm.Jungle, new List<bool>());
        checkpoints[Realm.Jungle].Add(true);
        checkpoints[Realm.Jungle].Add(false);
        checkpoints[Realm.Jungle].Add(false);
        checkpoints[Realm.Jungle].Add(false);

        checkpointId = 0;

        var managers = GameObject.FindGameObjectsWithTag("GameManger");
        if (managers.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            isDontDestroyOnLoad = true;
        }
        else
            Destroy(gameObject);

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"OnSceneLoaded called checkpointId {checkpointId}");
        uiManager = FindObjectOfType<UI_Manager>();
        playerResources = FindObjectOfType<PlayerResources>();
        playerSkills = FindObjectOfType<PlayerSkills>();
        playerSpells = FindObjectOfType<PlayerSpells>();
        playerController = FindObjectOfType<PlayerController>();
        weaponsHandler = FindObjectOfType<WeaponsHandler>();
        var checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        if (checkpoints.Length == 4)
        {
            foreach (var checkpoint in checkpoints)
            {
                if (checkpoint.GetComponent<PortalCheckpoint>().portalId == checkpointId)
                {
                    playerController.transform.position = checkpoint.transform.position;
                }
            }
        }
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

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ChangeCheckpointId(int id, int scene)
    {
        switch (scene)
        {
            case 2 when !checkpoints[Realm.Fire][id]:
            case 3 when !checkpoints[Realm.Ice][id]:
            case 4 when !checkpoints[Realm.Jungle][id]:
                return;
        }
        checkpointId = id;
        StartCoroutine(ChangeScene(scene));
        Debug.Log(checkpointId + " " + transform.position);
    }
    private IEnumerator ChangeScene(int scene)
    {
        var loader = GameObject.Find("Crossfade");
        loader.GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }
}