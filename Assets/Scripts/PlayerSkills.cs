using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private UI_SkillTree uiSkillTree;
    private Skills playerSkills;
    private void Start()
    {
        playerSkills = new Skills();
        uiSkillTree = GameObject.Find("UI_Skilltree").GetComponent<UI_SkillTree>();
        uiSkillTree.SetPlayerSkills(playerSkills);
    }


    public bool IsBetterBlockingIUnlocked()
    {
        return playerSkills.IsSkillUnlocked(Skills.SkillType.BetterBlockingI);
    }

}
