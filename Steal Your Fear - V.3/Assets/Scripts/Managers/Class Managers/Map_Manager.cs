using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map_Manager : Singleton<Map_Manager>, IInit
{
    ItemPool itemPool;
    public CS_Enum.CHANGE_ZONE current;
    public CS_Enum.CHANGE_ZONE old;

    public Dictionary<CS_Enum.CHANGE_ZONE, GameObject> sceneChangers = new Dictionary<CS_Enum.CHANGE_ZONE, GameObject>();
    
    // public Vector3 GetItemOffset()

    public virtual void Init()
    {
        GameManager.Instance.Overall_State_Update += Overall_State_Change;
        GameManager.Instance.InGame_State_Update += InGame_State_Change;
        current = CS_Enum.CHANGE_ZONE.TOWN;
        old = CS_Enum.CHANGE_ZONE.F_ONE;
    }

    public void StartLevel()
    {
        itemPool.StartLevel();

        SceneChanger[] go = FindObjectsOfType<SceneChanger>();
        sceneChangers.Clear();

        foreach (SceneChanger temp in go)
        {
            sceneChangers.Add(temp.ZoneType, temp.gameObject);
        }

        MaskControl[] maskControls = FindObjectsOfType<MaskControl>();

        foreach(MaskControl mc in maskControls)
        {
            mc.Init();
            mc.UpdateMask(Player_Manager.Instance.GetPlayer().fear.current);
        }

        Player_Manager.Instance.SpawnPoint(sceneChangers[old]);
        GameManager.Instance.InGame_State_Change(CS_Enum.INGAME_STATE.PLAYING);
    }

    public void SetNewZone(CS_Enum.CHANGE_ZONE pZone)
    {
        old = current;
        current = pZone;
    }

    public Vector3 GetItemOffset()
    {
        return Vector3.up * -100.0f;
    }

    public void Overall_State_Change(CS_Enum.OVERALL_STATE pNewState)
    {
        switch (pNewState)
        {
            case CS_Enum.OVERALL_STATE.START:
                GameObject go = (GameObject)Instantiate(Resources.Load(CS_Utility.itemPool));
                itemPool = go.GetComponent<ItemPool>();
                itemPool.transform.parent = transform;
                break;
            case CS_Enum.OVERALL_STATE.MAIN_MENU:

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

                break;
            case CS_Enum.INGAME_STATE.WIN:

                break;
            case CS_Enum.INGAME_STATE.LOSE:

                break;
        }
    }
}
