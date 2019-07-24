using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public delegate void Overall_Manager(CS_Enum.OVERALL_STATE pNewState);
    public event Overall_Manager Overall_State_Update;
    public delegate void InGame_Manager(CS_Enum.INGAME_STATE pNewState);
    public event InGame_Manager InGame_State_Update;

    public CS_Enum.OVERALL_STATE currentOverallState;
    public CS_Enum.INGAME_STATE currentInGameState;

    Scene old;
    bool gameStart = false;

    public AudioSource AS;
    public List<AudioClip> AC = new List<AudioClip>();

    public override void Awake()
    {
        if (!gameStart)
        {
            base.Awake();
            SceneManager.activeSceneChanged += UnloadOldScene;
            Overall_State_Change(CS_Enum.OVERALL_STATE.START, -1);
            gameStart = true;
        }
        else
        {
            Destroy(this.gameObject);
        }

        AS.Play();
    }

    public void Overall_State_Change(CS_Enum.OVERALL_STATE pNewState, int pSceneIndex)
    {
        switch (pNewState)
        {
            case CS_Enum.OVERALL_STATE.START:
                // Manager Creation on Game Start
                gameObject.AddComponent<Map_Manager>();
                gameObject.AddComponent<Player_Manager>();
                gameObject.AddComponent<AI_Manager>();
                gameObject.AddComponent<UI_Manager>();
                gameObject.AddComponent<Data_Manager>();

                // Manager Initilization
                Data_Manager.Instance.Init();
                Player_Manager.Instance.Init();
                AI_Manager.Instance.Init();
                UI_Manager.Instance.Init();
                Map_Manager.Instance.Init();

                currentOverallState = CS_Enum.OVERALL_STATE.START;

                if (Overall_State_Update != null)
                    Overall_State_Update(currentOverallState);

                currentOverallState = CS_Enum.OVERALL_STATE.MAIN_MENU;

                if (Overall_State_Update != null)
                    Overall_State_Update(currentOverallState);

                break;
            case CS_Enum.OVERALL_STATE.MAIN_MENU:
                if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))
                {
                    old = SceneManager.GetActiveScene();

                    if (AS.clip != AC[0])
                    {
                        AS.clip = AC[0];
                    }

                    StartCoroutine(LoadLevel(pNewState, 0));
                }
                break;
            case CS_Enum.OVERALL_STATE.TOWN:
                if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(1))
                {
                    old = SceneManager.GetActiveScene();

                    if (AS.clip != AC[0])
                    {
                        AS.clip = AC[0];
                    }

                    StartCoroutine(LoadLevel(pNewState, 1));
                }
                break;
            case CS_Enum.OVERALL_STATE.NEW_SCENE:
                if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(pSceneIndex))
                {
                    old = SceneManager.GetActiveScene();

                    if (AS.clip != AC[1])
                    {
                        AS.clip = AC[1];
                    }

                    StartCoroutine(LoadLevel(pNewState, pSceneIndex));
                }
                break;
            case CS_Enum.OVERALL_STATE.SAVE:

                break;
            case CS_Enum.OVERALL_STATE.EXIT:

                break;
        }
    }

    public void InGame_State_Change(CS_Enum.INGAME_STATE pNewState)
    {
        switch(pNewState)
        {
            case CS_Enum.INGAME_STATE.START_LEVEL:

                break;
            case CS_Enum.INGAME_STATE.PLAYING:

                break;
            case CS_Enum.INGAME_STATE.WIN:

                break;
            case CS_Enum.INGAME_STATE.LOSE:

                break;
        }

        currentInGameState = pNewState;
        InGame_State_Update(currentInGameState);
    }

    IEnumerator LoadLevel(CS_Enum.OVERALL_STATE pNewState, int pSceneIndex)
    {
        AsyncOperation newScene = SceneManager.LoadSceneAsync(pSceneIndex, LoadSceneMode.Single);

        while (!newScene.isDone)
        {
            //UIManager.Instance.Loading(newScene.progress);
            //Debug.Log("Loading Scene... " + newScene.progress);
            yield return null;
        }

        currentOverallState = pNewState;
        Overall_State_Update(currentOverallState);

        if(pNewState != CS_Enum.OVERALL_STATE.MAIN_MENU)
            InGame_State_Change(CS_Enum.INGAME_STATE.START_LEVEL);

        if(!AS.isPlaying)
            AS.Play();

        yield break;
    }

    public void UnloadOldScene(Scene arg0, Scene arg1)
    {
        if (old.IsValid())
        {
            SceneManager.UnloadSceneAsync(old);
        }
    }
}
