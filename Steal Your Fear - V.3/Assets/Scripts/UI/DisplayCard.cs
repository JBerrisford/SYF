using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayCard : MonoBehaviour
{
    public Text damageText;
    public Text asText;
    public Text resistText;
    public Text healthText;
    public Text healthRegenText;
    public Text manaText;
    public Text manaRegenText;
    public Text rarityText;
    public Text levelText;
    public Text nameText;

    public void SetDisplay(EquipmentPanel.InfoDisplay pInfo)
    {
        damageText.text = pInfo.damage.ToString("F0");
        asText.text = pInfo.attackSpeed.ToString("F0");
        resistText.text = pInfo.resist.ToString("F0");
        healthText.text = pInfo.health.ToString("F0");
        manaText.text = pInfo.mana.ToString("F0");
        healthRegenText.text = pInfo.healthRegen.ToString("F0");
        manaRegenText.text = pInfo.manaRegen.ToString("F0");
        GetRarity(pInfo.rarity);
        levelText.text = pInfo.level.ToString();
        nameText.text = pInfo.name;
    }

    public void ResetDisplay()
    {
        damageText.text = "--";
        asText.text = "--";
        resistText.text = "--";
        healthText.text = "--";
        manaText.text = "--";
        healthRegenText.text = "--";
        manaRegenText.text = "--";
        rarityText.text = "--";
        levelText.text = "--";
        nameText.text = "--";
    }

    void GetRarity(CS_Enum.RARITY pRarity)
    {
        switch (pRarity)
        {
            case CS_Enum.RARITY.COMMON:
                rarityText.text = "Common";
                rarityText.color = Color.white;
                //gameObject.GetComponent<Image>().color = Color.white;
                break;
            case CS_Enum.RARITY.UNCOMMON:
                rarityText.text = "Uncommon";
                rarityText.color = Color.blue;
                //gameObject.GetComponent<Image>().color = Color.blue;
                break;
            case CS_Enum.RARITY.RARE:
                rarityText.text = "Rare";
                rarityText.color = Color.yellow;
                //gameObject.GetComponent<Image>().color = Color.yellow;
                break;
            case CS_Enum.RARITY.UNIQUE:
                rarityText.text = "Unique";
                rarityText.color = Color.magenta;
                //gameObject.GetComponent<Image>().color = Color.magenta;
                break;
            case CS_Enum.RARITY.FORBIDDEN:
                rarityText.text = "Forbidden";
                rarityText.color = Color.red;
                //gameObject.GetComponent<Image>().color = Color.red;
                break;
            default:
                rarityText.text = "N/A";
                rarityText.color = Color.black;
                //gameObject.GetComponent<Image>().color = Color.black;
                break;
        }
    }
}
