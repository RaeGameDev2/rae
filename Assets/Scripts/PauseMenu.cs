using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;

    [SerializeField] GameObject settingsMenu;

    public void Pause() {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;

    }

    public void Resume() {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }
    
    public void BackToHub(int sceneID) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void Settings() {
        pauseButton.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Back() {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
