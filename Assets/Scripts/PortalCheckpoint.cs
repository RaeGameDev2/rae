using System;
using UnityEngine;

public class PortalCheckpoint : MonoBehaviour
{
    private GameManager gameManager;
    public int portalId;
    [SerializeField] private GameManager.Realm portalType;
    [SerializeField] private GameObject teleportMenu;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
                gameManager.checkpoints[GameManager.Realm.Ice][portalId] = true;
                // TODO: Save in file
                break;
            case GameManager.Realm.Fire:
                gameManager.checkpoints[GameManager.Realm.Fire][portalId] = true;
                break;
            case GameManager.Realm.Jungle:
                gameManager.checkpoints[GameManager.Realm.Jungle][portalId] = true;
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
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}