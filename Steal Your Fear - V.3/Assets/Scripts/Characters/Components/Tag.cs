using UnityEngine;
using System.Collections;

using CS_Enum;

public class Tag : MonoBehaviour
{
    public GameObject nameTag;
    public TextMesh nameText;

    public void Init(string pName, CS_Enum.RARITY pRarity)
    {
        nameText.text = pName;
        SetColour(pRarity);
        nameTag.SetActive(false);
    }

    public void Tick()
    {
        nameTag.transform.LookAt(Player_Manager.Instance.playerCamera.transform, Vector3.up);
    }

    public void SetColour(RARITY pRarity)
    {
        switch (pRarity)
        {
            case RARITY.COMMON:
                nameText.color = Color.white;
                break;
            case RARITY.UNCOMMON:
                nameText.color = Color.blue;
                break;
            case RARITY.RARE:
                nameText.color = Color.yellow;
                break;
            case RARITY.UNIQUE:
                nameText.color = Color.magenta;
                break;
            case RARITY.FORBIDDEN:
                nameText.color = Color.red;
                break;
        }

        nameText.color = new Color(nameText.color.r, nameText.color.g, nameText.color.b, 0.5f);
    }
}
