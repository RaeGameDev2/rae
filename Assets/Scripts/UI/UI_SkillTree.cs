using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillTree : MonoBehaviour
{
    private const int arr_size = 5;

    private int activePanel; // 0 - orb | 1 - scythe | 2 - staff
    [SerializeField] private Button Attack_button;
    [SerializeField] private Button AttackSpeed_button;
    [SerializeField] private Button CritBonus_button;
    [SerializeField] private Button CritRate_button;
    [SerializeField] private Button Debuff_button;
    [SerializeField] private Button Life_button;
    [SerializeField] private Button LifeDrain_button;
    [SerializeField] private Button Mana_button;

    [SerializeField] private Button Orb_button;
    private GameObject Orb_panel;
    private GameObject Orb_stats;
    private Text OrbAttackSpeedText;
    private Text OrbAttackText;
    private Text OrbCritBonusText;
    private Text OrbCritRateText;

    [SerializeField] private Button Parry_button;
    [SerializeField] private Button PhaseWalk_button;
    private PlayerResources playerResources;
    private Skills playerSkills;
    [SerializeField] private Button QuickTP_button;
    [SerializeField] private Button Scythe_button;

    private GameObject Scythe_panel;

    private GameObject Scythe_stats;
    private Text ScytheAttackSpeedText;

    private Text ScytheAttackText;
    private Text ScytheCritBonusText;
    private Text ScytheCritRateText;
    [SerializeField] private Button Shield_button;
    private GameObject Skill_stats;

    private  string[] SkillDescription =
    {
        "Attack - Increases Rae’s attack damage",
        "Attack Speed - Increases Rae’s attack speed",
        "Mana - Increases Rae’s maximum mana points",
        "Life - Increases Rae’s maximum health points",
        "Critical Rate - Raises the probability that an attack will be a critical hit, dealing increased damage",
        "Critical Bonus - Increases the amount of additional damage an enemy receives by a critical hit",
        "Quick TP - When the spell is first cast, Rae leaves their orb at the current position. When cast again, they teleport back to their orb, summoning a shock sphere that deals damage to enemies in the surrounding area",
        "Shield - Rae surrounds themselves with a magic shield, blocking a few enemy hits",
        "Parry - Rae attempts to use their scythe’s snath to parry an enemy attack. If they do it on time, right before a strike is about to hit them, they nullify the damage and deal a seismic blow on enemies around them",
        "Life Drain - Rae’s next attack pierces their enemy, dealing moderate immediate damage and making it bleed. For a few seconds, it continues to take damage over time",
        "Debuff - Rae’s next attack is a spell that disorders their enemy’s mind, lowering its movement speed and attack damage",
        "Phase Walk - For a few seconds, Rae becomes invisible, and immune to their enemies, but while in this state they are unable to attack"
    };

    [SerializeField] private Text SkillDescriptionText;

    private  string[] SkillNames =
    {
        "Attack",
        "Attack Speed",
        "Mana",
        "Life",
        "Critical Rate",
        "Critical Bonus",
        "Quick TP",
        "Shield",
        "Parry",
        "Life Drain",
        "Debuff",
        "Phase Walk"
    };

    [SerializeField] private Text SkillNameText;

    [SerializeField] private Text SkillPointsNr;
    [SerializeField] private  Sprite[] SP_Attack = new Sprite[arr_size];
    [SerializeField] private  Sprite[] SP_AttackSpeed = new Sprite[arr_size];
    [SerializeField] private  Sprite[] SP_CritBonus = new Sprite[arr_size];
    [SerializeField] private  Sprite[] SP_CritRate = new Sprite[arr_size];
    [SerializeField] private  Sprite[] SP_Debuff = new Sprite[arr_size];
    [SerializeField] private  Sprite[] SP_Life = new Sprite[arr_size];
    [SerializeField] private  Sprite[] SP_LifeDrain = new Sprite[arr_size];
    [SerializeField] private  Sprite[] SP_Mana = new Sprite[arr_size];
    [SerializeField] private  Sprite[] SP_Parry = new Sprite[arr_size];
    [SerializeField] private  Sprite[] SP_PhaseWalk = new Sprite[arr_size];
    [SerializeField] private  Sprite[] SP_QuickTP = new Sprite[arr_size];
    [SerializeField] private  Sprite[] SP_Shield = new Sprite[arr_size];
    [SerializeField] private Button Staff_button;
    private GameObject Staff_panel;
    private GameObject Staff_stats;
    private Text StaffAttackSpeedText;
    private Text StaffAttackText;
    private Text StaffCritBonusText;
    private Text StaffCritRateText;
    private WeaponsHandler weaponsHandler;

    private void Awake()
    {
        Scythe_stats = GameObject.Find("Scythe_Stats");
        Orb_stats = GameObject.Find("Orb_Stats");
        Staff_stats = GameObject.Find("Staff_Stats");
        Skill_stats = GameObject.Find("Skill_Stats");

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

        Skill_stats.SetActive(false);
        Scythe_stats.SetActive(false);
        Staff_stats.SetActive(false);
        Orb_stats.SetActive(true);
        Orb_panel.GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);

        var Parry_btn = Parry_button.GetComponent<Button>();
        Parry_btn.onClick.AddListener(Parry_Clicked);

        var LifeDrain_btn = LifeDrain_button.GetComponent<Button>();
        LifeDrain_btn.onClick.AddListener(LifeDrain_Clicked);

        var QuickTP_btn = QuickTP_button.GetComponent<Button>();
        QuickTP_btn.onClick.AddListener(QuickTP_Clicked);

        var Shield_btn = Shield_button.GetComponent<Button>();
        Shield_btn.onClick.AddListener(Shield_Clicked);

        var Debuff_btn = Debuff_button.GetComponent<Button>();
        Debuff_btn.onClick.AddListener(Debuff_Clicked);

        var PhaseWalk_btn = PhaseWalk_button.GetComponent<Button>();
        PhaseWalk_btn.onClick.AddListener(PhaseWalk_Clicked);

        var Attack_btn = Attack_button.GetComponent<Button>();
        Attack_btn.onClick.AddListener(Attack_Clicked);

        var CritBonus_btn = CritBonus_button.GetComponent<Button>();
        CritBonus_btn.onClick.AddListener(CritBonus_Clicked);

        var CritRate_btn = CritRate_button.GetComponent<Button>();
        CritRate_btn.onClick.AddListener(CritRate_Clicked);

        var AttackSpeed_btn = AttackSpeed_button.GetComponent<Button>();
        AttackSpeed_btn.onClick.AddListener(AttackSpeed_Clicked);

        var Life_btn = Life_button.GetComponent<Button>();
        Life_btn.onClick.AddListener(Life_Clicked);

        var Mana_btn = Mana_button.GetComponent<Button>();
        Mana_btn.onClick.AddListener(Mana_Clicked);

        var Scythe_btn = Scythe_button.GetComponent<Button>();
        Scythe_btn.onClick.AddListener(Scythe_Clicked);

        var Orb_btn = Orb_button.GetComponent<Button>();
        Orb_btn.onClick.AddListener(Orb_Clicked);

        var Staff_btn = Staff_button.GetComponent<Button>();
        Staff_btn.onClick.AddListener(Staff_Clicked);
    }

    private void Start()
    {
        playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        weaponsHandler = FindObjectOfType<WeaponsHandler>();
        playerSkills = FindObjectOfType<PlayerSkills>().playerSkills;
        UpdateUI();
    }

    private void Update()
    {
        SkillPointsNr.text = "SKILL POINTS: " + playerSkills.GetSkillPoints();
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
        ScytheAttackText.text =
            $"Attack: {weaponsHandler.weapons[(int) Weapon.WeaponType.Scythe].mainDamage + playerSkills.GetSkillLevel(Skills.SkillType.Attack) * weaponsHandler.weapons[(int) Weapon.WeaponType.Scythe].bonusAttackDmg}";
        ScytheAttackSpeedText.text =
            $"Attack Speed: {weaponsHandler.weapons[(int) Weapon.WeaponType.Scythe].attackSpeed + playerSkills.GetSkillLevel(Skills.SkillType.AttackSpeed) * weaponsHandler.weapons[(int) Weapon.WeaponType.Scythe].bonusAttackSpeed}";
        ScytheCritRateText.text =
            $"Critical Rate: {weaponsHandler.weapons[(int) Weapon.WeaponType.Scythe].critRate + playerSkills.GetSkillLevel(Skills.SkillType.CritRate) * weaponsHandler.weapons[(int) Weapon.WeaponType.Scythe].bonusCritRate}";
        ScytheCritBonusText.text =
            $"Critical Bonus: {weaponsHandler.weapons[(int) Weapon.WeaponType.Scythe].critDmg + playerSkills.GetSkillLevel(Skills.SkillType.CritBonus) * weaponsHandler.weapons[(int) Weapon.WeaponType.Scythe].bonusCritDmg}";

        OrbAttackText.text =
            $"Attack: {weaponsHandler.weapons[(int) Weapon.WeaponType.Orb].mainDamage + playerSkills.GetSkillLevel(Skills.SkillType.Attack) * weaponsHandler.weapons[(int) Weapon.WeaponType.Orb].bonusAttackDmg}";
        OrbAttackSpeedText.text =
            $"Attack Speed: {weaponsHandler.weapons[(int) Weapon.WeaponType.Orb].attackSpeed + playerSkills.GetSkillLevel(Skills.SkillType.AttackSpeed) * weaponsHandler.weapons[(int) Weapon.WeaponType.Orb].bonusAttackSpeed}";
        OrbCritRateText.text =
            $"Critical Rate: {weaponsHandler.weapons[(int) Weapon.WeaponType.Orb].critRate + playerSkills.GetSkillLevel(Skills.SkillType.CritRate) * weaponsHandler.weapons[(int) Weapon.WeaponType.Orb].bonusCritRate}";
        OrbCritBonusText.text =
            $"Critical Bonus: {weaponsHandler.weapons[(int) Weapon.WeaponType.Orb].critDmg + playerSkills.GetSkillLevel(Skills.SkillType.CritBonus) * weaponsHandler.weapons[(int) Weapon.WeaponType.Orb].bonusCritDmg}";

        StaffAttackText.text =
            $"Attack: {weaponsHandler.weapons[(int) Weapon.WeaponType.Staff].mainDamage + playerSkills.GetSkillLevel(Skills.SkillType.Attack) * weaponsHandler.weapons[(int) Weapon.WeaponType.Staff].bonusAttackDmg}";
        StaffAttackSpeedText.text =
            $"Attack Speed: {weaponsHandler.weapons[(int) Weapon.WeaponType.Staff].attackSpeed + playerSkills.GetSkillLevel(Skills.SkillType.AttackSpeed) * weaponsHandler.weapons[(int) Weapon.WeaponType.Staff].bonusAttackSpeed}";
        StaffCritRateText.text =
            $"Critical Rate: {weaponsHandler.weapons[(int) Weapon.WeaponType.Staff].critRate + playerSkills.GetSkillLevel(Skills.SkillType.CritRate) * weaponsHandler.weapons[(int) Weapon.WeaponType.Staff].bonusCritRate}";
        StaffCritBonusText.text =
            $"Critical Bonus: {weaponsHandler.weapons[(int) Weapon.WeaponType.Staff].critDmg + playerSkills.GetSkillLevel(Skills.SkillType.CritBonus) * weaponsHandler.weapons[(int) Weapon.WeaponType.Staff].bonusCritDmg}";
    }

    public void SetPlayerSkills(Skills playerSkills)
    {
        this.playerSkills = playerSkills;
    }


    //Mouse Over
    public void Attack_MouseOver()
    {
        Attack_button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SkillStats_Show(1);
    }

    public void AttackSpeed_MouseOver()
    {
        AttackSpeed_button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SkillStats_Show(2);
    }

    public void Mana_MouseOver()
    {
        Mana_button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SkillStats_Show(3);
    }

    public void Life_MouseOver()
    {
        Life_button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SkillStats_Show(4);
    }

    public void CritRate_MouseOver()
    {
        CritRate_button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SkillStats_Show(5);
    }

    public void CritBonus_MouseOver()
    {
        CritBonus_button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SkillStats_Show(6);
    }

    public void QuickTP_MouseOver()
    {
        QuickTP_button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SkillStats_Show(7);
    }

    public void Shield_MouseOver()
    {
        Shield_button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SkillStats_Show(8);
    }

    public void Parry_MouseOver()
    {
        Parry_button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SkillStats_Show(9);
    }

    public void LifeDrain_MouseOver()
    {
        LifeDrain_button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SkillStats_Show(10);
    }

    public void Debuff_MouseOver()
    {
        Debuff_button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SkillStats_Show(11);
    }

    public void PhaseWalk_MouseOver()
    {
        PhaseWalk_button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        SkillStats_Show(12);
    }


    //Mouse Exit
    public void Attack_MouseExit()
    {
        Attack_button.transform.localScale = new Vector3(1f, 1f, 1f);
        SkillStats_Hide();
    }

    public void AttackSpeed_MouseExit()
    {
        AttackSpeed_button.transform.localScale = new Vector3(1f, 1f, 1f);
        SkillStats_Hide();
    }

    public void Mana_MouseExit()
    {
        Mana_button.transform.localScale = new Vector3(1f, 1f, 1f);
        SkillStats_Hide();
    }

    public void Life_MouseExit()
    {
        Life_button.transform.localScale = new Vector3(1f, 1f, 1f);
        SkillStats_Hide();
    }

    public void CritRate_MouseExit()
    {
        CritRate_button.transform.localScale = new Vector3(1f, 1f, 1f);
        SkillStats_Hide();
    }

    public void CritBonus_MouseExit()
    {
        CritBonus_button.transform.localScale = new Vector3(1f, 1f, 1f);
        SkillStats_Hide();
    }

    public void QuickTP_MouseExit()
    {
        QuickTP_button.transform.localScale = new Vector3(1f, 1f, 1f);
        SkillStats_Hide();
    }

    public void Shield_MouseExit()
    {
        Shield_button.transform.localScale = new Vector3(1f, 1f, 1f);
        SkillStats_Hide();
    }

    public void Parry_MouseExit()
    {
        Parry_button.transform.localScale = new Vector3(1f, 1f, 1f);
        SkillStats_Hide();
    }

    public void LifeDrain_MouseExit()
    {
        LifeDrain_button.transform.localScale = new Vector3(1f, 1f, 1f);
        SkillStats_Hide();
    }

    public void Debuff_MouseExit()
    {
        Debuff_button.transform.localScale = new Vector3(1f, 1f, 1f);
        SkillStats_Hide();
    }

    public void PhaseWalk_MouseExit()
    {
        PhaseWalk_button.transform.localScale = new Vector3(1f, 1f, 1f);
        SkillStats_Hide();
    }

    private void SkillStats_Show(int skillId)
    {
        Skill_stats.SetActive(true);
        Scythe_stats.SetActive(false);
        Staff_stats.SetActive(false);
        Orb_stats.SetActive(false);

        SkillNameText.text = SkillNames[skillId - 1];
        SkillDescriptionText.text = SkillDescription[skillId - 1];
    }

    private void SkillStats_Hide()
    {
        Skill_stats.SetActive(false);
        if (activePanel == 0) Orb_stats.SetActive(true);
        else if (activePanel == 1) Scythe_stats.SetActive(true);
        else if (activePanel == 2) Staff_stats.SetActive(true);
    }

    private void Orb_Clicked()
    {
        Skill_stats.SetActive(false);
        Scythe_stats.SetActive(false);
        Staff_stats.SetActive(false);
        Orb_stats.SetActive(true);
        Orb_panel.GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
        Staff_panel.GetComponent<Image>().color = new Color(48f / 255f, 48f / 255f, 48f / 255f);
        Scythe_panel.GetComponent<Image>().color = new Color(48f / 255f, 48f / 255f, 48f / 255f);
        activePanel = 0;
    }

    private void Staff_Clicked()
    {
        Skill_stats.SetActive(false);
        Scythe_stats.SetActive(false);
        Staff_stats.SetActive(true);
        Orb_stats.SetActive(false);
        Orb_panel.GetComponent<Image>().color = new Color(48f / 255f, 48f / 255f, 48f / 255f);
        Staff_panel.GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
        Scythe_panel.GetComponent<Image>().color = new Color(48f / 255f, 48f / 255f, 48f / 255f);
        activePanel = 2;
    }

    private void Scythe_Clicked()
    {
        Skill_stats.SetActive(false);
        Scythe_stats.SetActive(true);
        Staff_stats.SetActive(false);
        Orb_stats.SetActive(false);
        Orb_panel.GetComponent<Image>().color = new Color(48f / 255f, 48f / 255f, 48f / 255f);
        Staff_panel.GetComponent<Image>().color = new Color(48f / 255f, 48f / 255f, 48f / 255f);
        Scythe_panel.GetComponent<Image>().color = new Color(80f / 255f, 80f / 255f, 80f / 255f);
        activePanel = 1;
    }

    private void Parry_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Parry);
        UpdateUI();
    }

    private void LifeDrain_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.LifeDrain);
        UpdateUI();
    }

    private void QuickTP_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.QuickTP);
        UpdateUI();
    }

    private void Shield_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Shield);
        UpdateUI();
    }

    private void Debuff_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Debuff);
        UpdateUI();
    }

    private void PhaseWalk_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.PhaseWalk);
        UpdateUI();
    }

    private void Attack_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Attack);
        UpdateUI();
    }

    private void CritBonus_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.CritBonus);
        UpdateUI();
    }

    private void CritRate_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.CritRate);
        UpdateUI();
    }

    private void AttackSpeed_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.AttackSpeed);
        UpdateUI();
    }

    private void Life_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Life);
        UpdateUI();
        playerResources.IncreaseMaxHealth();
    }

    private void Mana_Clicked()
    {
        playerSkills.UpgradeSkill(Skills.SkillType.Mana);
        UpdateUI();
        playerResources.IncreaseMaxMana();
    }
}