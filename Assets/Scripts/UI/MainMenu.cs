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

    [SerializeField] GameObject informationButton;
    [SerializeField] GameObject informationMenu;

    [SerializeField] GameObject weaponsMenu;

    [SerializeField] GameObject noteMenu;
    [SerializeField] GameObject noteMenu1;
    [SerializeField] GameObject noteMenu2;
    [SerializeField] GameObject noteMenu3;
    [SerializeField] GameObject noteMenu4;
    [SerializeField] GameObject noteMenu5;
    [SerializeField] GameObject noteMenu6;

    private bool isPaused = false;
    private bool isInfo = false;

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
        if (isInfo)
        {
            informationMenu.SetActive(true);
            weaponsMenu.SetActive(false);
            noteMenu.SetActive(false);
            return;
        }

        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        informationButton.SetActive(false);
        Time.timeScale = 0f;

    }

    public void Resume()
    {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        informationButton.SetActive(true);
        Time.timeScale = 1f;

        isPaused = false;
        isInfo = false;
    }

    public void BackToHub()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(hubSceneID);
    }

    public void Information()
    {
        isInfo = true;
        informationMenu.SetActive(true);
        informationButton.SetActive(false);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;

    }

    public void Note()
    {
        informationButton.SetActive(false);
        pauseButton.SetActive(false);
        informationMenu.SetActive(false);
        noteMenu.SetActive(true);
        noteMenu1.SetActive(true);
        noteMenu2.SetActive(false);
        noteMenu3.SetActive(false);
        noteMenu4.SetActive(false);
        noteMenu5.SetActive(false);
        noteMenu6.SetActive(false);
    }

    public void Note1()
    {
        informationButton.SetActive(false);
        pauseButton.SetActive(false);
        informationMenu.SetActive(false);

        noteMenu1.SetActive(false);
        noteMenu2.SetActive(true);
        noteMenu3.SetActive(false);
        noteMenu4.SetActive(false);
        noteMenu5.SetActive(false);
        noteMenu6.SetActive(false);
    }
    public void Note2()
    {
        informationButton.SetActive(false);
        pauseButton.SetActive(false);
        informationMenu.SetActive(false);
        noteMenu1.SetActive(false);
        noteMenu2.SetActive(false);
        noteMenu3.SetActive(true);
        noteMenu4.SetActive(false);
        noteMenu5.SetActive(false);
        noteMenu6.SetActive(false);
    }
    public void Note3()
    {
        informationButton.SetActive(false);
        pauseButton.SetActive(false);
        informationMenu.SetActive(false);
        noteMenu1.SetActive(false);
        noteMenu2.SetActive(false);
        noteMenu3.SetActive(false);
        noteMenu4.SetActive(true);
        noteMenu5.SetActive(false);
        noteMenu6.SetActive(false);
    }
    public void Note4()
    {
        informationButton.SetActive(false);
        pauseButton.SetActive(false);
        informationMenu.SetActive(false);
        noteMenu1.SetActive(false);
        noteMenu2.SetActive(false);
        noteMenu3.SetActive(false);
        noteMenu4.SetActive(false);
        noteMenu5.SetActive(true);
        noteMenu6.SetActive(false);
    }
    public void Note5()
    {
        informationButton.SetActive(false);
        pauseButton.SetActive(false);
        informationMenu.SetActive(false);
        noteMenu1.SetActive(false);
        noteMenu2.SetActive(false);
        noteMenu3.SetActive(false);
        noteMenu4.SetActive(false);
        noteMenu5.SetActive(false);
        noteMenu6.SetActive(true);
    }

    public void InformationResume()
    {
        informationButton.SetActive(true);
        informationMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;

    }
    public void Weapons()
    {
        informationButton.SetActive(false);
        informationMenu.SetActive(false);
        weaponsMenu.SetActive(true);
    }

}
