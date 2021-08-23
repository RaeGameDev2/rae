using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportMenu : MonoBehaviour
{
    [SerializeField] private GameObject icePortal1;
    [SerializeField] private GameObject icePortal2;
    [SerializeField] private GameObject icePortal3;
    [SerializeField] private GameObject icePortal4;
    [SerializeField] private GameObject player;

    public void Portal1()
    {
        player.transform.position = icePortal1.transform.position;
    }

    public void Portal2()
    {
        player.transform.position = icePortal2.transform.position;
    }

    public void Portal3()
    {
        player.transform.position = icePortal3.transform.position;
    }

    public void Portal4()
    {
        player.transform.position = icePortal4.transform.position;
    }

    public void IceCheckpoint1()
    {
        SceneManager.LoadScene(3); //ice
    }

    public void Hub()
    {
        SceneManager.LoadScene(1);
    }
}