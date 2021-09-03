using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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

    public static GameManager instance;

    private int checkpointId;

    public Dictionary<Realm, List<bool>> checkpoints = new Dictionary<Realm, List<bool>>();
    
    public bool fadingToBlackAnimation;
    public bool fadingFromBlackAnimation;
    
    [SerializeField] private GameObject fireHealthPointsPrefab;
    [SerializeField] private GameObject iceHealthPointsPrefab;
    public bool isDontDestroyOnLoad;
    [SerializeField] private GameObject jungleHealthPointsPrefab;

    public int lastCheckpointId = 0;

    private bool pause;
    private PlayerController playerController;
    private PlayerResources playerResources;
    private PlayerSkills playerSkills;
    private PlayerSpells playerSpells;
    public int[] skillLevel = new int[12];
    private UI_Manager uiManager;
    public float volume;
    private WeaponsHandler weaponsHandler;

    public static bool spawn_fire_core;
    public static bool spawn_ice_core;
    public static bool spawn_nature_core;

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

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            isDontDestroyOnLoad = true;
        }
        else
        {
            Destroy(gameObject);
        }
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

        Debug.Log("gameManager " + playerController.transform.position);
        var checkpointsGameObjects = GameObject.FindGameObjectsWithTag("Checkpoint");
        if (checkpointsGameObjects.Length == 4)
        {
            foreach (var checkpoint in checkpointsGameObjects)
                if (checkpoint.GetComponent<PortalCheckpoint>().portalId == checkpointId)
                    playerController.transform.position = checkpoint.transform.position;
        }
        else
        {
            var ice_Core = GameObject.FindGameObjectWithTag("IceCore");
            var fire_Core = GameObject.FindGameObjectWithTag("FireCore");
            var nature_Core = GameObject.FindGameObjectWithTag("NatureCore");
            if (spawn_fire_core == false)
            {
                fire_Core.SetActive(false);
            }

            if(spawn_ice_core == false)
            {
                ice_Core.SetActive(false);
            }

            if(spawn_nature_core == false)
            {
                nature_Core.SetActive(false);
            }
        }
        Debug.Log("gameManager " + playerController.transform.position);
        LoadAllData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            uiManager.Pause();
            playerResources.Pause();
            playerSpells.Pause();
            playerController.Pause();
            weaponsHandler.Pause();
        }

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

    public void Pause()
    {
        pause = !pause;
        uiManager.Pause();
        playerResources.Pause();
        playerSpells.Pause();
        playerController.Pause();
        weaponsHandler.Pause();
    }

    private GameObject GetCheckpointById(int id)
    {
        var checkpointsGameObjects = GameObject.FindGameObjectsWithTag("Checkpoint");

        return checkpointsGameObjects.FirstOrDefault(go => go.GetComponent<PortalCheckpoint>().portalId == id);
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

        if (SceneManager.GetActiveScene().buildIndex == scene)
        {
            StartCoroutine(MovePlayer(id, scene));
        }
        else
        {
            checkpointId = id;
            StartCoroutine(ChangeScene(scene));
        }
    }

    private IEnumerator MovePlayer(int id, int scene)
    {
        fadingToBlackAnimation = true;
        StartCoroutine(uiManager.FadeToBlack());
        while (fadingToBlackAnimation)
            yield return new WaitForFixedUpdate();
        playerController.transform.position = GetCheckpointById(id).transform.position;
        fadingToBlackAnimation = true;
        StartCoroutine(uiManager.FadeFromBlack());
    }

    private IEnumerator ChangeScene(int scene)
    {
        fadingToBlackAnimation = true;
        StartCoroutine(uiManager.FadeToBlack());
        while (fadingToBlackAnimation)
            yield return new WaitForFixedUpdate();
        SceneManager.LoadScene(scene);
    }

    public void SaveCheckpoints()
    {
        var formatter = new BinaryFormatter();
        var path = Application.persistentDataPath + "/checkpoints.data";


        Debug.Log(Application.persistentDataPath + "/checkpoints.data");
        var stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, checkpoints);
        stream.Close();
    }

    public void LoadCheckpoints()
    {
        var path = Application.persistentDataPath + "/checkpoints.data";

        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);

            checkpoints = formatter.Deserialize(stream) as Dictionary<Realm, List<bool>>;
            stream.Close();
        }
    }

    public void SaveVolume()
    {
        var fileName = Application.persistentDataPath + "/volume.data";

        var stream = new StreamWriter(fileName);

        stream.WriteLine(volume);
        stream.Close();
    }

    public void LoadVolume()
    {
        var path = Application.persistentDataPath + "/volume.data";
        if (File.Exists(path))
        {
            var readStream = new StreamReader(path);

            var floatString = readStream.ReadLine();
            volume = float.Parse(floatString);
        }
    }

    public void SaveSkillPoints()
    {
        var fileName = Application.persistentDataPath + "/skillpoints.data";

        var stream = new StreamWriter(fileName);

        stream.WriteLine(playerSkills.playerSkills.GetSkillPoints());

        stream.Close();
    }

    public void LoadSkillPoints()
    {
        var path = Application.persistentDataPath + "/skillpoints.data";
        if (File.Exists(path))
        {
            var readStream = new StreamReader(path);

            var skillPoints = int.Parse(readStream.ReadLine());

            playerSkills.playerSkills.SetSkillPoints(skillPoints);
        }
    }

    public void SaveSkillLevel()
    {
        var fileName = Application.persistentDataPath + "/skill.data";

        var stream = new StreamWriter(fileName);

        stream.WriteLine(skillLevel.Length);
        foreach (var i in skillLevel) stream.WriteLine(i);

        stream.Close();
    }

    public void LoadSkillLevel()
    {
        var path = Application.persistentDataPath + "/skill.data";
        if (File.Exists(path))
        {
            var readStream = new StreamReader(path);

            var size = int.Parse(readStream.ReadLine());

            for (var i = 0; i < size; i++) skillLevel[i] = int.Parse(readStream.ReadLine());

            playerSkills.playerSkills.SetSkillLevel();
        }
    }

    private void RespawnHealthPoints()
    {
        string name;

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 2:
                Debug.Log("Fire hpoints instantiated");
                name = fireHealthPointsPrefab.name;
                Destroy(GameObject.Find(name));
                Instantiate(fireHealthPointsPrefab, new Vector3(0, 0, 0), Quaternion.identity).name = name;
                break;
            case 3:
                Debug.Log("Ice hpoints instantiated");
                name = iceHealthPointsPrefab.name;
                Destroy(GameObject.Find(name));
                Instantiate(iceHealthPointsPrefab, new Vector3(0, 0, 0), Quaternion.identity).name = name;
                break;
            case 4:
                Debug.Log("Jungle hpoints instantiated");
                name = jungleHealthPointsPrefab.name;
                Destroy(GameObject.Find(name));
                Instantiate(jungleHealthPointsPrefab, new Vector3(0, 0, 0), Quaternion.identity).name = name;
                break;
        }
    }

    public void Die()
    {
        // TODO : Play player's death animation
        StartCoroutine(MovePlayer(lastCheckpointId, SceneManager.GetActiveScene().buildIndex));
        RespawnHealthPoints();
    }

    private void LoadAllData()
    {
        LoadCheckpoints();
        LoadVolume();
        LoadSkillPoints();
        LoadSkillLevel();
    }

    public void ResetAllData()
    {
        checkpoints.Clear();
        playerSkills.playerSkills.SetSkillPoints(10);
        skillLevel = new int[10];
    }
}