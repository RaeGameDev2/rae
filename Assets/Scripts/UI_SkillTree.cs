using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTree : MonoBehaviour
{
    private Skills playerSkills;
    private Weapons_Handler weaponsHandler;
    private Resources resources;

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
    public Button Life_button;
    public Button Mana_button;

    public Button Orb_button;
    public Button Staff_button;
    public Button Scythe_button;

    private GameObject Scythe_stats;
    private GameObject Orb_stats;
    private GameObject Staff_stats;

    private GameObject Scythe_panel;
    private GameObject Orb_panel;
    private GameObject Staff_panel;

    private Text ScytheAttackText;
    private Text ScytheCritRateText;
    private Text ScytheCritBonusText;
    private Text ScytheAttackSpeedText;
    private Text OrbAttackText;
    private Text OrbCritRateText;
    private Text OrbCritBonusText;
    private Text OrbAttackSpeedText;
    private Text StaffAttackText;
    private Text StaffCritRateText;
    private Text StaffCritBonusText;
    private Text StaffAttackSpeedText;

    private void Awake()
    {
        Scythe_stats = GameObject.Find("Scythe_Stats");
        Orb_stats = GameObject.Find("Orb_Stats");
        Staff_stats = GameObject.Find("Staff_Stats");

        Scythe_panel = GameObject.Find("Scythe_panel");
        Orb_panel = GameObject.Find("Orb_panel");
        Staff_panel = GameObject.Find("Staff_panel");

        var components = GetComponentsInChildren<Text>();
        ScytheAttackText = components.FirstOrDefault(component => component.name == "Scythe_Attack_text");
        ScytheCritRateText = components.FirstOrDefault(component => component.name == "Scythe_CritRate_text");
        ScytheCritBonusText = components.FirstOrDefault(component => component.name == "Scythe_CritBonus_text");
        ScytheAttackSpeedText = components.FirstOrDefault(component => component.name == "Scythe_AttackSpeed_text");

        OrbAttackText = components.FirstOrDefault(component => component.name == "Orb_Attack_text");
        OrbCritRateText = components.FirstOrDefault(component => component.name == "Orb_CritRate_text");
        OrbCritBonusText = components.FirstOrDefault(component => component.name == "Orb_CritBonus_text");
        OrbAttackSpeedText = components.FirstOrDefault(component => component.name == "Orb_AttackSpeed_text");

        StaffAttackText = components.FirstOrDefault(component => component.name == "Staff_Attack_text");
        StaffCritRateText = components.FirstOrDefault(component => component.name == "Staff_CritRate_text");
        StaffCritBonusText = components.FirstOrDefault(component => component.name == "Staff_CritBonus_text");
        StaffAttackSpeedText = components.FirstOrDefault(component => component.name == "Staff_AttackSpeed_text");

        Scythe_stats.SetActive(false);
        Staff_stats.SetActive(false);
        Orb_stats.SetActive(true);
        Orb_panel.GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);

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

        Button Life_btn = Life_button.GetComponent<Button>();
        Life_btn.onClick.AddListener(Life_Clicked);

        Button Mana_btn = Mana_button.GetComponent<Button>();
        Mana_btn.onClick.AddListener(Mana_Clicked);

        Button Scythe_btn = Scythe_button.GetComponent<Button>();
        Scythe_btn.onClick.AddListener(Scythe_Clicked);

        Button Orb_btn = Orb_button.GetComponent<Button>();
        Orb_btn.onClick.AddListener(Orb_Clicked);

        Button Staff_btn = Staff_button.GetComponent<Button>();
        Staff_btn.onClick.AddListener(Staff_Clicked);
    }

    private void Start()
    {
        resources = GameObject.FindGameObjectWithTag("Player").GetComponent<Resources>();
        weaponsHandler = FindObjectOfType<Weapons_Handler>();
        UpdateStats();
    }

    private void UpdateUI()
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

        if (playerSkills.GetSkillLevel(Skills.SkillType.Life) > 0)
        {
            Life_button.GetComponent<Button>().GetComponent<Image>().color = Color.green;
            Life_button.GetComponent<Button>().GetComponentInChildren<Text>().text = "Life\n(" + playerSkills.GetSkillLevel(Skills.SkillType.Life) + ")";
        }

        UpdateStats();
    }

    private void UpdateStats()
    {
        ScytheAttackText.text = $"Attack: {weaponsHandler.weapons[(int) Weapon.WeaponType.SCYTHE].mainDamage + playerSkills.GetSkillLevel(Skills.SkillType.Attack) * weaponsHandler.weapons[(int)Weapon.WeaponType.SCYTHE].bonusAttackDmg}";
        ScytheAttackSpeedText.text = $"Attack Speed: {weaponsHandler.weapons[(int) Weapon.WeaponType.SCYTHE].attackSpeed + playerSkills.GetSkillLevel(Skills.SkillType.AttackSpeed) * weaponsHandler.weapons[(int)Weapon.WeaponType.SCYTHE].bonusAttackSpeed}";
        ScytheCritRateText.text = $"Critical Rate: {weaponsHandler.weapons[(int) Weapon.WeaponType.SCYTHE].critRate + playerSkills.GetSkillLevel(Skills.SkillType.CritRate) * weaponsHandler.weapons[(int)Weapon.WeaponType.SCYTHE].bonusCritRate}";
        ScytheCritBonusText.text = $"Critical Bonus: {weaponsHandler.weapons[(int) Weapon.WeaponType.SCYTHE].critDmg + playerSkills.GetSkillLevel(Skills.SkillType.CritBonus) * weaponsHandler.weapons[(int)Weapon.WeaponType.SCYTHE].bonusCritDmg}";

        OrbAttackText.text = $"Attack: {weaponsHandler.weapons[(int) Weapon.WeaponType.ORB].mainDamage + playerSkills.GetSkillLevel(Skills.SkillType.Attack) * weaponsHandler.weapons[(int)Weapon.WeaponType.ORB].bonusAttackDmg}";
        OrbAttackSpeedText.text = $"Attack Speed: {weaponsHandler.weapons[(int) Weapon.WeaponType.ORB].attackSpeed + playerSkills.GetSkillLevel(Skills.SkillType.AttackSpeed) * weaponsHandler.weapons[(int)Weapon.WeaponType.ORB].bonusAttackSpeed}";
        OrbCritRateText.text = $"Critical Rate: {weaponsHandler.weapons[(int) Weapon.WeaponType.ORB].critRate + playerSkills.GetSkillLevel(Skills.SkillType.CritRate) * weaponsHandler.weapons[(int)Weapon.WeaponType.ORB].bonusCritRate}";
        OrbCritBonusText.text = $"Critical Bonus: {weaponsHandler.weapons[(int) Weapon.WeaponType.ORB].critDmg + playerSkills.GetSkillLevel(Skills.SkillType.CritBonus) * weaponsHandler.weapons[(int)Weapon.WeaponType.ORB].bonusCritDmg}";

        StaffAttackText.text = $"Attack: {weaponsHandler.weapons[(int) Weapon.WeaponType.STAFF].mainDamage + playerSkills.GetSkillLevel(Skills.SkillType.Attack) * weaponsHandler.weapons[(int)Weapon.WeaponType.STAFF].bonusAttackDmg}";
        StaffAttackSpeedText.text = $"Attack Speed: {weaponsHandler.weapons[(int) Weapon.WeaponType.STAFF].attackSpeed + playerSkills.GetSkillLevel(Skills.SkillType.AttackSpeed) * weaponsHandler.weapons[(int)Weapon.WeaponType.STAFF].bonusAttackSpeed}";
        StaffCritRateText.text = $"Critical Rate: {weaponsHandler.weapons[(int) Weapon.WeaponType.STAFF].critRate + playerSkills.GetSkillLevel(Skills.SkillType.CritRate) * weaponsHandler.weapons[(int)Weapon.WeaponType.STAFF].bonusCritRate}";
        StaffCritBonusText.text = $"Critical Bonus: {weaponsHandler.weapons[(int) Weapon.WeaponType.STAFF].critDmg + playerSkills.GetSkillLevel(Skills.SkillType.CritBonus) * weaponsHandler.weapons[(int)Weapon.WeaponType.STAFF].bonusCritDmg}";
    }

    public void SetPlayerSkills(Skills playerSkills)
    {
        this.playerSkills = playerSkills;
    }

    void Orb_Clicked()
    {
        Scythe_stats.SetActive(false);
        Staff_stats.SetActive(false);
        Orb_stats.SetActive(true);
        Orb_panel.GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
        Staff_panel.GetComponent<Image>().color = new Color(48f / 255f, 48f / 255f, 48f / 255f);
        Scythe_panel.GetComponent<Image>().color = new Color(48f / 255f, 48f / 255f, 48f / 255f);
    }

    void Staff_Clicked()
    {
        Scythe_stats.SetActive(false);
        Staff_stats.SetActive(true);
        Orb_stats.SetActive(false);
        Orb_panel.GetComponent<Image>().color = new Color(48f / 255f, 48f / 255f, 48f / 255f);
        Staff_panel.GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
        Scythe_panel.GetComponent<Image>().color = new Color(48f / 255f, 48f / 255f, 48f / 255f);
    }

    void Scythe_Clicked()
    {
        Scythe_stats.SetActive(true);
        Staff_stats.SetActive(false);
        Orb_stats.SetActive(false);
        Orb_panel.GetComponent<Image>().color = new Color(48f / 255f, 48f / 255f, 48f / 255f);
        Staff_panel.GetComponent<Image>().color = new Color(48f / 255f, 48f / 255f, 48f / 255f);
        Scythe_panel.GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
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

    void Life_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Life);
        UpdateUI();
    }
    void Mana_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Mana);
        UpdateUI();
    }

}
