using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportMenu : MonoBehaviour
{
    private GameObject icePortal1;
    private GameObject icePortal2;
    private GameObject icePortal3;
    private GameObject icePortal4;
    private GameObject player;

    [SerializeField]private GameObject icePortal1Button;
    [SerializeField]private GameObject icePortal2Button;
    [SerializeField]private GameObject icePortal3Button;
    [SerializeField]private GameObject icePortal4Button;

    [SerializeField]private GameObject firePortal1Button;
    [SerializeField]private GameObject firePortal2Button;
    [SerializeField]private GameObject firePortal3Button;
    [SerializeField]private GameObject firePortal4Button;

    [SerializeField]private GameObject junglePortal1Button;
    [SerializeField]private GameObject junglePortal2Button;
    [SerializeField]private GameObject junglePortal3Button;
    [SerializeField]private GameObject junglePortal4Button;

    public void OpenMeniuPortalIce()
    {
        
        if(icePortal1Button.activeInHierarchy == true)
        {
            icePortal1Button.SetActive(false);
            icePortal2Button.SetActive(false);
            icePortal3Button.SetActive(false);
            icePortal4Button.SetActive(false);
        }
        else
        {
            icePortal1Button.SetActive(true);
            icePortal2Button.SetActive(true);
            icePortal3Button.SetActive(true);
            icePortal4Button.SetActive(true);

            junglePortal1Button.SetActive(false);
            junglePortal2Button.SetActive(false);
            junglePortal3Button.SetActive(false);
            junglePortal4Button.SetActive(false);

            firePortal1Button.SetActive(false);
            firePortal2Button.SetActive(false);
            firePortal3Button.SetActive(false);
            firePortal4Button.SetActive(false);
        }
    }
    public void OpenMeniuPortalFire()
    {
        if(firePortal1Button.activeInHierarchy == true)
        {
            firePortal1Button.SetActive(false);
            firePortal2Button.SetActive(false);
            firePortal3Button.SetActive(false);
            firePortal4Button.SetActive(false);
        }
        else
        {
            firePortal1Button.SetActive(true);
            firePortal2Button.SetActive(true);
            firePortal3Button.SetActive(true);
            firePortal4Button.SetActive(true);

            junglePortal1Button.SetActive(false);
            junglePortal2Button.SetActive(false);
            junglePortal3Button.SetActive(false);
            junglePortal4Button.SetActive(false);

            icePortal1Button.SetActive(false);
            icePortal2Button.SetActive(false);
            icePortal3Button.SetActive(false);
            icePortal4Button.SetActive(false);
        }
    }

    public void OpenMeniuPortalJungle()
    {
        if(junglePortal1Button.activeInHierarchy == true)
        {
            junglePortal1Button.SetActive(false);
            junglePortal2Button.SetActive(false);
            junglePortal3Button.SetActive(false);
            junglePortal4Button.SetActive(false);
        }
        else
        {
            junglePortal1Button.SetActive(true);
            junglePortal2Button.SetActive(true);
            junglePortal3Button.SetActive(true);
            junglePortal4Button.SetActive(true);

            firePortal1Button.SetActive(false);
            firePortal2Button.SetActive(false);
            firePortal3Button.SetActive(false);
            firePortal4Button.SetActive(false);

            icePortal1Button.SetActive(false);
            icePortal2Button.SetActive(false);
            icePortal3Button.SetActive(false);
            icePortal4Button.SetActive(false);
        }
    }



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