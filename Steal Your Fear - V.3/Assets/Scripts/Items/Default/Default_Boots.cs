using UnityEngine;
using System.Collections;

public class Default_Boots : Item_Base
{
    public override void ItemCreation(CS_Struct.Item_Data pData)
    {
        myData = pData;
        myData.name = "Tattered Boots";
        base.ItemCreation(pData);

        Damage = 1.0f;
        Resist = 1.0f;
        AttackSpeed = 0.1f;

        Health = 0.0f;
        HealthBonus = 0.0f;
        HealthRegen = 0.0f;
        HealthRegenBonus = 0.0f;

        Mana = 0.0f;
        ManaBonus = 0.0f;
        ManaRegen = 0.0f;
        ManaRegenBonus = 0.0f;

        Slot = CS_Enum.SLOT.FEET;
        Stance = CS_Enum.STANCE.OTHER;
        ArmourClass = CS_Enum.ARMOUR_CLASS.CLOTH;

        FinishStats(0);
    }
}
