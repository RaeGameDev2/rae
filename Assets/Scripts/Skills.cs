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

    private int[] SkillLevel;
    private const int MAX_LEVEL = 4;
    private Resources resources;

    public Skills()
    {
        SkillLevel = new int[(int)SkillType.Size];
        LoadSkillLevels();
        resources = GameObject.FindGameObjectWithTag("Player").GetComponent<Resources>();
    }

    private void LoadSkillLevels()
    {
        SkillLevel[(int)SkillType.Parry] = PlayerPrefs.GetInt("ParryLevel");
        SkillLevel[(int)SkillType.LifeDrain] = PlayerPrefs.GetInt("LifeDrainLevel");
        SkillLevel[(int)SkillType.QuickTP] = PlayerPrefs.GetInt("QuickTPLevel");
        SkillLevel[(int)SkillType.Shield] = PlayerPrefs.GetInt("ShieldLevel");
        SkillLevel[(int)SkillType.PhaseWalk] = PlayerPrefs.GetInt("PhaseWalkLevel");
        SkillLevel[(int)SkillType.Debuff] = PlayerPrefs.GetInt("DebuffLevel");
        SkillLevel[(int)SkillType.Attack] = PlayerPrefs.GetInt("AttackLevel");
        SkillLevel[(int)SkillType.CritBonus] = PlayerPrefs.GetInt("CritBonusLevel");
        SkillLevel[(int)SkillType.CritRate] = PlayerPrefs.GetInt("CritRateLevel");
        SkillLevel[(int)SkillType.AttackSpeed] = PlayerPrefs.GetInt("AttackSpeedLevel");
        SkillLevel[(int)SkillType.Life] = PlayerPrefs.GetInt("LifeLevel");
        SkillLevel[(int)SkillType.Mana] = PlayerPrefs.GetInt("ManaLevel");
    }

    private void SaveSkillLevels()
    {
        PlayerPrefs.SetInt("ParryLevel", SkillLevel[(int)SkillType.Parry]);
        PlayerPrefs.SetInt("LifeDrainLevel", SkillLevel[(int)SkillType.LifeDrain]);
        PlayerPrefs.SetInt("QuickTPLevel", SkillLevel[(int)SkillType.QuickTP]);
        PlayerPrefs.SetInt("ShieldLevel", SkillLevel[(int)SkillType.Shield]);
        PlayerPrefs.SetInt("PhaseWalkLevel", SkillLevel[(int)SkillType.PhaseWalk]);
        PlayerPrefs.SetInt("DebuffLevel", SkillLevel[(int)SkillType.Debuff]);
        PlayerPrefs.SetInt("AttackLevel", SkillLevel[(int)SkillType.Attack]);
        PlayerPrefs.SetInt("CritBonusLevel", SkillLevel[(int)SkillType.CritBonus]);
        PlayerPrefs.SetInt("CritRateLevel", SkillLevel[(int)SkillType.CritRate]);
        PlayerPrefs.SetInt("AttackSpeedLevel", SkillLevel[(int)SkillType.AttackSpeed]);
        PlayerPrefs.SetInt("LifeLevel", SkillLevel[(int)SkillType.Life]);
        PlayerPrefs.SetInt("ManaLevel", SkillLevel[(int)SkillType.Mana]);
    }

    private void ResetSkillLevels() // In caz ca e nevoie
    {
        int i = 0;
        for(i = 0; i < (int)SkillType.Size; i++)
        {
            SkillLevel[i] = 0;
        }
    }

    public void UpgradeSkill(SkillType skillType)
    {
        if (SkillLevel[(int)skillType] == MAX_LEVEL) return;
        if (resources.skillPoints == 0) return;
        if (skillType == SkillType.Parry && SkillLevel[(int)SkillType.LifeDrain] > 0) return;
        if (skillType == SkillType.LifeDrain && SkillLevel[(int)SkillType.Parry] > 0) return;
        if (skillType == SkillType.QuickTP && SkillLevel[(int)SkillType.Shield] > 0) return;
        if (skillType == SkillType.Shield && SkillLevel[(int)SkillType.QuickTP] > 0) return;
        if (skillType == SkillType.PhaseWalk && SkillLevel[(int)SkillType.Debuff] > 0) return;
        if (skillType == SkillType.Debuff && SkillLevel[(int)SkillType.PhaseWalk] > 0) return;
        SkillLevel[(int)skillType]++;
        resources.skillPoints--;
        SaveSkillLevels();
    }

    public int GetSkillPoints()
    {
        return resources.skillPoints;
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return SkillLevel[(int)skillType] > 0;
    }

    public int GetSkillLevel(SkillType skillType)
    {
        return SkillLevel[(int)skillType];
    }

    public int GetMaxLevel()
    {
        return MAX_LEVEL;
    }

}
