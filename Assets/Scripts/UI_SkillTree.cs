using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTree : MonoBehaviour
{
    public Skills playerSkills;

    public Button Parry_button;
    public Button LifeDrain_button;
    public Button QuickTP_button;
    public Button Shield_button;
    public Button Debuff_button;
    public Button PhaseWalk_button;
    public Button Attack_button;
    public Button CritBonus_button;
    public Button CritRate_button;
    public Button AttackSpeed_button;
    public Button Defense_button;

    void Start()
    {
        Button Parry_btn = Parry_button.GetComponent<Button>();
        Parry_btn.onClick.AddListener(Parry_Clicked);

        Button LifeDrain_btn = LifeDrain_button.GetComponent<Button>();
        LifeDrain_btn.onClick.AddListener(LifeDrain_Clicked);

        Button QuickTP_btn = QuickTP_button.GetComponent<Button>();
        QuickTP_btn.onClick.AddListener(QuickTP_Clicked);

        Button Shield_btn = Shield_button.GetComponent<Button>();
        Shield_btn.onClick.AddListener(Shield_Clicked);

        Button Debuff_btn = Debuff_button.GetComponent<Button>();
        Debuff_btn.onClick.AddListener(Debuff_Clicked);

        Button PhaseWalk_btn = PhaseWalk_button.GetComponent<Button>();
        PhaseWalk_btn.onClick.AddListener(PhaseWalk_Clicked);

        Button Attack_btn = Attack_button.GetComponent<Button>();
        Attack_btn.onClick.AddListener(Attack_Clicked);

        Button CritBonus_btn = CritBonus_button.GetComponent<Button>();
        CritBonus_btn.onClick.AddListener(CritBonus_Clicked);

        Button CritRate_btn = CritRate_button.GetComponent<Button>();
        CritRate_btn.onClick.AddListener(CritRate_Clicked);

        Button AttackSpeed_btn = AttackSpeed_button.GetComponent<Button>();
        AttackSpeed_btn.onClick.AddListener(AttackSpeed_Clicked);

        Button Defense_btn = Defense_button.GetComponent<Button>();
        Defense_btn.onClick.AddListener(Defense_Clicked);
    }

    public void UpdateUI()
    {
        if (playerSkills.GetSkillLevel(Skills.SkillType.Parry) > 0)
        {
            Parry_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
            Parry_button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Parry\n(" + playerSkills.GetSkillLevel(Skills.SkillType.Parry) + ")";
        }

        if (playerSkills.GetSkillLevel(Skills.SkillType.LifeDrain) > 0)
        {
            LifeDrain_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
            LifeDrain_button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Life Drain\n(" + playerSkills.GetSkillLevel(Skills.SkillType.LifeDrain) + ")";
        }

        if (playerSkills.GetSkillLevel(Skills.SkillType.QuickTP) > 0)
        {
            QuickTP_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
            QuickTP_button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Quick TP\n(" + playerSkills.GetSkillLevel(Skills.SkillType.QuickTP) + ")";
        }

        if (playerSkills.GetSkillLevel(Skills.SkillType.Shield) > 0)
        {
            Shield_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
            Shield_button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Shield\n(" + playerSkills.GetSkillLevel(Skills.SkillType.Shield) + ")";
        }

        if (playerSkills.GetSkillLevel(Skills.SkillType.Debuff) > 0)
        {
            Debuff_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
            Debuff_button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Debuff\n(" + playerSkills.GetSkillLevel(Skills.SkillType.Debuff) + ")";
        }

        if (playerSkills.GetSkillLevel(Skills.SkillType.PhaseWalk) > 0)
        {
            PhaseWalk_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
            PhaseWalk_button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Phase Walk\n(" + playerSkills.GetSkillLevel(Skills.SkillType.PhaseWalk) + ")";
        }

        if (playerSkills.GetSkillLevel(Skills.SkillType.Attack) > 0)
        {
            Attack_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
            Attack_button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Attack\n(" + playerSkills.GetSkillLevel(Skills.SkillType.Attack) + ")";
        }

        if (playerSkills.GetSkillLevel(Skills.SkillType.CritRate) > 0)
        {
            CritRate_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
            CritRate_button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Critical Rate\n(" + playerSkills.GetSkillLevel(Skills.SkillType.CritRate) + ")";
        }

        if (playerSkills.GetSkillLevel(Skills.SkillType.CritBonus) > 0)
        {
            CritBonus_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
            CritBonus_button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Critical Bonus\n(" + playerSkills.GetSkillLevel(Skills.SkillType.CritBonus) + ")";
        }

        if (playerSkills.GetSkillLevel(Skills.SkillType.AttackSpeed) > 0)
        {
            AttackSpeed_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
            AttackSpeed_button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Attack Speed\n(" + playerSkills.GetSkillLevel(Skills.SkillType.AttackSpeed) + ")";
        }

        if (playerSkills.GetSkillLevel(Skills.SkillType.Defense) > 0)
        {
            Defense_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
            Defense_button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Defense\n(" + playerSkills.GetSkillLevel(Skills.SkillType.Defense) + ")";
        }

    }

    public void SetPlayerSkills(Skills playerSkills)
    {
        this.playerSkills = playerSkills;
    }

    void Parry_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Parry);
        UpdateUI();
    }

    void LifeDrain_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.LifeDrain);
        UpdateUI();
    }

    void QuickTP_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.QuickTP);
        UpdateUI();
    }

    void Shield_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Shield);
        UpdateUI();
    }

    void Debuff_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Debuff);
        UpdateUI();
    }

    void PhaseWalk_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.PhaseWalk);
        UpdateUI();
    }

    void Attack_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Attack);
        UpdateUI();
    }

    void CritBonus_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.CritBonus);
        UpdateUI();
    }

    void CritRate_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.CritRate);
        UpdateUI();
    }

    void AttackSpeed_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.AttackSpeed);
        UpdateUI();
    }

    void Defense_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Defense);
        UpdateUI();
    }

}
