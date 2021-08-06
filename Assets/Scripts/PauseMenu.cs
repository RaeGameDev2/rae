using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject Panel;
    // Start is called before the first frame update
    public void Pause() {
        Panel.SetActive(true);
        pauseButton.SetActive(false);
       // Time.timeScale = 0f;

    }

    public void Resume() {
        pauseButton.SetActive(true);
        Panel.SetActive(false);
        Time.timeScale = 1f;

    }
    
    public void BackToHub(int sceneID) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void Settings() {
        pauseButton.SetActive(false);
        Panel.SetActive(false);
        // TO DO incarca meniul setari
    }
}
