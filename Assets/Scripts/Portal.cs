using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections;

public class Portal : MonoBehaviour
{
    private GameObject fireButton;
    private GameObject iceButton;
    private GameObject interiorPortal;
    public Animator animator;
    public float transitionTime = 1f;

    private void Start()
    {
        fireButton = GameObject.Find("Fire_Button");
        iceButton = GameObject.Find("Ice_Button");
        interiorPortal = GameObject.Find("Interior Portal");
        fireButton.SetActive(false);
        iceButton.SetActive(false);
        interiorPortal.SetActive(false);
    }

    public void Fire_Load()
    {

        LoadNextLevel(2);
    }

    public void Ice_Load()
    {
        LoadNextLevel(3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        fireButton.SetActive(true);
        iceButton.SetActive(true);
        interiorPortal.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        fireButton.SetActive(false);
        iceButton.SetActive(false);
        interiorPortal.SetActive(false);
    }

    public void LoadNextLevel(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
    
}
