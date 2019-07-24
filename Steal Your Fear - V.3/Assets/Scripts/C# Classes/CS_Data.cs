using System.Collections.Generic;

namespace CS_Enum
{
    // Possible states for the overall game.
    public enum OVERALL_STATE
    {
        START = 0,
        MAIN_MENU,
        NEW_SCENE,
        TOWN,
        SAVE,
        EXIT
    }
    // Possible states for  ingame.
    public enum INGAME_STATE
    {
        START_LEVEL,
        PLAYING,
        LOSE,
        WIN
    }
    // Gets the stance of the character to get the correct damage value.
    public enum STANCE
    {
        ONE_HANDED = 0,
        TWO_HANDED,
        DUAL_WIELD,
        MAGIC,
        ARCHERY,
        OTHER
    }
    // Gets the most prominant armour for the character
    public enum ARMOUR_CLASS
    {
        HEAVY = 0,
        LIGHT,
        CLOTH,
        OTHER
    }
    // Gets the other skill levels, such as health and mana
    public enum OTHER
    {
        HEALTH = 0,
        MANA,
        HEALTH_REGEN,
        MANA_REGEN
    }

    // Item slots. Used mainly to define the slot items will occupy when equipt.
    public enum SLOT
    {
       HEAD = 0,
       CHEST,
       ARMS,
       LEGS,
       FEET,
       BACK,
       MAIN_WEAPON,
       OFF_WEAPON
    }
    // Rarity for the game. Rarity will effect the power of enemies and items.
    public enum RARITY
    {
        COMMON = 0, // No boost to stats --- 45% Chance of occuring
        UNCOMMON, // Used to boost the stats by about 20%. --- 35% Chance of occuring
        RARE, // Used to boost the stats by about 37.5%. --- 14% Chance of occuring
        UNIQUE, // Used to boost the stats by about 50%. --- 5% Chance of occuring 
        FORBIDDEN // Used to boost the stats by about 100%. --- 1% Chance of occuring
    }

    public enum SKILL
    {
        ONE = 0,
        TWO,
        DUAL,
        MAGIC,
        ARCHERY,
        HEAVY,
        LIGHT,
        CLOTH,
        HEALTH,
        HEATLH_REGEN,
        MANA,
        MANA_REGEN
    }

    public enum CHANGE_ZONE
    {
        ENTRANCE = 0,
        F_ONE,
        F_TWO,
        F_THREE,
        BASEMENT,
        VAULT,
        GARDEN,
        TOWN
    }
}

namespace CS_Struct
{
    [System.Serializable]
    public struct Character_Data
    {
        public string name;
        public int saveIndex;

        public Inventory_Data itemData;
        public Skill_Data skillData;
    }
    [System.Serializable]
    public struct Item_Data
    {
        public string name;
        public int level;
        public int rarity;
        public string item;

        public Item_Data(int pLevel, int pRarity, string pItemIndex)
        {
            name = "-/-";
            level = pLevel;
            rarity = pRarity;
            item = pItemIndex;
        }
    }
    [System.Serializable]
    public struct Skill_Data
    {
        public int oneHanded;
        public int twoHanded;
        public int dualWield;
        public int magic;
        public int archery;

        public int health;
        public int mana;
        public int healthRegen;
        public int manaRegen;

        public int heavyArmour;
        public int lightArmour;
        public int clothArmour;

        public int level;
        public int experience;
        public int points;

        public Skill_Data(int OH, int TH, int DW, int MA, int AR, int H, int M, int HR, int MR, int HA, int LA, int CA, int LVL, int XP, int P)
        {
            oneHanded = OH;
            twoHanded = TH;
            dualWield = DW;
            magic = MA;
            archery = AR;

            health = H;
            mana = M;
            healthRegen = HR;
            manaRegen = MR;

            heavyArmour = HA;
            lightArmour = LA;
            clothArmour = CA;

            level = LVL;
            experience = XP;
            points = P;
        }
    }
    [System.Serializable]
    public struct Inventory_Data
    {
        public List<Item_Data> inventory;
        public List<Item_Data> equipment;

        public Inventory_Data(List<Item_Data> pInventory, List<Item_Data> pEquipment)
        {
            inventory = pInventory;
            equipment = pEquipment;
        }
    }
}
