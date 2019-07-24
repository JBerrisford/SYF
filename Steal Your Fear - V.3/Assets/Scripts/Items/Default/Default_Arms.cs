using UnityEngine;
using System.Collections;

public class Default_Arms : Item_Base
{
    public override void ItemCreation(CS_Struct.Item_Data pData)
    {
        myData = pData;
        myData.name = "Tattered Arms";
        base.ItemCreation(pData);

        Damage = 0.0f;
        Resist = 2.0f;
        AttackSpeed = 0.2f;

        Health = 5.0f;
        HealthBonus = 0.25f;
        HealthRegen = 0.0f;
        HealthRegenBonus = 0.0f;

        Mana = 0.0f;
        ManaBonus = 0.0f;
        ManaRegen = 0.0f;
        ManaRegenBonus = 0.0f;

        Slot = CS_Enum.SLOT.ARMS;
        Stance = CS_Enum.STANCE.OTHER;
        ArmourClass = CS_Enum.ARMOUR_CLASS.CLOTH;

        FinishStats(0);
    }
}
