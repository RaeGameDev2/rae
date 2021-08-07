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
        if (Input.GetKeyDown(KeyCode.Alpha3))
            playerResources.AddLife();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            playerResources.AddMana();
        if (Input.GetKeyDown(KeyCode.Alpha5))
            playerResources.AddSkillPoint();
        if (Input.GetKeyDown(KeyCode.Alpha6))
            playerSpells.Shockwave();
        if (Input.GetKeyDown(KeyCode.Alpha7))
            playerResources.TakeDamage(1);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            playerResources.UseMana();
    }
}
