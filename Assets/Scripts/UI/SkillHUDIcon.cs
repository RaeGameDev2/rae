using System;
using System.Collections;
using UnityEngine;

public class SkillHUDIcon : MonoBehaviour
{
    public Sprite[] scytheIcons;
    public Sprite[] orbIcons;
    public Sprite[] staffIcons;

    private PlayerSpells playerSpells;
    private PlayerSkills playerSkills;
    private WeaponsHandler weaponsHandler;
    private PlayerResources playerResources;

    private UnityEngine.UI.Image icon;

    private void Start()
    {
        playerSpells = GameObject.FindObjectOfType<PlayerSpells>().GetComponent<PlayerSpells>();
        playerSkills = GameObject.FindObjectOfType<PlayerSkills>().GetComponent<PlayerSkills>();
        playerResources = GameObject.FindObjectOfType<PlayerResources>().GetComponent<PlayerResources>();
        weaponsHandler = GameObject.FindObjectOfType<WeaponsHandler>().GetComponent<WeaponsHandler>();
        icon = GetComponent<UnityEngine.UI.Image>();
        icon.enabled = false;
    }

    public void Update()
    {
        changeIcon(weaponsHandler.currWeapon.type);
    }

    public void changeIcon(Weapon.WeaponType type)
    {
        switch (type)
        {
            case Weapon.WeaponType.Scythe:
                {
                    if (playerSkills.playerSkills.IsSkillUnlocked(Skills.SkillType.LifeDrain))
                    {
                        icon.enabled = true;
                        if (playerResources.currentMana == 0 || playerSpells.lifeDrainActive)
                            icon.sprite = scytheIcons[0];
                        else
                            icon.sprite = scytheIcons[1];
                    }
                    else if (playerSkills.playerSkills.IsSkillUnlocked(Skills.SkillType.Parry))
                    {
                        icon.enabled = true;
                        if (playerResources.currentMana == 0 || playerSpells.parryActive)
                            icon.sprite = scytheIcons[2];
                        else
                            icon.sprite = scytheIcons[3];
                    }
                    else
                    {
                        icon.enabled = false;
                    }
                }
                break;
            case Weapon.WeaponType.Orb:
                {
                    if (playerSkills.playerSkills.IsSkillUnlocked(Skills.SkillType.QuickTP))
                    {
                        icon.enabled = true;
                        if (playerResources.currentMana == 0 || playerSpells.quickTeleportActive)
                            icon.sprite = orbIcons[0];
                        else
                            icon.sprite = orbIcons[1];
                    }
                    else if (playerSkills.playerSkills.IsSkillUnlocked(Skills.SkillType.Shield))
                    {
                        icon.enabled = true;
                        if (playerResources.currentMana == 0 || playerSpells.shieldActive)
                            icon.sprite = orbIcons[2];
                        else
                            icon.sprite = orbIcons[3];
                    }
                    else
                    {
                        icon.enabled = false;
                    }
                }
                break;
            case Weapon.WeaponType.Staff:
                {
                    if (playerSkills.playerSkills.IsSkillUnlocked(Skills.SkillType.PhaseWalk))
                    {
                        icon.enabled = true;
                        if (playerResources.currentMana == 0 || playerSpells.phaseWalkActive)
                            icon.sprite = staffIcons[0];
                        else
                            icon.sprite = staffIcons[1];
                    }
                    else if (playerSkills.playerSkills.IsSkillUnlocked(Skills.SkillType.Debuff))
                    {
                        icon.enabled = true;
                        if (playerResources.currentMana == 0 || playerSpells.debuffActive)
                            icon.sprite = staffIcons[2];
                        else
                            icon.sprite = staffIcons[3];
                    }
                    else
                    {
                        icon.enabled = false;
                    }
                }
                break;
        }
    }
}
