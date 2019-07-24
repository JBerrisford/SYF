using UnityEngine;
using System.Collections;

public class Default_Chest : Item_Base
{
    public override void ItemCreation(CS_Struct.Item_Data pData)
    {
        myData = pData;
        myData.name = "Tattered Chest";
        base.ItemCreation(pData);

        Damage = 0.0f;
        Resist = 5.0f;
        AttackSpeed = -0.1f;

        Health = 10.0f;
        HealthBonus = 1.0f;
        HealthRegen = 0.0f;
        HealthRegenBonus = 0.0f;

        Mana = 0.0f;
        ManaBonus = 0.0f;
        ManaRegen = 0.0f;
        ManaRegenBonus = 0.0f;

        Slot = CS_Enum.SLOT.CHEST;
        Stance = CS_Enum.STANCE.OTHER;
        ArmourClass = CS_Enum.ARMOUR_CLASS.CLOTH;

        FinishStats(0);
    }
}
