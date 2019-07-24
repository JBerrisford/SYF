using UnityEngine;
using System.Collections;

public class Default_Shield : Item_Base
{
    public override void ItemCreation(CS_Struct.Item_Data pData)
    {
        myData = pData;
        myData.name = "Tattered Shield";
        base.ItemCreation(pData);

        Damage = 0.0f;
        Resist = 2.5f;
        AttackSpeed = -0.1f;

        Health = 5.0f;
        HealthBonus = 0.0f;
        HealthRegen = 0.0f;
        HealthRegenBonus = 0.0f;

        Mana = 0.0f;
        ManaBonus = 0.0f;
        ManaRegen = 0.0f;
        ManaRegenBonus = 0.0f;

        Slot = CS_Enum.SLOT.OFF_WEAPON;
        Stance = CS_Enum.STANCE.ONE_HANDED;
        ArmourClass = CS_Enum.ARMOUR_CLASS.OTHER;

        FinishStats(0);
    }
}
