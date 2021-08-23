using System.Collections;
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
}