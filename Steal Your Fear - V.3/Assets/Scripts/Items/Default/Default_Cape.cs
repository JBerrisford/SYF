using UnityEngine;
using System.Collections;

public class Default_Cape : Item_Base
{
    public override void ItemCreation(CS_Struct.Item_Data pData)
    {
        myData = pData;
        myData.name = "Tattered Cape";
        base.ItemCreation(pData);

        Damage = 0.0f;
        Resist = 0.0f;
        AttackSpeed = 0.0f;

        Health = 5.0f;
        HealthBonus = 0.5f;
        HealthRegen = 1.0f;
        HealthRegenBonus = 0.0f;

        Mana = 5.0f;
        ManaBonus = 0.5f;
        ManaRegen = 1.0f;
        ManaRegenBonus = 0.0f;

        Slot = CS_Enum.SLOT.BACK;
        Stance = CS_Enum.STANCE.OTHER;
        ArmourClass = CS_Enum.ARMOUR_CLASS.CLOTH;

        FinishStats(0);
    }
}
