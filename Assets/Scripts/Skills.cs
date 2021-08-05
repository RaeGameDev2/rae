using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills
{
    public enum SkillType
    {
        Parry,
        LifeDrain,
        QuickTP,
        Shield,
        PhaseWalk,
        Debuff,
        Attack,
        CritBonus,
        CritRate,
        AttackSpeed,
        Defense,
        Size
    }

    private int[] SkillLevel;
    private int SkillPoints;
    private const int MAX_LEVEL = 10;

    public Skills()
    {
        SkillLevel = new int[(int)SkillType.Size];
        SkillPoints = 200;
    }

    public void UpgradeSkill(SkillType skillType)
    {
        if (SkillLevel[(int)skillType] == MAX_LEVEL) return;
        if (SkillPoints == 0) return;
        SkillLevel[(int)skillType]++;
        SkillPoints--;
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return SkillLevel[(int)skillType] > 0;
    }

    public int GetSkillLevel(SkillType skillType)
    {
        return SkillLevel[(int)skillType];
    }

    public int GetSkillPoints()
    {
        return SkillPoints;
    }

    public void SetSkillPoints(int sp)
    {
        SkillPoints = sp;
    }

    public int GetMaxLevel()
    {
        return MAX_LEVEL;
    }

}