using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using CS_Enum;
using CS_Struct;

[System.Serializable]
public abstract class Character_Base : Target, IDamage
{
    public STANCE stance;
    public ARMOUR_CLASS armourClass;

    [SerializeField]
    protected CS_Stat health;
    [SerializeField]
    protected CS_Stat mana;

    protected Equipment equipment;
    protected Skills skills;
    protected Character_Mounting_Points mountingPoints;

    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float resist;
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    protected float health_base;
    [SerializeField]
    protected float health_bonus;

    [SerializeField]
    protected float health_regen_base;
    [SerializeField]
    protected float health_regen_bonus;

    [SerializeField]
    protected float mana_base;
    [SerializeField]
    protected float mana_bonus;

    [SerializeField]
    protected float mana_regen_base;
    [SerializeField]
    protected float mana_regen_bonus;
    [SerializeField]
    private bool isAlive;
    public bool IsAlive
    { get { return isAlive; } set { isAlive = value; } }

    public GameObject MeshObj;

    public override void Init()
    {
        base.Init();

        EquipmentSetUp();
        SkillSetUp();
        MountingPointSetup();
    }

    public void UpdateCombat() // REWORK (Too Bulky)
    {
        damage = 0.0f;
        damage += equipment.GetDamage();
        damage += (GetSkillBonus(stance) * 10) / 2.0f;

        resist = 0.0f;
        resist += equipment.GetResistance();
        resist += GetArmourBonus(armourClass) * 2;

        attackSpeed = 0.0f;
        attackSpeed += equipment.GetAttackSpeed();

        health_base = 50.0f;
        health_base += equipment.GetHealth();
        health_base += 50.0f * GetOtherBonus(OTHER.HEALTH);
        health_bonus = 0.0f;
        health_bonus += equipment.GetHealthBonus();
        health_bonus += GetOtherBonus(OTHER.HEALTH);

        health_regen_base = 0.0f;
        health_regen_base += equipment.GetHealthRegen();
        health_regen_base += GetOtherBonus(OTHER.HEALTH_REGEN);
        health_regen_bonus = 0.0f;
        health_regen_bonus += equipment.GetHealthRegenBonus();
        health_regen_bonus += GetOtherBonus(OTHER.HEALTH_REGEN);

        mana_base = 50.0f;
        mana_base += equipment.GetMana();
        mana_base += 50.0f * GetOtherBonus(OTHER.MANA);
        mana_bonus = 0.0f;
        mana_bonus += equipment.GetManaBonus();
        mana_bonus += GetOtherBonus(OTHER.MANA);

        mana_regen_base = 1.0f;
        mana_regen_base += equipment.GetManaRegen();
        mana_regen_base += GetOtherBonus(OTHER.MANA_REGEN);
        mana_regen_bonus = 0.0f;
        mana_regen_bonus += equipment.GetManaRegenBonus();
        mana_regen_bonus += GetOtherBonus(OTHER.MANA_REGEN);

        health.UpdateStat(health_base, health_bonus, health_regen_base, health_regen_bonus);
        mana.UpdateStat(mana_base, mana_bonus, mana_regen_base, mana_regen_bonus);
    }

    float GetSkillBonus(STANCE pStance)
    {
        switch(pStance)
        {
            case STANCE.ONE_HANDED:
                return skills.GetOneBonus();
            case STANCE.TWO_HANDED:
                return skills.GetTwoBonus();
            case STANCE.DUAL_WIELD:
                return skills.GetDualBonus();
            case STANCE.MAGIC:
                return skills.GetMagicBonus();
            case STANCE.ARCHERY:
                return skills.GetArcheryBonus();
            default:
                return 0.0f;
        }
    }

    float GetArmourBonus(ARMOUR_CLASS pArmour)
    {
        switch (pArmour)
        {
            case ARMOUR_CLASS.HEAVY:
                return skills.GetHeavyBonus();
            case ARMOUR_CLASS.LIGHT:
                return skills.GetLightBonus();
            case ARMOUR_CLASS.CLOTH:
                return skills.GetClothBonus();
            default:
                return 0.0f;
        }
    }

    float GetOtherBonus(OTHER pOther)
    {
        switch(pOther)
        {
            case OTHER.HEALTH:
                return skills.GetHealthBonus();
            case OTHER.HEALTH_REGEN:
                return skills.GetHealthRegenBonus();
            case OTHER.MANA:
                return skills.GetManaBonus();
            case OTHER.MANA_REGEN:
                return skills.GetManaRegenBonus();
            default:
                return 0.0f;
        }
    }

    public void SetDefaults(List<IItem> defaults)
    {
        equipment.SetDefaults(defaults);
    }

    public virtual bool TakeDamage(float pDamage)
    {
        pDamage = pDamage - (pDamage / 100.0f * resist);

        if (health.ReduceStat(pDamage))
        {
            Debug.Log("OUCH!");
            DamageTaken(2.0f);
            return true;
        }
        else
        {
            Death();
            return false;
        }
    }

    public virtual void DamageTaken(float pDamage)
    {

    }

    public void EquipItem(IItem pItem)
    {
        equipment.EquipItem(pItem);
    }

    void EquipmentSetUp()
    {
        equipment = gameObject.GetComponent<Equipment>();

        if (equipment == null)
        {
            equipment = gameObject.AddComponent<Equipment>();
        }

        equipment.SetCharacter(this);
        equipment.Init();
    }

    public virtual void SkillSetUp()
    {
        skills = gameObject.GetComponent<Skills>();

        if (skills == null)
        {
            skills = gameObject.AddComponent<Skills>();
        }

        skills.SetCharacter(this);
        skills.LoadCharacter(new Skill_Data(1,1,1,1,1,1,1,1,1,1,1,1,1,1,0));
    }

    void MountingPointSetup()
    {
        mountingPoints = gameObject.transform.Find("Character Mounting Points").gameObject.GetComponent<Character_Mounting_Points>();

        if (mountingPoints == null)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load(CS_Utility.mounting));
            go.transform.parent = transform;
            mountingPoints = go.GetComponent<Character_Mounting_Points>();
        }

        mountingPoints.SetCharacter(this);
        mountingPoints.Init();
    }

    public Skills GetSkills()
    {
        return skills;
    }

    public void SetParent(IItem pItem)
    {
        mountingPoints.AddItemToPoint(pItem);
    }

    public void SetInventoryParent(IItem pItem)
    {
        pItem.SetParent(mountingPoints.GetInventoryTransform());
    }

    void SetStance()
    {
        stance = equipment.GetOff().Stance;
    }

    public override void ResetClass()
    {
        base.ResetClass();

        equipment.ResetDefault();
        skills.ResetDefault();
    }

    public virtual void UpdateAllStats()
    {
        UpdateCombat();
    }

    public abstract void Death();
}
