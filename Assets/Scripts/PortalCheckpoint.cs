using System;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;

public class PortalCheckpoint : MonoBehaviour
{
    private GameManager gameManager;
    public int portalId;
    [SerializeField] private GameManager.Realm portalType;
    [SerializeField] private GameObject teleportMenu;
    [SerializeField] private GameObject teleportClosingAnimationPrefab;
    [SerializeField] private GameObject teleportOpenAnimationPrefab;
    [SerializeField] private GameObject teleportOpeningAnimationPrefab;

    private GameObject animationClosing;
    private GameObject animationOpening;
    private GameObject animationOpen;

    private void Start()
    {
        gameManager = FindObjectsOfType<GameManager>().FirstOrDefault(manager => manager.isDontDestroyOnLoad);
        teleportMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        animationOpening = Instantiate(teleportOpeningAnimationPrefab, transform.position, Quaternion.identity, transform);
        teleportMenu.SetActive(true);
        switch (portalType)
        {
            case GameManager.Realm.Ice:
                if (!gameManager.checkpoints[GameManager.Realm.Ice][portalId])
                {
                    gameManager.checkpoints[GameManager.Realm.Ice][portalId] = true;
                    gameManager.SaveCheckpoints();
                }

                gameManager.lastCheckpointId = portalId;
                break;
            case GameManager.Realm.Fire:
                if (!gameManager.checkpoints[GameManager.Realm.Fire][portalId])
                {
                    gameManager.checkpoints[GameManager.Realm.Fire][portalId] = true;
                    gameManager.SaveCheckpoints();
                }

                gameManager.lastCheckpointId = portalId;
                break;
            case GameManager.Realm.Jungle:
                if (!gameManager.checkpoints[GameManager.Realm.Jungle][portalId])
                {
                    gameManager.checkpoints[GameManager.Realm.Jungle][portalId] = true;
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
        teleportMenu.SetActive(false);
        Destroy(animationOpen);
        animationClosing = Instantiate(teleportClosingAnimationPrefab, transform.position, Quaternion.identity, transform);
    }

    public void onPortalOpened()
    {
        Destroy(animationOpening);
        animationOpen = Instantiate(teleportOpenAnimationPrefab, transform.position, Quaternion.identity, transform);
    }

    public void OnPortalClosed()
    {
        Destroy(animationClosing);
    }
}