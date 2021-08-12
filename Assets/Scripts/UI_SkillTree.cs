using System.Linq;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class UI_SkillTree : MonoBehaviour
{
    private Skills playerSkills;
    private Weapons_Handler weaponsHandler;
    private Resources resources;

    public Text SkillPointsNr;


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


    private const int arr_size = 5;
    public Sprite[] SP_Parry = new Sprite[arr_size];
    public Sprite[] SP_LifeDrain = new Sprite[arr_size];
    public Sprite[] SP_QuickTP = new Sprite[arr_size];
    public Sprite[] SP_Shield = new Sprite[arr_size];
    public Sprite[] SP_Debuff = new Sprite[arr_size];
    public Sprite[] SP_PhaseWalk = new Sprite[arr_size];
    public Sprite[] SP_Attack = new Sprite[arr_size];
    public Sprite[] SP_CritBonus = new Sprite[arr_size];
    public Sprite[] SP_CritRate = new Sprite[arr_size];
    public Sprite[] SP_AttackSpeed = new Sprite[arr_size];
    public Sprite[] SP_Life = new Sprite[arr_size];
    public Sprite[] SP_Mana = new Sprite[arr_size];

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
        playerSkills = FindObjectOfType<PlayerSkills>().playerSkills;
        UpdateUI();
    }

    private void Update()
    {
        SkillPointsNr.text = "SKILL POINTS: " + playerSkills.GetSkillPoints().ToString();
    }

    private void UpdateUI()
    {
        UpdateStats();

        int lvl;
        lvl = playerSkills.GetSkillLevel(Skills.SkillType.Parry);
        Parry_button.GetComponent<Button>().GetComponent<Image>().sprite = SP_Parry[lvl];

        lvl = playerSkills.GetSkillLevel(Skills.SkillType.LifeDrain);
        LifeDrain_button.GetComponent<Button>().GetComponent<Image>().sprite = SP_LifeDrain[lvl];

        lvl = playerSkills.GetSkillLevel(Skills.SkillType.QuickTP);
        QuickTP_button.GetComponent<Button>().GetComponent<Image>().sprite = SP_QuickTP[lvl];

        lvl = playerSkills.GetSkillLevel(Skills.SkillType.Shield);
        Shield_button.GetComponent<Button>().GetComponent<Image>().sprite = SP_Shield[lvl];

        lvl = playerSkills.GetSkillLevel(Skills.SkillType.Debuff);
        Debuff_button.GetComponent<Button>().GetComponent<Image>().sprite = SP_Debuff[lvl];

        lvl = playerSkills.GetSkillLevel(Skills.SkillType.PhaseWalk);
        PhaseWalk_button.GetComponent<Button>().GetComponent<Image>().sprite = SP_PhaseWalk[lvl];

        lvl = playerSkills.GetSkillLevel(Skills.SkillType.Attack);
        Attack_button.GetComponent<Button>().GetComponent<Image>().sprite = SP_Attack[lvl];

        lvl = playerSkills.GetSkillLevel(Skills.SkillType.CritRate);
        CritRate_button.GetComponent<Button>().GetComponent<Image>().sprite = SP_CritRate[lvl];

        lvl = playerSkills.GetSkillLevel(Skills.SkillType.CritBonus);
        CritBonus_button.GetComponent<Button>().GetComponent<Image>().sprite = SP_CritBonus[lvl];

        lvl = playerSkills.GetSkillLevel(Skills.SkillType.AttackSpeed);
        AttackSpeed_button.GetComponent<Button>().GetComponent<Image>().sprite = SP_AttackSpeed[lvl];

        lvl = playerSkills.GetSkillLevel(Skills.SkillType.Life);
        Life_button.GetComponent<Button>().GetComponent<Image>().sprite = SP_Life[lvl];

        lvl = playerSkills.GetSkillLevel(Skills.SkillType.Mana);
        Mana_button.GetComponent<Button>().GetComponent<Image>().sprite = SP_Mana[lvl];

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
        resources.IncreaseMaxHealth();
    }
    void Mana_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Mana);
        UpdateUI();
        resources.IncreaseMaxMana();
    }

}
