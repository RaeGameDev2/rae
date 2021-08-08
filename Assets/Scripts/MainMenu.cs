using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;

    public void Play(int sceneID) {
        SceneManager.LoadScene(sceneID);
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
