using UnityEngine;
using System.Collections.Generic;

using CS_Enum;

public class Character_Mounting_Points : Character_Component_Base, ICharacter
{
    Dictionary<SLOT, Transform> mountingPoints = new Dictionary<SLOT, Transform>();
    Transform inventoryTransform;

    public void Init()
    {
        mountingPoints[SLOT.HEAD] = transform.Find("Head_Point");
        mountingPoints[SLOT.BACK] = transform.Find("Back_Point");
        mountingPoints[SLOT.CHEST] = transform.Find("Chest_Point");
        mountingPoints[SLOT.ARMS] = transform.Find("Arms_Point");
        mountingPoints[SLOT.LEGS] = transform.Find("Legs_Point");
        mountingPoints[SLOT.FEET] = transform.Find("Feet_Point");
        mountingPoints[SLOT.MAIN_WEAPON] = transform.Find("Main_Point");
        mountingPoints[SLOT.OFF_WEAPON] = transform.Find("Off_Point");
        inventoryTransform = transform.Find("Inventory");
    }

    public void AddItemToPoint(IItem pItem)
    {
        pItem.SetParent(mountingPoints[pItem.Slot]);
    }

    public Transform GetInventoryTransform()
    {
        return inventoryTransform;
    }
}
