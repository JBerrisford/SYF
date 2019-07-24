using UnityEngine;
using System.Collections;

[System.Serializable]
public class Default_Sword : Item_Base
{
    public override void ItemCreation(CS_Struct.Item_Data pData)
    {
        myData = pData;
        myData.name = "Tattered Sword";
        base.ItemCreation(pData);

        Damage = 5.0f;
        Resist = 0.5f;
        AttackSpeed = 0.75f;

        Health = 0.0f;
        HealthBonus = 0.0f;
        HealthRegen = 0.0f;
        HealthRegenBonus = 0.0f;

        Mana = 0.0f;
        ManaBonus = 0.0f;
        ManaRegen = 0.0f;
        ManaRegenBonus = 0.0f;

        /*Health = Random.Range(0, 5);
        HealthBonus = Random.Range(0, 2.5f);

        Mana = Random.Range(0, 10);
        ManaBonus = Random.Range(0, 2);*/

        Slot = CS_Enum.SLOT.MAIN_WEAPON;
        Stance = CS_Enum.STANCE.ONE_HANDED;
        ArmourClass = CS_Enum.ARMOUR_CLASS.OTHER;

        FinishStats(0);
    }
}
