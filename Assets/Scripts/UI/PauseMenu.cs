using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject resetdataButton;

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
        resetdataButton.SetActive(false);
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
        resetdataButton.SetActive(true);
    }

    public void ResetPersistentData()
    {
        string skill_file_path = Application.persistentDataPath + "/skill.data";
        string skilpoints_file_path = Application.persistentDataPath + "/skillpoints.data";
        string checkpoints_file_path = Application.persistentDataPath + "/checkpoints.data";
        if (File.Exists(checkpoints_file_path))
        {
            File.Delete(checkpoints_file_path);
        }
        if (File.Exists(skill_file_path))
        {
            File.Delete(skill_file_path);
        }
        if (File.Exists(skilpoints_file_path))
        {
            File.Delete(skilpoints_file_path);
        }
        SceneManager.LoadScene(1);
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