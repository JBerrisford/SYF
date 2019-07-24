using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{
    public GameObject LoadScreen;
    public Text[] text;

    public void ToggleAll(bool pToggle)
    {
        gameObject.SetActive(pToggle);
    }

    public void LoadNewScene(int pIndex)
    {
        GameManager.Instance.Overall_State_Change(CS_Enum.OVERALL_STATE.TOWN, pIndex);
    }

    public void LoadPlayer(int pIndex)
    {
        Player_Manager.Instance.data = Data_Manager.Instance.Load(pIndex);
        LoadNewScene(1);
    }

    public void DeletePlayer(int pIndex)
    {
        Data_Manager.Instance.DeletePlayer(pIndex);
    }

    public void ToggleLoad()
    {
        if(LoadScreen.activeInHierarchy)
        {
            LoadScreen.SetActive(false);
        }
        else
        {
            LoadScreen.SetActive(true);
        }

        UpdateText();
    }

    public void UpdateText()
    {
        for(int i = 0; i < text.Length; i++)
        {
            text[i].text = Data_Manager.Instance.charData[i].name;
        }
    }

    public void Exit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
