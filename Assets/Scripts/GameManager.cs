using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool pause;
    private UI_Manager uiManager;
    private Resources playerResources;
    private PlayerSkills playerSkills;
    private PlayerSpells playerSpells;
    private PlayerController playerController;
    private Weapons_Handler weaponsHandler;

    private void Start()
    {
        uiManager = FindObjectOfType<UI_Manager>();
        playerResources = FindObjectOfType<Resources>();
        playerSkills = FindObjectOfType<PlayerSkills>();
        playerSpells = FindObjectOfType<PlayerSpells>();
        playerController = FindObjectOfType<PlayerController>();
        weaponsHandler = FindObjectOfType<Weapons_Handler>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            uiManager.Pause();
            playerResources.Pause();
            playerSpells.Pause();
            playerSkills.Pause();
            playerController.Pause();
            weaponsHandler.Pause();
        }

        // For Testing
        if (pause) return;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            playerResources.AddLife();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            playerResources.AddMana();
        if (Input.GetKeyDown(KeyCode.Alpha5))
            playerResources.AddSkillPoint();
        if (Input.GetKeyDown(KeyCode.Alpha6))
            playerSpells.Shockwave();
        if (Input.GetKeyDown(KeyCode.Alpha7))
            playerResources.TakeDamage(1, Vector3.zero);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            playerResources.UseMana();
    }
}
