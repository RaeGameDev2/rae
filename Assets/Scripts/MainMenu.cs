using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;

    private void Awake()
    {
        Time.timeScale = 0f;
    }
    public void Play(int sceneID) {
        
        Time.timeScale = 1f;
         mainMenu.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }

    public void Quit() {
        Application.Quit();
    }

    public void Settings() {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Back() {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
