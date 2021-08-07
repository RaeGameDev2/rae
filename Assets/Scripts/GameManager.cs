using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool pause;
    private UI_Manager uiManager;
    private Resources playerResources;
    private PlayerSkills playerSkills;
    private PlayerSpells playerSpells;

    private void Start()
    {
        uiManager = FindObjectOfType<UI_Manager>();
        playerResources = FindObjectOfType<Resources>();
        playerSkills = FindObjectOfType<PlayerSkills>();
        playerSpells = FindObjectOfType<PlayerSpells>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            uiManager.Pause();
        }

        // For Testing

    }
}
