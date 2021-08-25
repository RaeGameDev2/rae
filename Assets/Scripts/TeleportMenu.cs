using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportMenu : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectsOfType<GameManager>().FirstOrDefault(manager => manager.isDontDestroyOnLoad);
    }

    public void FireCheckpoint(int id)
    {
        // Debug.Log($"IceCheckpoint called id: {id}");
        gameManager.ChangeCheckpointId(id, 2);
    }
    public void IceCheckpoint(int id)
    {
        // Debug.Log($"IceCheckpoint called id: {id}");
        gameManager.ChangeCheckpointId(id, 3);
    }
    public void JungleCheckpoint(int id)
    {
        // Debug.Log($"IceCheckpoint called id: {id}");
        gameManager.ChangeCheckpointId(id, 4);
    }

    public void Hub()
    {
        gameManager.ChangeCheckpointId(0, 1);
    }

    public void ResetCheckpoints()
    {
        string checkpoints_file = Application.persistentDataPath + "/checkpoints.data";
        if (File.Exists(checkpoints_file))
        {
            File.Delete(checkpoints_file);
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_WEBPLAYER
                Application.OpenURL(webplayerQuitURL);
            #else
                Application.Quit();
            #endif
        }
    }
}