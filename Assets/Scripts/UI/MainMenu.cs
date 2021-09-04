using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int hubSceneID;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;

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
                yield return new WaitForSeconds(1f);
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
        SceneManager.LoadScene(hubSceneID);
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        Debug.Log("XD");
    }

    public void Back()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
