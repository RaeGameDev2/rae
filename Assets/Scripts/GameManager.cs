using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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

        LoadAllData();

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

    public GameObject getCheckpointBySceneAndId(int id, int scene)
    {
        string sceneName = "";

        switch (scene)
        {
            case 2 :
                sceneName += "Fire";
                break;
            case 3 :
                sceneName += "Ice";
                break;
            case 4 :
                sceneName += "Jungle";
                break;
        }

        Debug.Log(sceneName + " Checkpoint " + (id + 1));
        var checkpoint = GameObject.Find(sceneName + " Checkpoint " + (id + 1));

        return checkpoint;
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
        

        if (SceneManager.GetActiveScene().buildIndex == scene) {
            var loader = GameObject.Find("Crossfade");
            loader.GetComponent<Animator>().SetTrigger("Start");
            crossfadeStartAnimation = true;
            StartCoroutine(MovePlayer(loader, id, scene));
        } else {  
            checkpointId = id;
            StartCoroutine(ChangeScene(scene));
            Debug.Log(checkpointId + " " + transform.position);
        }

    }

    public bool crossfadeStartAnimation;
    public bool crossfadeEndAnimation;
    private IEnumerator MovePlayer(GameObject loader, int id, int scene)
    {
        while (crossfadeStartAnimation)
            yield return new WaitForFixedUpdate();
        playerController.transform.position = getCheckpointBySceneAndId(id, scene).transform.position;
        crossfadeEndAnimation = true;
        loader.GetComponent<Animator>().SetTrigger("End");
    }

    private IEnumerator ChangeScene(int scene)
    {
        var loader = GameObject.Find("Crossfade");
        loader.GetComponent<Animator>().SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scene);
    }

    public void SaveCheckpoints()
    {
        Debug.Log("Checkpoints SAVED!!");

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/checkpoints.data";


        Debug.Log(Application.persistentDataPath + "/checkpoints.data");
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, checkpoints);
        stream.Close();
    }

    public void LoadCheckpoints()
    {
        Debug.Log("Checkpoints LOADED!!");

        string path = Application.persistentDataPath + "/checkpoints.data";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            this.checkpoints = formatter.Deserialize(stream) as Dictionary<Realm, List<bool>>;
            stream.Close();
        } else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }

    public void SaveVolume()
    {
        Debug.Log("Volume SAVED!!");
        string fileName = Application.persistentDataPath + "/volume.data";

        StreamWriter stream = new StreamWriter(fileName);

        stream.WriteLine(volume);
        stream.Close();
    }

    public void LoadVolume()
    {
        Debug.Log("Volume LOADED!!");

        string path = Application.persistentDataPath + "/volume.data";
        if (File.Exists(path))
        {
            StreamReader readStream = new StreamReader(path);

            var floatString = readStream.ReadLine();
            volume = float.Parse(floatString);
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }

    public void SaveSkillLevel()
    {
        Debug.Log("Skill SAVED!!");

        string fileName = Application.persistentDataPath + "/skill.data";

        StreamWriter stream = new StreamWriter(fileName);

        stream.WriteLine(skillLevel.Length);
        foreach (int i in skillLevel)
        {
            stream.WriteLine(i);
        }
        stream.Close();
    }

    public void LoadSkillLevel()
    {
        Debug.Log("Skill LOADED!!");

        string path = Application.persistentDataPath + "/skill.data";
        if (File.Exists(path))
        {
            StreamReader readStream = new StreamReader(path);

            int size = int.Parse(readStream.ReadLine());

            for (int i = 0; i < size; i++)
            {
                skillLevel[i] = int.Parse(readStream.ReadLine());
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }

    public void LoadAllData()
    {
        LoadCheckpoints();
        LoadVolume();
        LoadSkillLevel();
    }
}