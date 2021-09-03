using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int hubSceneID;
    [SerializeField] int playSceneID;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;


    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseButton;

    private bool isPaused = false;

    public IEnumerator loadSceneAsync(int id)
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);

        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;

        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.7f);
                Destroy(GameObject.FindObjectOfType<AudioListener>());
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
        unloadScene();
    }

    public void unloadScene()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.UnloadSceneAsync(0);
    }

    public void Play()
    {
        hubSceneID = 1;
        playSceneID = 1;
        foreach (Transform child in transform)
        {
            foreach (Transform nephew in child)
            {
                nephew.GetComponent<TweenText>().FadeOut();
            }
        }
        StartCoroutine(loadSceneAsync(playSceneID));
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void SettingsMain()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void SettingPause()
    {
        isPaused = true;
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Back()
    {

        if (isPaused)
        {
            pauseMenu.SetActive(true);
            settingsMenu.SetActive(false);
            return;
        }
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

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
        SceneManager.LoadScene(hubSceneID);
    }
}
