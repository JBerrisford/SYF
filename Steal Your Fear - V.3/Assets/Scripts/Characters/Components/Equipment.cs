using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using CS_Enum;
using CS_Struct;

public class Equipment : Character_Component_Base, IInit
{
    Dictionary<SLOT, IItem> defaultEquipment = new Dictionary<SLOT, IItem>();
    public Dictionary<SLOT, IItem> currentEquipment = new Dictionary<SLOT, IItem>();

    public List<string> display = new List<string>();

    public void Init()
    {

    }

    public void SetDefaults(List<IItem> pNewDefaults)
    {
        foreach (IItem item in pNewDefaults)
        {
            defaultEquipment[item.Slot] = item;
            EquipItem(item);
        }
    }

    // Get the accumulated value of all the items that are currently equipt.
    public float GetDamage()
    {
        float damage = 0.0f;

        foreach(IItem item in currentEquipment.Values)
        {
            damage += item.Damage;
        }

        return damage;
    }
    public float GetResistance()
    {
        float resist = 0.0f;

        foreach (IItem item in currentEquipment.Values)
        {
            resist += item.Resist;
        }

        return resist;
    }
    public ARMOUR_CLASS GetArmourClass()
    {
        ARMOUR_CLASS armourClass = ARMOUR_CLASS.CLOTH;

        int h = 0;
        int l = 0;
        int c = 0;

        foreach(IItem item in currentEquipment.Values)
        {
            switch(item.ArmourClass)
            {
                case ARMOUR_CLASS.HEAVY:
                    h++;
                    break;
                case ARMOUR_CLASS.LIGHT:
                    l++;
                    break;
                case ARMOUR_CLASS.CLOTH:
                    c++;
                    break;
            }
        }

        if (h > l && h > c)
        {
            armourClass = ARMOUR_CLASS.HEAVY;
        }
        else if (l > h && l > c)
        {
            armourClass = ARMOUR_CLASS.LIGHT;
        }
        else if (c > h && c > l)
        {
            armourClass = ARMOUR_CLASS.CLOTH;
        }
        else
        {
            switch (character.GetSkills().GetHighestArmourSkill())
            {
                case ARMOUR_CLASS.HEAVY:
                    if (h >= l && h >= c)
                    {
                        return ARMOUR_CLASS.HEAVY;
                    }
                    else
                    {
                        return currentEquipment[SLOT.CHEST].ArmourClass;
                    }
                case ARMOUR_CLASS.LIGHT:
                    if (l >= h && l >= c)
                    {
                        return ARMOUR_CLASS.LIGHT;
                    }
                    else
                    {
                        return currentEquipment[SLOT.CHEST].ArmourClass;
                    }
                case ARMOUR_CLASS.CLOTH:
                    if (c >= h && c >= l)
                    {
                        return ARMOUR_CLASS.CLOTH;
                    }
                    else
                    {
                        return currentEquipment[SLOT.CHEST].ArmourClass;
                    }
            }
        }

        return armourClass;
    }
    public float GetAttackSpeed()
    {
        float attackSpeed = 0.0f;

        foreach (IItem item in currentEquipment.Values)
        {
            attackSpeed += item.AttackSpeed;
        }

        return attackSpeed;
    }
    public float GetHealth()
    {
        float health = 0.0f;

        foreach (IItem item in currentEquipment.Values)
        {
            health += item.Health;
        }

        return health;
    }
    public float GetHealthBonus()
    {
        float healthBonus = 0.0f;

        foreach (IItem item in currentEquipment.Values)
        {
            healthBonus += item.HealthBonus;
        }

        return healthBonus;
    }
    public float GetMana()
    {
        float mana = 0.0f;

        foreach (IItem item in currentEquipment.Values)
        {
            mana += item.Mana;
        }

        return mana;
    }
    public float GetManaBonus()
    {
        float manaBonus = 0.0f;

        foreach (IItem item in currentEquipment.Values)
        {
            manaBonus += item.ManaBonus;
        }

        return manaBonus;
    }
    public float GetHealthRegen()
    {
        float healthRegen = 0.0f;

        foreach (IItem item in currentEquipment.Values)
        {
            healthRegen += item.HealthRegen;
        }

        return healthRegen;
    }
    public float GetHealthRegenBonus()
    {
        float healthRegenBonus = 0.0f;

        foreach (IItem item in currentEquipment.Values)
        {
            healthRegenBonus += item.HealthRegenBonus;
        }

        return healthRegenBonus;
    }
    public float GetManaRegen()
    {
        float manaRegen = 0.0f;

        foreach (IItem item in currentEquipment.Values)
        {
            manaRegen += item.ManaRegen;
        }

        return manaRegen;
    }
    public float GetManaRegenBonus()
    {
        float manaRegenBonus = 0.0f;

        foreach (IItem item in currentEquipment.Values)
        {
            manaRegenBonus += item.ManaRegenBonus;
        }

        return manaRegenBonus;
    }

    public void EquipItem(IItem pItem)
    {
        if (currentEquipment.ContainsKey(pItem.Slot))
        {
            IItem old = currentEquipment[pItem.Slot];

            if(old.Stance == STANCE.TWO_HANDED)
            {
                if(pItem.Slot == SLOT.MAIN_WEAPON)
                {
                    currentEquipment[SLOT.OFF_WEAPON] = defaultEquipment[SLOT.OFF_WEAPON];
                }
                else if (pItem.Slot == SLOT.OFF_WEAPON)
                {
                    currentEquipment[SLOT.MAIN_WEAPON] = defaultEquipment[SLOT.MAIN_WEAPON];
                }
            }

            if(display.Contains(old.Name))
            {
                display.Remove(old.Name);
            }

            RemoveItem(currentEquipment[pItem.Slot]);
        }

        pItem.Equip();
        currentEquipment[pItem.Slot] = pItem;
        Display(pItem);

        if (pItem.Stance == STANCE.TWO_HANDED)
        {
            currentEquipment[SLOT.OFF_WEAPON] = pItem;
        }

        if (currentEquipment.ContainsKey(SLOT.OFF_WEAPON))
        {
            character.stance = currentEquipment[SLOT.OFF_WEAPON].Stance;
        }

        character.SetParent(pItem);
        character.armourClass = GetArmourClass();
        character.UpdateAllStats();
    }

    void RemoveItem(IItem pItem)
    {
        pItem.ToggleMesh(false);
        character.SetInventoryParent(pItem);
    }

    void Display(IItem pItem)
    {
        display.Add(pItem.Name);
    }

    public IItem GetMain()
    {
        return currentEquipment[SLOT.MAIN_WEAPON];
    }

    public IItem GetOff()
    {
        return currentEquipment[SLOT.OFF_WEAPON];
    }

    public IItem GetItem(SLOT pSlot)
    {
        return currentEquipment[pSlot];
    }

    public void ResetDefault()
    {

    }
}
