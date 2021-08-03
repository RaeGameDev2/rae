using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTree : MonoBehaviour
{
    public Skills playerSkills;

    public Button BetterBlockingI_button;
    public Button BetterBlockingII_button;
    public Button MeleeChargedAttackI_button;
    public Button MeleeChargedAttackII_button;
    public Button StaffChargedAttackI_button;
    public Button StaffChargedAttackII_button;
    public Button ArcaneBolt_button;
    public Button ArcaneBoltPlus_button;
    public Button StunningSpell_button;
    public Button EnchantWeapon_button;
    public Button Invincibility_button;
    public Button EmpoweredAttack_button;
    public Button ImprovedDashing_button;
    public Button AncientRecall_button;
    public Button PotionAffinityI_button;
    public Button PotionAffinityII_button;
    public Button PotionAffinityIII_button;

    void Start()
    {
        Button BetterBlockingI_btn = BetterBlockingI_button.GetComponent<Button>();
        BetterBlockingI_btn.onClick.AddListener(BetterBlockingI_Clicked);

        Button BetterBlockingII_btn = BetterBlockingII_button.GetComponent<Button>();
        BetterBlockingII_btn.onClick.AddListener(BetterBlockingII_Clicked);

        Button MeleeChargedAttackI_btn = MeleeChargedAttackI_button.GetComponent<Button>();
        MeleeChargedAttackI_btn.onClick.AddListener(MeleeChargedAttackI_Clicked);

        Button MeleeChargedAttackII_btn = MeleeChargedAttackII_button.GetComponent<Button>();
        MeleeChargedAttackII_btn.onClick.AddListener(MeleeChargedAttackII_Clicked);

        Button StaffChargedAttackI_btn = StaffChargedAttackI_button.GetComponent<Button>();
        StaffChargedAttackI_btn.onClick.AddListener(StaffChargedAttackI_Clicked);

        Button StaffChargedAttackII_btn = StaffChargedAttackII_button.GetComponent<Button>();
        StaffChargedAttackII_btn.onClick.AddListener(StaffChargedAttackII_Clicked);

        Button ArcaneBolt_btn = ArcaneBolt_button.GetComponent<Button>();
        ArcaneBolt_btn.onClick.AddListener(ArcaneBolt_Clicked);

        Button ArcaneBoltPlus_btn = ArcaneBoltPlus_button.GetComponent<Button>();
        ArcaneBoltPlus_btn.onClick.AddListener(ArcaneBoltPlus_Clicked);

        Button StunningSpell_btn = StunningSpell_button.GetComponent<Button>();
        StunningSpell_btn.onClick.AddListener(StunningSpell_Clicked);

        Button EnchantWeapon_btn = EnchantWeapon_button.GetComponent<Button>();
        EnchantWeapon_btn.onClick.AddListener(EnchantWeapon_Clicked);

        Button Invincibility_btn = Invincibility_button.GetComponent<Button>();
        Invincibility_btn.onClick.AddListener(Invincibility_Clicked);

        Button EmpoweredAttack_btn = EmpoweredAttack_button.GetComponent<Button>();
        EmpoweredAttack_btn.onClick.AddListener(EmpoweredAttack_Clicked);

        Button ImprovedDashing_btn = ImprovedDashing_button.GetComponent<Button>();
        ImprovedDashing_btn.onClick.AddListener(ImprovedDashing_Clicked);

        Button AncientRecall_btn = AncientRecall_button.GetComponent<Button>();
        AncientRecall_btn.onClick.AddListener(AncientRecall_Clicked);

        Button PotionAffinityI_btn = PotionAffinityI_button.GetComponent<Button>();
        PotionAffinityI_btn.onClick.AddListener(PotionAffinityI_Clicked);

        Button PotionAffinityII_btn = PotionAffinityII_button.GetComponent<Button>();
        PotionAffinityII_btn.onClick.AddListener(PotionAffinityII_Clicked);

        Button PotionAffinityIII_btn = PotionAffinityIII_button.GetComponent<Button>();
        PotionAffinityIII_btn.onClick.AddListener(PotionAffinityIII_Clicked);
    }

    public void SetPlayerSkills(Skills playerSkills)
    {
        this.playerSkills = playerSkills;
    }

    void BetterBlockingI_Clicked()
    {
        if(!playerSkills.IsSkillUnlocked(Skills.SkillType.BetterBlockingI))
        {
            playerSkills.UnlockSkill(Skills.SkillType.BetterBlockingI);
            BetterBlockingI_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void BetterBlockingII_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.BetterBlockingI)) return;
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.BetterBlockingII))
        {
            playerSkills.UnlockSkill(Skills.SkillType.BetterBlockingII);
            BetterBlockingII_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void MeleeChargedAttackI_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.MeleeChargedAttackI))
        {
            playerSkills.UnlockSkill(Skills.SkillType.MeleeChargedAttackI);
            MeleeChargedAttackI_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void MeleeChargedAttackII_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.MeleeChargedAttackI)) return;
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.MeleeChargedAttackII))
        {
            playerSkills.UnlockSkill(Skills.SkillType.MeleeChargedAttackII);
            MeleeChargedAttackII_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void StaffChargedAttackI_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.StaffChargedAttackI))
        {
            playerSkills.UnlockSkill(Skills.SkillType.StaffChargedAttackI);
            StaffChargedAttackI_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void StaffChargedAttackII_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.StaffChargedAttackI)) return;
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.StaffChargedAttackII))
        {
            playerSkills.UnlockSkill(Skills.SkillType.StaffChargedAttackII);
            StaffChargedAttackII_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void ArcaneBolt_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.ArcaneBolt))
        {
            playerSkills.UnlockSkill(Skills.SkillType.ArcaneBolt);
            ArcaneBolt_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void ArcaneBoltPlus_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.ArcaneBolt)) return;
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.ArcaneBoltPlus))
        {
            playerSkills.UnlockSkill(Skills.SkillType.ArcaneBoltPlus);
            ArcaneBoltPlus_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void StunningSpell_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.ArcaneBolt)) return;
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.StunningSpell))
        {
            playerSkills.UnlockSkill(Skills.SkillType.StunningSpell);
            StunningSpell_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void EnchantWeapon_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.ArcaneBolt)) return;
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.EnchantWeapon))
        {
            playerSkills.UnlockSkill(Skills.SkillType.EnchantWeapon);
            EnchantWeapon_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void Invincibility_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.EnchantWeapon)) return;
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.Invincibility))
        {
            playerSkills.UnlockSkill(Skills.SkillType.Invincibility);
            Invincibility_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void EmpoweredAttack_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.Invincibility) || !playerSkills.IsSkillUnlocked(Skills.SkillType.StunningSpell)) return;
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.EmpoweredAttack))
        {
            playerSkills.UnlockSkill(Skills.SkillType.EmpoweredAttack);
            EmpoweredAttack_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void ImprovedDashing_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.ImprovedDashing))
        {
            playerSkills.UnlockSkill(Skills.SkillType.ImprovedDashing);
            ImprovedDashing_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void AncientRecall_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.ImprovedDashing)) return;
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.AncientRecall))
        {
            playerSkills.UnlockSkill(Skills.SkillType.AncientRecall);
            AncientRecall_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void PotionAffinityI_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.PotionAffinityI))
        {
            playerSkills.UnlockSkill(Skills.SkillType.PotionAffinityI);
            PotionAffinityI_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void PotionAffinityII_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.PotionAffinityI)) return;
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.PotionAffinityII))
        {
            playerSkills.UnlockSkill(Skills.SkillType.PotionAffinityII);
            PotionAffinityII_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }

    void PotionAffinityIII_Clicked()
    {
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.PotionAffinityII)) return;
        if (!playerSkills.IsSkillUnlocked(Skills.SkillType.PotionAffinityIII))
        {
            playerSkills.UnlockSkill(Skills.SkillType.PotionAffinityIII);
            PotionAffinityIII_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
        }
    }
}
