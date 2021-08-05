using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
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
    private const int MAX_LEVEL = 10;

    public Skills()
    {
        SkillLevel = new int[(int)SkillType.Size];
    }

    public void UpgradeSkill(SkillType skillType)
    {
        if (SkillLevel[(int)skillType] == MAX_LEVEL) return;
        if (GameObject.Find("Player").GetComponent<Resources>().skillPoints == 0) return;
        if (skillType == SkillType.Parry && SkillLevel[(int)SkillType.LifeDrain] > 0) return;
        if (skillType == SkillType.LifeDrain && SkillLevel[(int)SkillType.Parry] > 0) return;
        if (skillType == SkillType.QuickTP && SkillLevel[(int)SkillType.Shield] > 0) return;
        if (skillType == SkillType.Shield && SkillLevel[(int)SkillType.QuickTP] > 0) return;
        if (skillType == SkillType.PhaseWalk && SkillLevel[(int)SkillType.Debuff] > 0) return;
        if (skillType == SkillType.Debuff && SkillLevel[(int)SkillType.PhaseWalk] > 0) return;
        SkillLevel[(int)skillType]++;
        GameObject.Find("Player").GetComponent<Resources>().skillPoints--;
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
        return GameObject.Find("Player").GetComponent<Resources>().skillPoints;
    }

    public void SetSkillPoints(int sp)
    {
        GameObject.Find("Player").GetComponent<Resources>().skillPoints = sp;
    }

    public int GetMaxLevel()
    {
        return MAX_LEVEL;
    }

}
