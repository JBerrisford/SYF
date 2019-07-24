using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : Singleton<Player_Manager>, IInit
{
    public delegate void Stat_Update(float pCurrent, float pMax);
    public delegate void XP_Update(float pCurrent, float pMax, int pLevel);

    public event Stat_Update HealthUpdate;
    public event Stat_Update ManaUpdate;
    public event Stat_Update FearUpdate;
    public event XP_Update XPUpdate;

    Player_Base player;
    public CameraScript playerCamera;

    public CS_Struct.Character_Data data;

    public virtual void Init()
    {
        GameManager.Instance.Overall_State_Update += Overall_State_Change;
        GameManager.Instance.InGame_State_Update += InGame_State_Change;
    }

    public void StartLevel()
    {
        if (player == null)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load(CS_Utility.player));
            player = go.GetComponent<Player_Base>();
            player.data = data;
            player.Init();
            DontDestroyOnLoad(player);
        }

        playerCamera = FindObjectOfType<CameraScript>();
        playerCamera.Init();
    }

    public void SpawnPoint(GameObject go)
    {
        player.transform.position = new Vector3(go.transform.position.x, 0.0f, go.transform.position.z);
    }

    public void HealthChange(float pCurrent, float pMax)
    {
        if (HealthUpdate != null)
        {
            HealthUpdate(pCurrent, pMax);
        }
    }

    public void ResetValues()
    {
        if(HealthUpdate == null)
            HealthUpdate = null;

        if (ManaUpdate == null)
            ManaUpdate = null;

        if (FearUpdate == null)
            FearUpdate = null;

        if (XPUpdate == null)
            XPUpdate = null;

        if (player == null)
            player = null;

        data = new CS_Struct.Character_Data();
    }

    public void ManaChange(float pCurrent, float pMax)
    {
        if (ManaUpdate != null)
        {
            ManaUpdate(pCurrent, pMax);
        }
    }

    public void FearChange(float pCurrent, float pMax)
    {
        if (FearUpdate != null)
        {
            FearUpdate(pCurrent, pMax);
        }
    }

    public void XPChange()
    {
        if (XPUpdate != null)
        {
            XPUpdate(player.GetSkills().experience, player.GetSkills().experienceMax, player.GetSkills().level);
        }
    }

    public void AddXP(int pXP)
    {
        player.AddXP(pXP);
    }

    public void LevelSkill(CS_Enum.SKILL pSkill)
    {
        player.GetSkills().IncreaseSkill(pSkill);
    }

    public void DropItem(int pIndex)
    {
        player.Drop(pIndex);
    }

    public void EquipItem(int pIndex)
    {
        player.GetInventory().EquipItem(pIndex);
    }

    public void Overall_State_Change(CS_Enum.OVERALL_STATE pNewState)
    {
        switch (pNewState)
        {
            case CS_Enum.OVERALL_STATE.START:
                ResetValues();
                break;
            case CS_Enum.OVERALL_STATE.MAIN_MENU:
                ResetValues();
                break;
            case CS_Enum.OVERALL_STATE.TOWN:

                break;
            case CS_Enum.OVERALL_STATE.NEW_SCENE:

                break;
            case CS_Enum.OVERALL_STATE.SAVE:

                break;
            case CS_Enum.OVERALL_STATE.EXIT:

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
            case CS_Enum.INGAME_STATE.PLAYING:
                player.UpdateAllStats();
                player.Save();
                break;
            case CS_Enum.INGAME_STATE.WIN:

                break;
            case CS_Enum.INGAME_STATE.LOSE:

                break;
        }
    }

    public void Death()
    {
        Data_Manager.Instance.DeletePlayer(data.saveIndex);
        GameManager.Instance.Overall_State_Change(CS_Enum.OVERALL_STATE.MAIN_MENU, 0);
        Destroy(player.gameObject);
    }

    public GameObject GetPlayerGO()
    {
        return player.gameObject;
    }

    public Player_Base GetPlayer()
    {
        return player;
    }
}
