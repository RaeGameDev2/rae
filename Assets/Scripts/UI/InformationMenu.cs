
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InformationMenu : MonoBehaviour
{
    [SerializeField] GameObject InformationButton;
    [SerializeField] GameObject InformationnMenu;

    [SerializeField] GameObject WeaponsMenu;
    [SerializeField] GameObject NoteMenu;

    [SerializeField] GameObject PauseButton;


    public void Information()
    {
        InformationnMenu.SetActive(true);
        InformationButton.SetActive(false);
         PauseButton.SetActive(false);
        Time.timeScale = 0f;

    }

    public void InformationResume()
    {
        InformationButton.SetActive(true);
        InformationnMenu.SetActive(false);
        PauseButton.SetActive(true);
        Time.timeScale = 1f;

    }

    public void BackToHub(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }
    public void Back()
    {
        InformationnMenu.SetActive(true);
        WeaponsMenu.SetActive(false);
        NoteMenu.SetActive(false);
    }

    public void Weapons()
    {
        InformationButton.SetActive(false);
        InformationnMenu.SetActive(false);
        WeaponsMenu.SetActive(true);
    }

    public void InformationBackToHub(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void Note()
    {
        InformationButton.SetActive(false);
        PauseButton.SetActive(false);
        InformationnMenu.SetActive(false);
        NoteMenu.SetActive(true);
    }
}

