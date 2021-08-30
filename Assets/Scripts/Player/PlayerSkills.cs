using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public Skills playerSkills;

    private void Awake()
    {
        playerSkills = new Skills();
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