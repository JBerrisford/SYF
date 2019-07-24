using UnityEngine;

static class CS_Utility
{
    // Prefab directory for the enemies that are in the game.
    public static readonly string[] EnemyDirectory = { "Prefabs/Enemy/Enemy_Basic" };
    // Prefab directory for the items that are in the game.
    public static readonly string[] ItemDirectory = { "Prefabs/Item/Sword", "Prefabs/Item/TwoHandSword", "Prefabs/Item/Shield", "Prefabs/Item/Staff", "Prefabs/Item/Bow",
        "Prefabs/Item/Dagger", "Prefabs/Item/HeavyHead" , "Prefabs/Item/HeavyChest" , "Prefabs/Item/HeavyArms" , "Prefabs/Item/HeavyLegs", "Prefabs/Item/HeavyFeet",
        "Prefabs/Item/LightHead" , "Prefabs/Item/LightChest" , "Prefabs/Item/LightArms" , "Prefabs/Item/LightLegs", "Prefabs/Item/LightFeet",
        "Prefabs/Item/ClothHead" , "Prefabs/Item/ClothChest" , "Prefabs/Item/ClothArms" , "Prefabs/Item/ClothLegs", "Prefabs/Item/ClothFeet"};
    // Prefab directory for the items that are in the game.
    public static readonly string[] DefaultItemDirectory = { "Prefabs/Item/Default/Helm", "Prefabs/Item/Default/Chest", "Prefabs/Item/Default/Cape",
        "Prefabs/Item/Default/Arms", "Prefabs/Item/Default/Legs", "Prefabs/Item/Default/Boots", "Prefabs/Item/Default/MainWeapon", "Prefabs/Item/Default/OffWeapon"};

    // Scene directory. Will be used to randomise the current scene, to emulate random generation.
    public static readonly int[] BasementDirectory = { 0, 1, 2, 3, 4 };
    public static readonly int[] FirstFloorDirectory = { 5, 6, 7, 8, 9 };
    public static readonly int[] SecondFloorDirectory = { 10, 11, 12, 13, 14 };
    public static readonly int[] ThirdFloorDirectory = { 15, 16, 17, 18, 19 };
    public static readonly int[] TopFloorDirectory = { 20, 21, 22, 23, 24};

    // Component directory. Most items here will be required for initilization of a bunch of the classes. 
    public static readonly string highlight = "Prefabs/Component/Highlight";
    public static readonly string mounting = "Prefabs/Component/Character Mounting Points";
    public static readonly string player = "Prefabs/Player";
    public static readonly string ui = "Prefabs/UI";
    public static readonly string itemPool = "Prefabs/ItemPool";

    public static readonly string btnTemplate = "Prefabs/btn";

    // Utility functioality that will be used project wide.

    // Return the distance between the object and the player. 
    public static float GetDistance(GameObject pObj)
    {
        return Vector3.Distance(Player_Manager.Instance.GetPlayerGO().transform.position, pObj.transform.position);
    }
}

