using System.Collections;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportMenu : MonoBehaviour
{
    private GameManager gameManager;

    private GameObject icePortal1;
    private GameObject icePortal2;
    private GameObject icePortal3;
    private GameObject icePortal4;
    private GameObject player;

    [SerializeField] private GameObject icePortal1Button;
    [SerializeField] private GameObject icePortal2Button;
    [SerializeField] private GameObject icePortal3Button;
    [SerializeField] private GameObject icePortal4Button;

    [SerializeField] private GameObject icePortal2Blocked;
    [SerializeField] private GameObject icePortal3Blocked;
    [SerializeField] private GameObject icePortal4Blocked;

    [SerializeField] private GameObject firePortal1Button;
    [SerializeField] private GameObject firePortal2Button;
    [SerializeField] private GameObject firePortal3Button;
    [SerializeField] private GameObject firePortal4Button;

    [SerializeField] private GameObject firePortal2Blocked;
    [SerializeField] private GameObject firePortal3Blocked;
    [SerializeField] private GameObject firePortal4Blocked;

    [SerializeField] private GameObject junglePortal1Button;
    [SerializeField] private GameObject junglePortal2Button;
    [SerializeField] private GameObject junglePortal3Button;
    [SerializeField] private GameObject junglePortal4Button;

    [SerializeField] private GameObject junglePortal2Blocked;
    [SerializeField] private GameObject junglePortal3Blocked;
    [SerializeField] private GameObject junglePortal4Blocked;

    public void Start()
    {
        gameManager = GameManager.instance;
        icePortal1Button.SetActive(false);
        icePortal2Button.SetActive(false);
        icePortal3Button.SetActive(false);
        icePortal4Button.SetActive(false);
        icePortal2Blocked.SetActive(false);
        icePortal3Blocked.SetActive(false);
        icePortal4Blocked.SetActive(false);

        junglePortal1Button.SetActive(false);
        junglePortal2Button.SetActive(false);
        junglePortal3Button.SetActive(false);
        junglePortal4Button.SetActive(false);
        junglePortal2Blocked.SetActive(false);
        junglePortal3Blocked.SetActive(false);
        junglePortal4Blocked.SetActive(false);

        firePortal1Button.SetActive(false);
        firePortal2Button.SetActive(false);
        firePortal3Button.SetActive(false);
        firePortal4Button.SetActive(false);
        firePortal2Blocked.SetActive(false);
        firePortal3Blocked.SetActive(false);
        firePortal4Blocked.SetActive(false);
    }

    public void OpenMeniuPortalIce()
    {

        if (icePortal1Button.activeInHierarchy == true)
        {
            icePortal1Button.SetActive(false);
            icePortal2Button.SetActive(false);
            icePortal3Button.SetActive(false);
            icePortal4Button.SetActive(false);
            icePortal2Blocked.SetActive(false);
            icePortal3Blocked.SetActive(false);
            icePortal4Blocked.SetActive(false);
        }
        else
        {
            icePortal1Button.SetActive(true);
            if (gameManager.checkpoints[GameManager.Realm.Ice][1] == true)
            {
                icePortal2Button.SetActive(true);
                icePortal2Blocked.SetActive(false);
            }
            else
            {
                icePortal2Button.SetActive(false);
                icePortal2Blocked.SetActive(true);
            }

            if (gameManager.checkpoints[GameManager.Realm.Ice][2] == true)
            {
                icePortal3Button.SetActive(true);
                icePortal3Blocked.SetActive(false);
            }
            else
            {
                icePortal3Button.SetActive(false);
                icePortal3Blocked.SetActive(true);
            }

            if (gameManager.checkpoints[GameManager.Realm.Ice][3] == true)
            {
                icePortal4Button.SetActive(true);
                icePortal4Blocked.SetActive(false);
            }
            else
            {
                icePortal4Button.SetActive(false);
                icePortal4Blocked.SetActive(true);
            }

            junglePortal1Button.SetActive(false);
            junglePortal2Button.SetActive(false);
            junglePortal3Button.SetActive(false);
            junglePortal4Button.SetActive(false);
            junglePortal2Blocked.SetActive(false);
            junglePortal3Blocked.SetActive(false);
            junglePortal4Blocked.SetActive(false);

            firePortal1Button.SetActive(false);
            firePortal2Button.SetActive(false);
            firePortal3Button.SetActive(false);
            firePortal4Button.SetActive(false);
            firePortal2Blocked.SetActive(false);
            firePortal3Blocked.SetActive(false);
            firePortal4Blocked.SetActive(false);
        }
    }
    public void OpenMeniuPortalFire()
    {
        if (firePortal1Button.activeInHierarchy == true)
        {
            firePortal1Button.SetActive(false);
            firePortal2Button.SetActive(false);
            firePortal3Button.SetActive(false);
            firePortal4Button.SetActive(false);
            firePortal2Blocked.SetActive(false);
            firePortal3Blocked.SetActive(false);
            firePortal4Blocked.SetActive(false);
        }
        else
        {
            firePortal1Button.SetActive(true);
            if (gameManager.checkpoints[GameManager.Realm.Fire][1] == true)
            {
                firePortal2Button.SetActive(true);
                firePortal2Blocked.SetActive(false);
            }
            else
            {
                firePortal2Button.SetActive(false);
                firePortal2Blocked.SetActive(true);
            }

            if (gameManager.checkpoints[GameManager.Realm.Fire][2] == true)
            {
                firePortal3Button.SetActive(true);
                firePortal3Blocked.SetActive(false);
            }
            else
            {
                firePortal3Button.SetActive(false);
                firePortal3Blocked.SetActive(true);
            }

            if (gameManager.checkpoints[GameManager.Realm.Fire][3] == true)
            {
                firePortal4Button.SetActive(true);
                firePortal4Blocked.SetActive(false);
            }
            else
            {
                firePortal4Button.SetActive(false);
                firePortal4Blocked.SetActive(true);
            }

            junglePortal1Button.SetActive(false);
            junglePortal2Button.SetActive(false);
            junglePortal3Button.SetActive(false);
            junglePortal4Button.SetActive(false);
            junglePortal2Blocked.SetActive(false);
            junglePortal3Blocked.SetActive(false);
            junglePortal4Blocked.SetActive(false);

            icePortal1Button.SetActive(false);
            icePortal2Button.SetActive(false);
            icePortal3Button.SetActive(false);
            icePortal4Button.SetActive(false);
            icePortal2Blocked.SetActive(false);
            icePortal3Blocked.SetActive(false);
            icePortal4Blocked.SetActive(false);

        }
    }

    public void OpenMeniuPortalJungle()
    {
        if (junglePortal1Button.activeInHierarchy == true)
        {
            junglePortal1Button.SetActive(false);
            junglePortal2Button.SetActive(false);
            junglePortal3Button.SetActive(false);
            junglePortal4Button.SetActive(false);
            junglePortal2Blocked.SetActive(false);
            junglePortal3Blocked.SetActive(false);
            junglePortal4Blocked.SetActive(false);
        }
        else
        {
            junglePortal1Button.SetActive(true);
            if (gameManager.checkpoints[GameManager.Realm.Jungle][1] == true)
            {
                junglePortal2Button.SetActive(true);
                junglePortal2Blocked.SetActive(false);
            }
            else
            {
                junglePortal2Button.SetActive(false);
                junglePortal2Blocked.SetActive(true);
            }

            if (gameManager.checkpoints[GameManager.Realm.Jungle][2] == true)
            {
                junglePortal3Button.SetActive(true);
                junglePortal3Blocked.SetActive(false);
            }
            else
            {
                junglePortal3Button.SetActive(false);
                junglePortal3Blocked.SetActive(true);
            }

            if (gameManager.checkpoints[GameManager.Realm.Jungle][3] == true)
            {
                junglePortal4Button.SetActive(true);
                junglePortal4Blocked.SetActive(false);
            }
            else
            {
                junglePortal4Button.SetActive(false);
                junglePortal4Blocked.SetActive(true);
            }

            firePortal1Button.SetActive(false);
            firePortal2Button.SetActive(false);
            firePortal3Button.SetActive(false);
            firePortal4Button.SetActive(false);
            firePortal2Blocked.SetActive(false);
            firePortal3Blocked.SetActive(false);
            firePortal4Blocked.SetActive(false);

            icePortal1Button.SetActive(false);
            icePortal2Button.SetActive(false);
            icePortal3Button.SetActive(false);
            icePortal4Button.SetActive(false);
            icePortal2Blocked.SetActive(false);
            icePortal3Blocked.SetActive(false);
            icePortal4Blocked.SetActive(false);
        }
    }

    public void FireCheckpoint(int id)
    {
        gameManager.ChangeCheckpointId(id, 2);
    }
    public void IceCheckpoint(int id)
    {
        gameManager.ChangeCheckpointId(id, 3);
    }
    public void JungleCheckpoint(int id)
    {
        gameManager.ChangeCheckpointId(id, 4);
    }

    public void Hub()
    {
        gameManager.ChangeCheckpointId(0, 1);
    }

    /*public void ResetCheckpoints()
    {
        string skil_file_path = Application.persistentDataPath + "/skill.data";
        string checkpoints_file_path = Application.persistentDataPath + "/checkpoints.data";
        if(File.Exists(checkpoints_file_path))
        {
            File.Delete(checkpoints_file_path);
        }
        if (File.Exists(skil_file_path))
        {
            File.Delete(skil_file_path);
        }
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
        #else
            Application.Quit();
        #endif
    }*/
}