using UnityEngine;
using System.Collections;

using CS_Enum;
using CS_Struct;

[System.Serializable]
public class Item_Base : Target, IItem
{
    SLOT slot;
    public SLOT Slot
    { get { return slot; } set { slot = value; } }

    RARITY rarity;
    public RARITY Rarity
    { get { return rarity; } set { rarity = value; } }

    STANCE stance;
    public STANCE Stance
    { get { return stance; } set { stance = value; } }

    ARMOUR_CLASS armourClass;
    public ARMOUR_CLASS ArmourClass
    { get { return armourClass; } set { armourClass = value; } }

    int level;
    public int Level
    { get { return level; } set { level = value; } }

    float damage;
    public float Damage
    { get { return damage; } set { damage = value; } }

    float attackSpeed;
    public float AttackSpeed
    { get { return attackSpeed; } set { attackSpeed = value; } }

    float resist;
    public float Resist
    { get { return resist; } set { resist = value; } }

    float health;
    public float Health
    { get { return health; } set { health = value; } }

    float healthBonus;
    public float HealthBonus
    { get { return healthBonus; } set { healthBonus = value; } }

    float mana;
    public float Mana
    { get { return mana; } set { mana = value; } }

    float manaBonus;
    public float ManaBonus
    { get { return manaBonus; } set { manaBonus = value; } }

    float healthRegen;
    public float HealthRegen
    { get { return healthRegen; } set { healthRegen = value; } }

    float healthRegenBonus;
    public float HealthRegenBonus
    { get { return healthRegenBonus; } set { healthRegenBonus = value; } }

    float manaRegen;
    public float ManaRegen
    { get { return manaRegen; } set { manaRegen = value; } }

    float manaRegenBonus;
    public float ManaRegenBonus
    { get { return manaRegenBonus; } set { manaRegenBonus = value; } }

    string _name;
    public string Name
    { get { return _name; }
        set
        {
            _name = value;
            gameObject.name = _name;
        }
    }

    MeshRenderer myMesh;
    public MeshRenderer MyMesh
    { get { return myMesh; } set { myMesh = value; } }

    Collider myCollider;
    public Collider MyCollider
    { get { return myCollider; } set { myCollider = value; } }

    public Tag nameTag;
    public Item_Data myData;

    public override void Init()
    {
        base.Init();

        BasePriority = 2.0f;
        myMesh = transform.Find("Mesh").gameObject.GetComponent<MeshRenderer>();
        myCollider = gameObject.GetComponent<SphereCollider>();
        nameTag = transform.Find("Tag").gameObject.GetComponent<Tag>();
        nameTag.Init(Name, Rarity);
    }

    public override IEnumerator Tick()
    {
        while (IsActive)
        {
            UpdateTargetPriority(CS_Utility.GetDistance(gameObject));
            ToggleTag(true);
            nameTag.Tick();
            yield return null;
        }

        ToggleTag(false);
        ResetClass();
        yield break;
    }

    void ToggleTag(bool pToggle)
    {
        nameTag.gameObject.SetActive(pToggle);
    }

    public virtual void ItemCreation(Item_Data pData)
    {
        Name = myData.name;
        Rarity = (RARITY)myData.rarity;
        Init();

        switch(Rarity)
        {
            case RARITY.COMMON:
                MyMesh.material.color = Color.white;
                break;
            case RARITY.UNCOMMON:
                MyMesh.material.color = Color.blue;
                break;
            case RARITY.RARE:
                MyMesh.material.color = Color.yellow;
                break;
            case RARITY.UNIQUE:
                MyMesh.material.color = Color.magenta;
                break;
            case RARITY.FORBIDDEN:
                MyMesh.material.color = Color.red;
                break;
        }
    }

    public IItem GetItem()
    {
        return this;
    }

    public void Drop(Vector3 pPos)
    {
        transform.parent = null;
        transform.position = pPos;
        ToggleMesh(true);
    }

    public void ToggleMesh(bool pToggle)
    {
        nameTag.gameObject.SetActive(pToggle);
        gameObject.SetActive(pToggle);
        myCollider.enabled = pToggle;
        myMesh.enabled = pToggle;
    }

    public void Equip()
    {
        ToggleMesh(false);
        ToggleHighlight(false);
        gameObject.SetActive(true);
        myMesh.enabled = true;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public ITarget GetITarget()
    {
        return this;
    }

    public Item_Data GetData()
    {
        return myData;
    }

    public void SetParent(Transform pParent)
    {
        transform.parent = pParent;
        transform.localPosition = Vector3.zero;
    }

    protected void FinishStats(int pLevel)
    {
        //int range = Player_Manager.Instance.GetPlayer().GetLevel();
        //Level = range + Random.Range(-3, 4);

        Damage += Damage * (pLevel / 2.0f);
        Resist += Resist * (pLevel / 10.0f);
        AttackSpeed += AttackSpeed * (pLevel / 20.0f);

        Health += Health * (pLevel / 2.0f);
        HealthBonus += HealthBonus * (pLevel / 2.0f);
        HealthRegen += HealthRegen * (pLevel / 10.0f);
        HealthRegenBonus += HealthRegenBonus * (pLevel / 10.0f);

        Mana += Mana * (pLevel / 2.0f);
        ManaBonus += ManaBonus * (pLevel / 2.0f);
        ManaRegen += ManaRegen * (pLevel / 10.0f);
        ManaRegenBonus += ManaRegenBonus * (pLevel / 10.0f);

        float mod;

        switch (Rarity)
        {
            case RARITY.COMMON:
                mod = 0.0f;
                break;
            case RARITY.UNCOMMON:
                mod = 0.2f;
                break;
            case RARITY.RARE:
                mod = 0.375f;
                break;
            case RARITY.UNIQUE:
                mod = 0.5f;
                break;
            case RARITY.FORBIDDEN:
                mod = 1.0f;
                break;
            default:
                mod = 0.0f;
                break;
        }

        Damage += Damage * mod;
        Resist += Resist * mod;
        AttackSpeed += AttackSpeed * mod;

        Health += Health * mod;
        HealthBonus += HealthBonus * mod;
        HealthRegen += HealthRegen * mod;
        HealthRegenBonus += HealthRegenBonus * mod;

        Mana += Mana * mod;
        ManaBonus += ManaBonus * mod;
        ManaRegen += ManaRegen * mod;
        ManaRegenBonus += ManaRegenBonus * mod;
    }
}
