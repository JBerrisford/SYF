using UnityEngine;

public interface IItem
{
    CS_Enum.SLOT Slot
    { get; set; }
    CS_Enum.RARITY Rarity
    { get; set; }
    CS_Enum.STANCE Stance
    { get; set; }
    CS_Enum.ARMOUR_CLASS ArmourClass
    { get; set; }

    int Level
    { get; set; }

    float Damage
    { get; set; }
    float AttackSpeed
    { get; set; }
    float Resist
    { get; set; }
    float Health
    { get; set; }
    float HealthBonus
    { get; set; }
    float Mana
    { get; set; }
    float ManaBonus
    { get; set; }
    float HealthRegen
    { get; set; }
    float HealthRegenBonus
    { get; set; }
    float ManaRegen
    { get; set; }
    float ManaRegenBonus
    { get; set; }

    string Name
    { get; set; }

    MeshRenderer MyMesh
    { get; set; }
    Collider MyCollider
    { get; set; }

    IItem GetItem();
    void ItemCreation(CS_Struct.Item_Data pData);
    void Drop(Vector3 pPos);
    void Equip();
    void ToggleMesh(bool pToggle);

    Transform GetTransform();
    ITarget GetITarget();
    void SetParent(Transform pParent);
    CS_Struct.Item_Data GetData();
}

