﻿using UnityEngine;
using System.Collections;

public class WoolHeadWrap : Item_Base
{
    public override void ItemCreation(CS_Struct.Item_Data pData)
    {
        myData = pData;
        myData.name = "Tattered Helm";
        base.ItemCreation(pData);
    }
}
