using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryButton : MonoBehaviour
{
    public Button btn;
    public Text text;

    int index;

    EquipmentPanel parent;

    public void SetUp(string pText, int pIndex, CS_Enum.RARITY pRarity, EquipmentPanel pParent)
    {
        text.text = pText;
        index = pIndex;
        parent = pParent;

        switch (pRarity)
        {
            case CS_Enum.RARITY.COMMON:
                text.color = Color.white;
                break;
            case CS_Enum.RARITY.UNCOMMON:
                text.color = Color.blue;
                break;
            case CS_Enum.RARITY.RARE:
                text.color = Color.yellow;
                break;
            case CS_Enum.RARITY.UNIQUE:
                text.color = Color.magenta;
                break;
            case CS_Enum.RARITY.FORBIDDEN:
                text.color = Color.red;
                break;
        }

        btn.onClick.AddListener(OnClick);
    } 

    void OnClick()
    {
        parent.SetSelected(index);
    }
}
