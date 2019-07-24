using UnityEngine;
using System.Collections;

using CS_Struct;
using CS_Enum;

public class Skills : Character_Component_Base
{
    public Skill_Data data;
    int experienceMax;

    public void LoadCharacter(Skill_Data pData)
    {
        data = pData;
        experienceMax = GetMax();
    }

    public Skill_Data GetSkillData()
    {
        return data;
    }

    public void IncreaseExperience(int pAmount)
    {
        data.experience += pAmount;
        LevelCheck();
    }

    public void LevelCheck()
    {
        if(data.experience >= experienceMax)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        data.points += 5;
        data.level++;
        data.experience = 0;
        experienceMax = GetMax();
    }

    int GetMax()
    {
        int newMax = 50;

        for(int i = 0; i < data.level; i++)
        {
            newMax *= 2;
        }

        return newMax;
    }

    public void IncreaseSkill(CS_Enum.SKILL pSkill)
    {
        if (data.points > 0)
        {
            switch (pSkill)
            {
                case CS_Enum.SKILL.ONE:
                    data.oneHanded++;
                    break;
                case CS_Enum.SKILL.TWO:
                    data.twoHanded++;
                    break;
                case CS_Enum.SKILL.DUAL:
                    data.dualWield++;
                    break;
                case CS_Enum.SKILL.MAGIC:
                    data.magic++;
                    break;
                case CS_Enum.SKILL.ARCHERY:
                    data.archery++;
                    break;
                case CS_Enum.SKILL.HEAVY:
                    data.heavyArmour++;
                    break;
                case CS_Enum.SKILL.LIGHT:
                    data.lightArmour++;
                    break;
                case CS_Enum.SKILL.CLOTH:
                    data.clothArmour++;
                    break;
                case CS_Enum.SKILL.HEALTH:
                    data.health++;
                    break;
                case CS_Enum.SKILL.HEATLH_REGEN:
                    data.healthRegen++;
                    break;
                case CS_Enum.SKILL.MANA:
                    data.mana++;
                    break;
                case CS_Enum.SKILL.MANA_REGEN:
                    data.manaRegen++;
                    break;
            }

            data.points--;
            character.UpdateAllStats();
        }
    }

    public void ResetDefault()
    {

    }

    public float GetOneBonus()
    {
        return data.oneHanded / 10.0f;
    }

    public float GetTwoBonus()
    {
        return data.twoHanded / 10.0f;
    }

    public float GetDualBonus()
    {
        return data.dualWield / 10.0f;
    }

    public float GetMagicBonus()
    {
        return data.magic / 10.0f;
    }

    public float GetArcheryBonus()
    {
        return data.archery / 10.0f;
    }

    public float GetHeavyBonus()
    {
        return data.heavyArmour / 10.0f;
    }

    public float GetLightBonus()
    {
        return data.lightArmour / 10.0f;
    }

    public float GetClothBonus()
    {
        return data.clothArmour / 10.0f;
    }

    public float GetHealthBonus()
    {
        return data.health / 10.0f;
    }

    public float GetHealthRegenBonus()
    {
        return data.healthRegen / 10.0f;
    }

    public float GetManaBonus()
    {
        return data.mana / 10.0f;
    }

    public float GetManaRegenBonus()
    {
        return data.manaRegen / 10.0f;
    }

    public ARMOUR_CLASS GetHighestArmourSkill()
    {
        if (data.lightArmour >= data.heavyArmour && data.lightArmour >= data.clothArmour)
        {
            return ARMOUR_CLASS.LIGHT;
        }
        else if (data.heavyArmour >= data.lightArmour && data.heavyArmour >= data.clothArmour)
        {
            return ARMOUR_CLASS.HEAVY;
        }
        else
        {
            return ARMOUR_CLASS.CLOTH;
        }
    }
}
