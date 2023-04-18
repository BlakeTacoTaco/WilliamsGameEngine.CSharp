using GameEngine;
using MyGame.GameEngine.Item_Uses;
using MyGame.GameEngine.ItemUses;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.Inventory
{
    static class ItemDat
    {
        private const int defaultStackSize = 99;
        private static Texture[] textures;
        private static int[] stackSizes;
        private static string[] names;
        private static string[] descriptions;
        private static ItemUse[] itemFunctions;
        public const int itemCount = 3;
        public static void Initialize()
        {
            //initialize data
            textures = new Texture[itemCount];
            stackSizes = new int[itemCount];
            names = new string[itemCount];
            descriptions = new string[itemCount];
            itemFunctions = new ItemUse[itemCount];
            

            //smile
            textures[0] = Game.GetTexture("../../../Resources/mouse test.png");
            stackSizes[0] = defaultStackSize;
            names[0] = "silly guy";
            descriptions[0] = "got too silly";
            itemFunctions[0] = new PlaceTile(0);

            //grass
            textures[1] = Game.GetTexture("../../../Resources/high quality grass.png");
            stackSizes[1] = defaultStackSize;
            names[1] = "grass";
            descriptions[1] = "its some grass";
            itemFunctions[1] = new PlaceTile(1);

            //samon
            textures[2] = Game.GetTexture("../../../Resources/samon.png");
            stackSizes[2] = defaultStackSize;
            names[2] = "samon";
            descriptions[2] = "without the L";
            itemFunctions[2] = new PlaceTile(-1);
        }
        //gets texture baed on item ID
        public static Texture GetTexture(int ID)
        {
            if (ID < itemCount && ID >= 0) { return textures[ID]; }
            else if (ID == -1) { return Game.GetTexture("../../../Resources/nothing.png"); }
            return Game.GetTexture("../../../Resources/null.png");
        }
        //gets stacksize based on item ID
        public static int GetStackSize(int ID)
        {
            if (ID < itemCount && ID >= 0) { return stackSizes[ID]; }
            else if (ID == -1) { return 0; }
            return defaultStackSize;
        }
        public static string GetName(int ID)
        {
            if (ID < itemCount && ID >= 0) { return names[ID]; }
            else if (ID == -1) { return null; }
            return "null";
        }
        public static string GetDesc(int ID)
        {
            if (ID < itemCount && ID >= 0) { return descriptions[ID]; }
            else if (ID == -1) { return null; }
            return "null";
        }
        public static void UseItem()
        {
            if (Game._Mouse.item.ID >= 0)
            {
                itemFunctions[Game._Mouse.item.ID].Use();
            }
        }
    }
}
