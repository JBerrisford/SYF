using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Manager : Singleton<AI_Manager>, IInit
{
    public Spawner[] spawners;
    public List<AI_Base> enemies = new List<AI_Base>();

    public virtual void Init()
    {
        GameManager.Instance.Overall_State_Update += Overall_State_Change;
        GameManager.Instance.InGame_State_Update += InGame_State_Change;
    }

    public void StartLevel()
    {
        spawners = FindObjectsOfType<Spawner>();

        foreach (Spawner spawner in spawners)
        {
            enemies.Add(spawner.SpawnEnemies());
        }

        foreach(AI_Base enemy in enemies)
        {
            enemy.Init();
        }
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
                ResetValues();
                break;
            case CS_Enum.OVERALL_STATE.NEW_SCENE:
                ResetValues();
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

    public void ResetValues()
    {
        enemies.Clear();
    }
}

