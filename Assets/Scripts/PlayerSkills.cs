using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private UI_SkillTree uiSkillTree;
    private Skills playerSkills;

    private GameObject Skilltree;
    private GameObject ShowButton;

    private void Start()
    {
        playerSkills = new Skills();
        uiSkillTree = GameObject.Find("UI_Skilltree").GetComponent<UI_SkillTree>();
        uiSkillTree.SetPlayerSkills(playerSkills);

        Skilltree = GameObject.Find("UI_Skilltree");
        ShowButton = GameObject.Find("Show_button");

        Button Exit_btn = GameObject.Find("Exit_button").GetComponent<Button>();
        Exit_btn.onClick.AddListener(HideUI);


        Button Show_btn = ShowButton.GetComponent<Button>();
        Show_btn.onClick.AddListener(ShowUI);

        HideUI();
    }

    private void HideUI()
    {
        ShowButton.SetActive(true);
        Skilltree.SetActive(false);
    }

    private void ShowUI()
    {
        Skilltree.SetActive(true);
        ShowButton.SetActive(false);
    }
    
    public bool IsLifeDrainUnlocked()
    {
        return playerSkills.IsSkillUnlocked(Skills.SkillType.LifeDrain);
    }
    public bool IsParryUnlocked()
    {
        return playerSkills.IsSkillUnlocked(Skills.SkillType.Parry);
    }
    public bool IsQuickTpUnlocked()
    {
        return playerSkills.IsSkillUnlocked(Skills.SkillType.QuickTP);
    }
    public bool IsShieldUnlocked()
    {
        return playerSkills.IsSkillUnlocked(Skills.SkillType.Shield);
    }
    public bool IsPhaseWalkUnlocked()
    {
        return playerSkills.IsSkillUnlocked(Skills.SkillType.PhaseWalk);
    }
    public bool IsDebuffUnlocked()
    {
        return playerSkills.IsSkillUnlocked(Skills.SkillType.Debuff);
    }
    public int GetLevelParry()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.Parry);
    }
    public int GetLevelLifeDrain()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.LifeDrain);
    }
    public int GetLevelQuickTP()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.QuickTP);
    }
    public int GetLevelShield()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.Shield);
    }
    public int GetLevelPhaseWalk()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.PhaseWalk);
    }
    public int GetLevelDebuff()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.Debuff);
    }
    public int GetLevelAttack()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.Attack);
    }
    public int GetLevelCritBonus()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.CritBonus);
    }
    public int GetLevelCritRate()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.CritRate);
    }
    public int GetLevelAttackSpeed()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.AttackSpeed);
    }
    public int GetLevelLife()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.Life);
    }
    public int GetLevelMana()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.Mana);
    }
}
