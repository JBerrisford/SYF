using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class Data_Manager : Singleton<Data_Manager>
{
    public CS_Struct.Character_Data[] charData = new CS_Struct.Character_Data[4];

    public void Init()
    {
        for(int i = 0; i <= 3; i++)
        {string path = "Character " + i;
            
            if (File.Exists(Path.Combine(Application.persistentDataPath, path)))
            {
                charData[i] = Load(i);
            }
            else
            {
                charData[i] = GetDefault(i);
                Save(charData[i]);
            }
        }
    }

    public void Save(CS_Struct.Character_Data data)
    {
        string characterIndex = "Character " + data.saveIndex.ToString();
        string path = Path.Combine(Application.persistentDataPath, characterIndex);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            string json = JsonUtility.ToJson(data);
            streamWriter.Write(json);
        }
    }

    public CS_Struct.Character_Data Load(int index)
    {
        CS_Struct.Character_Data newTemp = new CS_Struct.Character_Data();
        string path = "Character " + index.ToString();

        using (StreamReader streamReader = File.OpenText(Path.Combine(Application.persistentDataPath, path)))
        {
            string jsonString = streamReader.ReadToEnd();
            newTemp = JsonUtility.FromJson<CS_Struct.Character_Data>(jsonString);
        }

        return newTemp;
    }

    public void DeletePlayer(int index)
    {
        string path = "Character " + index.ToString();

        File.Delete(Path.Combine(Application.persistentDataPath, path));
        charData[index] = GetDefault(index);
        Save(charData[index]);

        UI_Manager.Instance.main_menu.UpdateText();
    }

    CS_Struct.Character_Data GetDefault(int index)
    {
        CS_Struct.Character_Data charData = new CS_Struct.Character_Data();
        charData.name = "-/-";
        charData.saveIndex = index;
        charData.itemData = new CS_Struct.Inventory_Data(new List<CS_Struct.Item_Data>(), new List<CS_Struct.Item_Data>());
        charData.skillData = new CS_Struct.Skill_Data(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0);
        return charData;
    }
}