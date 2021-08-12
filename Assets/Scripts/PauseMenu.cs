using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pauseMenu;

    [SerializeField] GameObject settingsMenu;
    private bool isPaused = false;

    public void Pause() {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;

    }

    public void Resume() {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }
    
    public void BackToHub() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Settings() {
        pauseButton.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Back() {
        
            pauseMenu.SetActive(true);
            isPaused = true;
            settingsMenu.SetActive(false);
        
    }

    private new void Update()
    {
        if (Input.GetKeyDown("escape"))
        {

            if (isPaused == false)
            { 
                Back(); 
            } 
            else
            {
                Resume();
            }
        }
    }
}
