using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Animator animator;
    private GameObject fireButton;
    private GameObject iceButton;
    private GameObject natureButton;
    private GameObject interiorPortal;
    public float transitionTime = 1f;

    private void Start()
    {
        fireButton = GameObject.Find("Fire_Button");
        iceButton = GameObject.Find("Ice_Button");
        natureButton = GameObject.Find("Nature_Button");
        interiorPortal = GameObject.Find("Interior Portal");
        fireButton.SetActive(false);
        iceButton.SetActive(false);
        natureButton.SetActive(false);
        interiorPortal.SetActive(false);
    }

    public void Fire_Load()
    {
        SceneManager.LoadScene(2);
        /*LoadNextLevel(2);*/
    }

    public void Ice_Load()
    {
        SceneManager.LoadScene(3);
        //LoadNextLevel(3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        fireButton.SetActive(true);
        iceButton.SetActive(true);
        natureButton.SetActive(true);
        interiorPortal.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        fireButton.SetActive(false);
        iceButton.SetActive(false);
        natureButton.SetActive(false);
        interiorPortal.SetActive(false);
    }

    public void LoadNextLevel(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}