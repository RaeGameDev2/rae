using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject settingsMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        GameManager.instance.Pause();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        GameManager.instance.Pause();
    }

    public void BackToHub()
    {
        Resume();
        GameManager.instance.Pause();
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Back()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
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
        GameManager.instance.ResetAllData();
        SceneManager.LoadScene(1);
    }
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!pauseMenu.activeSelf && !settingsMenu.activeSelf)
            {
                Pause();
            }
        }
    }
}