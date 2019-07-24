using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using CS_Struct;
using CS_Enum;

[System.Serializable]
public class Inventory : Character_Component_Base, IInit
{
    [SerializeField]
    public List<IItem> inventory = new List<IItem>();
    public int inventoryCapacity = 20;

    public List<IItem> currentEquipment = new List<IItem>();

    public Equipment equipment;

    public void Init()
    {
        if (inventory.Count > 0)
        {

            foreach (IItem temp in inventory)
            {
                temp.ToggleMesh(false);
                character.SetInventoryParent(temp);
            }
        }

        UI_Manager.Instance.InventoryChange();
    }

    public void SetCurrent(Equipment pCurrent)
    {
        equipment = pCurrent;

        foreach (IItem temp in equipment.currentEquipment.Values)
        {
            currentEquipment.Add(temp);
        }
    }

    public void AddItem(IItem pNewItem)
    {
        if (inventory.Count < inventoryCapacity)
        {
            inventoryCapacity++;
            inventory.Add(pNewItem);
            pNewItem.ToggleMesh(false);
            character.SetInventoryParent(pNewItem);
            UI_Manager.Instance.InventoryChange();
        }
        else
        {
            Debug.Log("Failed To Add Item - Inventory Full");
        }

        Player_Manager.Instance.GetPlayer().Save();
    }
    public void RemoveItem(IItem pOldItem)
    {
        if (inventory.Contains(pOldItem))
        {
            inventory.Remove(pOldItem);
            inventoryCapacity--;
            UI_Manager.Instance.InventoryChange();
        }
    }

    public void EquipItem(int pIndex)
    {
        IItem newItem = inventory[pIndex];
        IItem oldItem = equipment.GetItem(newItem.Slot);

        RemoveItem(newItem);
        currentEquipment.Add(newItem);

        AddItem(oldItem);
        currentEquipment.Remove(oldItem);

        equipment.EquipItem(newItem);
        Player_Manager.Instance.GetPlayer().Save();
        UI_Manager.Instance.InventoryChange();
    }

    public IItem DropItem(int pIndex)
    {
        IItem item = inventory[pIndex];
        RemoveItem(inventory[pIndex]);

        Player_Manager.Instance.GetPlayer().Save();
        return item;
    }

    public Inventory_Data GetItemData()
    {
        List<Item_Data> inventoryID = new List<Item_Data>();
        List<Item_Data> equipmentID = new List<Item_Data>();

        SetCurrent(equipment);

        foreach(IItem temp in inventory)
        {
            inventoryID.Add(temp.GetData());
        }

        foreach (IItem temp in currentEquipment)
        {
            equipmentID.Add(temp.GetData());
        }

        Inventory_Data newData = new Inventory_Data(inventoryID, equipmentID);
        return newData;
    }
}

