using System;
using UnityEngine;

public class PortalCheckpoint : MonoBehaviour
{
    private GameManager gameManager;
    public int portalId;
    [SerializeField] private GameManager.Realm portalType;
    [SerializeField] private GameObject teleportMenu;

    private GameObject animationClosing;
    private GameObject animationOpening;
    private GameObject animationOpen;
    private TeleportBackground teleportBackground;

    private void Awake()
    {
        teleportBackground = GetComponentInChildren<TeleportBackground>();
        teleportBackground.gameObject.SetActive(false);
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        teleportMenu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        if (teleportBackground.gameObject.activeInHierarchy)
            teleportBackground.gameObject.SetActive(false);
        teleportBackground.gameObject.SetActive(true);

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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;
        teleportMenu.SetActive(false);
        teleportBackground.ClosePortal();
    }
}