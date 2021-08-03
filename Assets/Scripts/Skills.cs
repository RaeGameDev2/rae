using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills
{
    public enum SkillType
    {
        BetterBlockingI,
        BetterBlockingII,
        MeleeChargedAttackI,
        MeleeChargedAttackII,
        StaffChargedAttackI,
        StaffChargedAttackII,
        ArcaneBolt,
        ArcaneBoltPlus,
        StunningSpell,
        EnchantWeapon,
        Invincibility,
        EmpoweredAttack,
        ImprovedDashing,
        AncientRecall,
        PotionAffinityI,
        PotionAffinityII,
        PotionAffinityIII
    }

    private List<SkillType> unlockedSkillsList;

    public Skills()
    {
        unlockedSkillsList = new List<SkillType>();
    }

    public void UnlockSkill(SkillType skillType)
    {
        unlockedSkillsList.Add(skillType);
    }

    public bool IsSkillUnlocked(SkillType skillType)
    {
        return unlockedSkillsList.Contains(skillType);
    }

}
