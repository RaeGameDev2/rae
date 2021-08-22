using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportMenu : MonoBehaviour
{
    private GameObject icePortal1;
    private GameObject icePortal2;
    private GameObject icePortal3;
    private GameObject icePortal4;
    private GameObject player;

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

    public void Hub()
    {
        SceneManager.LoadScene(1);
    }
}