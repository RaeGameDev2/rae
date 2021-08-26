using System;
using UnityEngine;
using System.Linq;

public class PortalCheckpoint : MonoBehaviour
{
    private GameManager gameManager;
    public int portalId;
    [SerializeField] private GameManager.Realm portalType;
    [SerializeField] private GameObject teleportMenu;

    private void Start()
    {
        gameManager = FindObjectsOfType<GameManager>().FirstOrDefault(manager => manager.isDontDestroyOnLoad);
        teleportMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        teleportMenu.SetActive(true);
        switch (portalType)
        {
            case GameManager.Realm.Ice:
                if (!gameManager.checkpoints[GameManager.Realm.Ice][portalId])
                {
                    gameManager.checkpoints[GameManager.Realm.Ice][portalId] = true;
                    // TODO: Save in file
                    gameManager.SaveCheckpoints();
                }

                gameManager.lastCheckpointId = portalId;
                break;
            case GameManager.Realm.Fire:
                if (!gameManager.checkpoints[GameManager.Realm.Fire][portalId])
                {
                    gameManager.checkpoints[GameManager.Realm.Fire][portalId] = true;
                    // TODO: Save in file
                    gameManager.SaveCheckpoints();
                }

                gameManager.lastCheckpointId = portalId;
                break;
            case GameManager.Realm.Jungle:
                if (!gameManager.checkpoints[GameManager.Realm.Jungle][portalId])
                {
                    gameManager.checkpoints[GameManager.Realm.Jungle][portalId] = true;
                    // TODO: Save in file
                    gameManager.SaveCheckpoints();
                }

                gameManager.lastCheckpointId = portalId;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        Debug.Log(gameManager.checkpoints[GameManager.Realm.Ice][1]);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        switch (portalType)
        {
            case GameManager.Realm.Ice:
                teleportMenu.SetActive(false);
                break;
            case GameManager.Realm.Fire:
                teleportMenu.SetActive(false);
                break;
            case GameManager.Realm.Jungle:
                teleportMenu.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}