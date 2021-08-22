using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject settingsMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void BackToHub()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        pauseButton.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Back()
    {
        pauseMenu.SetActive(true);
        isPaused = true;
        settingsMenu.SetActive(false);
    }

    private void Update()
    {
        if (!Input.GetKeyDown("escape")) return;
        if (isPaused == false)
            Back();
        else
            Resume();
    }
}