using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_GUI : MonoBehaviour
{
    public Slider healthBar;
    public Text health;

    public Slider manaBar;
    public Text mana;

    public Slider fearBar;
    public Text fear;

    public Slider xpBar;
    public Text xp;

    public GameObject inventoryPanel;
    public GameObject equipmentPanel;
    public GameObject statsPanel;
    public GameObject abilitiesPanel;

    public GameObject invBtn;
    public GameObject eqBtn;
    public GameObject spBtn;
    public GameObject abBtn;

    public SkillPanel skills;
    public EquipmentPanel invEqu;

    public void ToggleAll(bool pToggle)
    {
        gameObject.SetActive(pToggle);
    }

    public void UpdateHealth(float pCurrent, float pMax)
    { 
        if (healthBar != null && health != null)
        {
            healthBar.maxValue = pMax;
            healthBar.value = pCurrent;

            health.text = pCurrent.ToString("F0") + "/" + pMax.ToString("F0");
        }
    }
    public void UpdateMana(float pCurrent, float pMax)
    {
        if (manaBar != null && mana != null)
        {
            manaBar.maxValue = pMax;
            manaBar.value = pCurrent;

            mana.text = pCurrent.ToString("F0") + "/" + pMax.ToString("F0");
        }
    }
    public void UpdateFear(float pCurrent, float pMax)
    {
        if (fearBar != null && fear != null)
        {
            fearBar.maxValue = pMax;
            fearBar.value = pCurrent;

            fear.text = pCurrent.ToString("F0") + "/" + pMax.ToString("F0");
        }
    }

    public void UpdateXP(float pCurrent, float pMax, int pLevel)
    {
        if (fearBar != null && fear != null)
        {
            xpBar.maxValue = pMax;
            xpBar.value = pCurrent;

            xp.text = pCurrent.ToString("F0") + "/" + pMax.ToString("F0") + " - Level " + pLevel.ToString("F0");
        }
    }

    public void UpdateSkills(int pOH, int pTH, int pDW, int pMa, int pA, int pHA, int pLA, int pCA, int pH, int pM, int pHR, int pMR, int pPoints)
    {
        skills.UpdateAll(pOH, pTH, pDW, pMa, pA, pHA, pLA, pCA, pH, pM, pHR, pMR, pPoints);
    }
    public void LevelSkill(int pSkill)
    {
        Player_Manager.Instance.LevelSkill((CS_Enum.SKILL)pSkill);
    }

    public void Interact()
    {
        Player_Manager.Instance.GetPlayer().Interact();
    }
    public void AbilityOne()
    {
        Player_Manager.Instance.GetPlayer().abilities[0]();
    }
    public void AbilityTwo()
    {
        Player_Manager.Instance.GetPlayer().abilities[1]();
    }
    public void AbilityThree()
    {
        Player_Manager.Instance.GetPlayer().abilities[2]();
    }

    public void ToggleLeft(bool pToggle)
    {
        inventoryPanel.SetActive(pToggle);
        statsPanel.SetActive(pToggle);
        invBtn.SetActive(pToggle);
        spBtn.SetActive(pToggle);
    }

    public void ToggleRight(bool pToggle)
    {
        equipmentPanel.SetActive(pToggle);
        abilitiesPanel.SetActive(pToggle);
        eqBtn.SetActive(pToggle);
        abBtn.SetActive(pToggle);
    }

    public void TogglePanels(bool pToggle)
    {
        ToggleLeft(pToggle);
        ToggleRight(pToggle);
    }

    public void ToggleInventory()
    {
        ToggleNewPanel(false, inventoryPanel);
    }

    public void ToggleStats()
    {
        ToggleNewPanel(false, statsPanel);
    }

    public void ToggleEquipment()
    {
        ToggleNewPanel(true, equipmentPanel);
    }

    public void ToggleAbilities()
    {
        ToggleNewPanel(true, abilitiesPanel);
    }

    public void ToggleNewPanel(bool pSide, GameObject pPanel)
    {
        if(pPanel.activeInHierarchy)
        {
            ToggleSide(pSide);

            if(pSide)
            {
                eqBtn.SetActive(true);
                abBtn.SetActive(true);
            }
            else
            {
                invBtn.SetActive(true);
                spBtn.SetActive(true);
            }
        }
        else
        {
            ToggleSide(pSide);
            pPanel.SetActive(true);
        }
    }

    public void ToggleSide(bool pSide)
    {
        if (pSide)
        {
            ToggleRight(false);
        }
        else
        {
            ToggleLeft(false);
        }
    }

    public void ResetEquipmentPanel()
    {
        invEqu.current.ResetDisplay();
        invEqu.selected.ResetDisplay();
    }
}
