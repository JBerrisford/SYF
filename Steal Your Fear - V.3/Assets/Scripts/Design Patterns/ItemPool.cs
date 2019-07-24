using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ItemPool : MonoBehaviour, IInit
{
    public void Init()
    {

    }

    public void StartLevel()
    {
        if (Player_Manager.Instance.data.itemData.equipment.Count <= 0)
        {
            Player_Manager.Instance.GetPlayer().SetDefaults(CreateDefaults());
        }
        else
        {
            Player_Manager.Instance.GetPlayer().SetDefaults(LoadEquipment(Player_Manager.Instance.data.itemData.equipment));
            Player_Manager.Instance.GetPlayer().GetInventory().inventory = LoadInventory(Player_Manager.Instance.data.itemData.inventory);
            Player_Manager.Instance.GetPlayer().GetInventory().Init();
        }

        foreach (AI_Base ai in AI_Manager.Instance.enemies)
        {
            ai.SetDefaults(CreateDefaults());
        }

        IDrop[] drops = FindObjectsOfType<MonoBehaviour>().OfType<IDrop>().ToArray();

        foreach (IDrop temp in drops)
        {
            CreateDrop(temp);
        }        
    }

    void CreateDrop(IDrop pDropper)
    {
        string itemPath = CS_Utility.DefaultItemDirectory[Random.Range(0, CS_Utility.DefaultItemDirectory.Length)];
        IItem newItem = CreateItem(itemPath);
        newItem.ItemCreation(new CS_Struct.Item_Data(1, (int)GetRarity(), itemPath));
        pDropper.ItemDrop = newItem;
    }

    List<IItem> CreateDefaults()
    {
        List<IItem> defaultSet = new List<IItem>();

        IItem newItem;
        string path;

        for (int i = 0; i < CS_Utility.DefaultItemDirectory.Length; i++ )
        {
            path = CS_Utility.DefaultItemDirectory[i];
            newItem = CreateItem(path);
            defaultSet.Add(newItem);
            newItem.ItemCreation(new CS_Struct.Item_Data(1, (int)CS_Enum.RARITY.COMMON, path));
        }

        return defaultSet;
    }

    List<IItem> LoadEquipment(List<CS_Struct.Item_Data> data)
    {
        List<IItem> equipmentSet = new List<IItem>();
        IItem newItem;

        foreach (CS_Struct.Item_Data temp in data)
        {
            newItem = CreateItem(temp.item);
            equipmentSet.Add(newItem);
            newItem.ItemCreation(temp);
        }

        return equipmentSet;
    }

    List<IItem> LoadInventory(List<CS_Struct.Item_Data> data)
    {
        List<IItem> inventorySet = new List<IItem>();
        IItem newItem;

        foreach (CS_Struct.Item_Data temp in data)
        {
            newItem = CreateItem(temp.item);
            newItem.ItemCreation(temp);
            inventorySet.Add(newItem);
        }

        return inventorySet;
    }

    IItem CreateItem(string pDir)
    {
        IItem item;
        GameObject go;

        go = (GameObject)Instantiate(Resources.Load(pDir));
        item = go.GetComponent<IItem>();

        return item;
    }

    CS_Enum.RARITY GetRarity()
    {
        int percentage = Random.Range(1, 100);

        if (percentage >= 99)
            return CS_Enum.RARITY.FORBIDDEN;
        else if (percentage >= 90)
            return CS_Enum.RARITY.UNIQUE;
        else if (percentage >= 75)
            return CS_Enum.RARITY.RARE;
        else if (percentage >= 50)
            return CS_Enum.RARITY.UNCOMMON;
        else
            return CS_Enum.RARITY.COMMON;
    }
}
