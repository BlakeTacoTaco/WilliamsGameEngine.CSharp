using GameEngine;
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
        public const int itemCount = 2;
        public static void Initialize()
        {
            //textures
            textures = new Texture[itemCount];
            textures[0] = Game.GetTexture("../../../Resources/mouse test.png");
            textures[1] = Game.GetTexture("../../../Resources/high quality grass.png");

            //stack size
            stackSizes = new int[itemCount];
            stackSizes[0] = defaultStackSize;
            stackSizes[1] = defaultStackSize;

            //names
            names = new string[itemCount];
            names[0] = "smile";
            names[1] = "grass";

            //descriptions
            descriptions = new string[itemCount];
            descriptions[0] = "its a smile";
            descriptions[1] = "its some grass";
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
    }
}
