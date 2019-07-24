using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : Singleton<UI_Manager>, IInit
{
    public Canvas ui;

    public Main_Menu main_menu;
    public InGame_GUI inGame_ui;
    public EquipmentPanel invEqu_UI;

    public virtual void Init()
    {
        GameManager.Instance.Overall_State_Update += Overall_State_Change;
        GameManager.Instance.InGame_State_Update += InGame_State_Change;
    }

    public void StartLevel()
    {
        ToggleAll(false);
        SubscribeToEvents();
        inGame_ui.ToggleAll(true);
    }

    void SubscribeToEvents()
    {
        Player_Manager.Instance.HealthUpdate += HealthUpdate;
        Player_Manager.Instance.ManaUpdate += ManaUpdate;
        Player_Manager.Instance.FearUpdate += FearUpdate;
        Player_Manager.Instance.XPUpdate += XPUpdate;
    }

    public void HealthUpdate(float pCurrent, float pMax)
    {
        inGame_ui.UpdateHealth(pCurrent, pMax);
    }

    public void ManaUpdate(float pCurrent, float pMax)
    {
        inGame_ui.UpdateMana(pCurrent, pMax);
    }

    public void FearUpdate(float pCurrent, float pMax)
    {
        inGame_ui.UpdateFear(pCurrent, pMax);
    }

    public void XPUpdate(float pCurrent, float pMax, int pLevel)
    {
        inGame_ui.UpdateXP(pCurrent, pMax, pLevel);
    }

    public void InventoryChange()
    {
        invEqu_UI.InventoryUpdate();
    }

    public void Overall_State_Change(CS_Enum.OVERALL_STATE pNewState)
    {
        switch (pNewState)
        {
            case CS_Enum.OVERALL_STATE.START:
                GameObject go = (GameObject)Instantiate(Resources.Load(CS_Utility.ui));
                ui = go.GetComponent<Canvas>();
                ui.transform.SetParent(transform);
                main_menu = ui.transform.Find("MainMenu").gameObject.GetComponent<Main_Menu>();
                inGame_ui = ui.transform.Find("InGame").gameObject.GetComponent<InGame_GUI>();
                invEqu_UI = inGame_ui.invEqu;
                inGame_ui.ResetEquipmentPanel();
                ToggleAll(false);
                break;
            case CS_Enum.OVERALL_STATE.MAIN_MENU:
                ToggleAll(false);
                main_menu.ToggleAll(true);
                break;
            case CS_Enum.OVERALL_STATE.TOWN:
                ToggleAll(false);

                break;
            case CS_Enum.OVERALL_STATE.NEW_SCENE:

                break;
            case CS_Enum.OVERALL_STATE.SAVE:
                ToggleAll(false);
                break;
        }

    }

    public void InGame_State_Change(CS_Enum.INGAME_STATE pNewState)
    {
        switch (pNewState)
        {
            case CS_Enum.INGAME_STATE.START_LEVEL:
                StartLevel();
                break;
            case CS_Enum.INGAME_STATE.WIN:

                break;
            case CS_Enum.INGAME_STATE.LOSE:

                break;
        }
    }

    public void ToggleAll(bool pToggle)
    {
        main_menu.ToggleAll(pToggle);
        inGame_ui.ToggleAll(pToggle);
    }
}
