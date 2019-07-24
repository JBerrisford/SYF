using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EquipmentPanel : MonoBehaviour
{
    public struct InfoDisplay
    {
        public float damage, attackSpeed, resist, health, mana, healthRegen, manaRegen;
        public  int level;
        public CS_Enum.RARITY rarity;
        public string name;

        public InfoDisplay(float pDamage, float pAttackSpeed, float pResist, float pHealth, float pMana, float pHealthRegen, float pManaRegen, int pLevel, CS_Enum.RARITY pRarity, string pName)
        {
            damage = pDamage;
            attackSpeed = pAttackSpeed;
            resist = pResist;
            health = pHealth;
            mana = pMana;
            healthRegen = pHealthRegen;
            manaRegen = pManaRegen;
            rarity = pRarity;
            name = pName;
            level = pLevel;
        }
    }

    public DisplayCard current;
    public DisplayCard selected;

    public GameObject btnTemplate;
    public GameObject content;

    public Text invSpace;

    int selectedIndex;

    public List<GameObject> contents = new List<GameObject>();
    public List<GameObject> buttons = new List<GameObject>();

    public void DisplayArmourPiece(int pSlot)
    {
        IItem item = Player_Manager.Instance.GetPlayer().GetInventory().equipment.currentEquipment[(CS_Enum.SLOT)pSlot];
        current.SetDisplay(new InfoDisplay(item.Damage, item.AttackSpeed, item.Resist, item.Health, item.Mana, item.HealthRegen, item.ManaRegen, item.Level, item.Rarity, item.Name));
        selected.ResetDisplay();
    }

    public void SetSelected(int pIndex)
    {
        IItem item = Player_Manager.Instance.GetPlayer().GetInventory().inventory[pIndex];
        DisplayArmourPiece((int)item.Slot);
        selected.SetDisplay(new InfoDisplay(item.Damage, item.AttackSpeed, item.Resist, item.Health, item.Mana, item.HealthRegen, item.ManaRegen, item.Level, item.Rarity, item.Name));
        //buttons[pIndex].GetComponent<Image>().color = SetColor(item.Rarity);
        selectedIndex = pIndex;
    }

    public void InventoryUpdate()
    {
        Inventory inventory = Player_Manager.Instance.GetPlayer().GetInventory();

        for (int i = 0; i < contents.Count; i++)
        {
            Destroy(contents[i]);
        }

        contents.Clear();

        foreach (IItem temp in inventory.inventory)
        {
            GameObject go = (GameObject)Instantiate(btnTemplate);
            contents.Add(go);
            go.SetActive(true);

            go.transform.SetParent(content.transform, false);

            InventoryButton tempBtn = go.GetComponent<InventoryButton>();
            tempBtn.SetUp(temp.Name, inventory.inventory.IndexOf(temp), temp.Rarity, this);
        }

        foreach(GameObject temp in buttons)
        {
            temp.GetComponent<Image>().color = SetColor(Player_Manager.Instance.GetPlayer().GetInventory().equipment.currentEquipment[(CS_Enum.SLOT)buttons.IndexOf(temp)].Rarity);
        }

        invSpace.text = inventory.inventory.Count.ToString("F0") + " / " + inventory.inventoryCapacity;
    }

    public void DropItem()
    {
        Player_Manager.Instance.DropItem(selectedIndex);
    }

    public void EquipItem()
    {
        Player_Manager.Instance.EquipItem(selectedIndex);
    }

    Color SetColor(CS_Enum.RARITY pRarity)
    {
        switch (pRarity)
        {
            case CS_Enum.RARITY.COMMON:
                return Color.white;                
            case CS_Enum.RARITY.UNCOMMON:
                return Color.blue;
            case CS_Enum.RARITY.RARE:
                return Color.yellow;
            case CS_Enum.RARITY.UNIQUE:
                return Color.magenta;
            case CS_Enum.RARITY.FORBIDDEN:
                return Color.red;
        }

        return Color.black;
    }
}
