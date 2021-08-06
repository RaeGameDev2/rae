using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool pause;
    private UI_Manager uiManager;

    void Start()
    {
        uiManager = FindObjectOfType<UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            uiManager.Pause();
        }
    }
}
