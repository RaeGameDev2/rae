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
        Life,
        Mana,
        Size
    }

    private const int MAX_LEVEL = 4;
    private PlayerResources playerResources;

    private int[] skillLevel;

    public Skills()
    {
        skillLevel = new int[(int) SkillType.Size];
        playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
    }

    public void UpgradeSkill(SkillType skillType)
    {
        if (skillLevel[(int) skillType] == MAX_LEVEL) return;
        if (playerResources.skillPoints == 0) return;
        if (skillType == SkillType.Parry && skillLevel[(int) SkillType.LifeDrain] > 0) return;
        if (skillType == SkillType.LifeDrain && skillLevel[(int) SkillType.Parry] > 0) return;
        if (skillType == SkillType.QuickTP && skillLevel[(int) SkillType.Shield] > 0) return;
        if (skillType == SkillType.Shield && skillLevel[(int) SkillType.QuickTP] > 0) return;
        if (skillType == SkillType.PhaseWalk && skillLevel[(int) SkillType.Debuff] > 0) return;
        if (skillType == SkillType.Debuff && skillLevel[(int) SkillType.PhaseWalk] > 0) return;
        skillLevel[(int) skillType]++;

        var gameManager = GameManager.instance;
        System.Array.Copy(skillLevel, GameManager.instance.skillLevel, skillLevel.Length);
        gameManager.SaveSkillLevel();
        
        playerResources.skillPoints--;

        gameManager.SaveSkillPoints();
    }

    public int GetSkillPoints()
    {
        return playerResources.skillPoints;
    }

    public void SetSkillPoints(int skillPoints)
    {
        playerResources.skillPoints = skillPoints;
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return skillLevel[(int) skillType] > 0;
    }

    public int GetSkillLevel(SkillType skillType)
    {
        return skillLevel[(int) skillType];
    }

    public int GetMaxLevel()
    {
        return MAX_LEVEL;
    }

    public void SetSkillLevel()
    {
        System.Array.Copy(GameManager.instance.skillLevel, skillLevel, GameManager.instance.skillLevel.Length);
    }
}