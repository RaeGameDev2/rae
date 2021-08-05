using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private UI_SkillTree uiSkillTree;
    private Skills playerSkills;

    GameObject Skilltree;
    GameObject ShowButton;

    private void Start()
    {
        playerSkills = new Skills();
        uiSkillTree = GameObject.Find("UI_Skilltree").GetComponent<UI_SkillTree>();
        uiSkillTree.SetPlayerSkills(playerSkills);

        Skilltree = GameObject.Find("UI_Skilltree");
        ShowButton = GameObject.Find("Show_button");

        Button Exit_btn = GameObject.Find("Exit_button").GetComponent<Button>();
        Exit_btn.onClick.AddListener(HideUI);


        Button Show_btn = ShowButton.GetComponent<Button>();
        Show_btn.onClick.AddListener(ShowUI);

        HideUI();

    }

    public void HideUI()
    {
        ShowButton.SetActive(true);
        Skilltree.SetActive(false);
    }

    public void ShowUI()
    {
        Skilltree.SetActive(true);
        ShowButton.SetActive(false);
    }

    //Exemplu:
    public bool IsParryUnlocked()
    {
         return playerSkills.IsSkillUnlocked(Skills.SkillType.Parry);
    }

}
