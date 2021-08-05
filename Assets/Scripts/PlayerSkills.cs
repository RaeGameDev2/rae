using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private UI_SkillTree uiSkillTree;
    private Skills playerSkills;

    GameObject Skilltree;
    GameObject ShowButton;

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

    public void HideUI()
    {
        ShowButton.SetActive(true);
        Skilltree.SetActive(false);
    }

    public void ShowUI()
    {
        Skilltree.SetActive(true);
        ShowButton.SetActive(false);
    }

    //Exemplu:
    public bool IsLifeDrainUnlocked()
    {
        return playerSkills.IsSkillUnlocked(Skills.SkillType.LifeDrain);
    }
    public bool IsParryUnlocked()
    {
        return playerSkills.IsSkillUnlocked(Skills.SkillType.Parry);
    }
    public bool IsLifeQuickTpUnlocked()
    {
        return playerSkills.IsSkillUnlocked(Skills.SkillType.QuickTP);
    }
    public bool IsLifeShieldUnlocked()
    {
        return playerSkills.IsSkillUnlocked(Skills.SkillType.Shield);
    }
    public bool IsLifePhaseWalkUnlocked()
    {
        return playerSkills.IsSkillUnlocked(Skills.SkillType.PhaseWalk);
    }
    public bool IsLifeDebuffUnlocked()
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
    public int GetLevelDefense()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.Defense);
    }
    public int GetLevelSize()
    {
        return playerSkills.GetSkillLevel(Skills.SkillType.Size);
    }
}
