using GameEngine;
using MyGame.GameEngine.Item_Uses;
using MyGame.GameEngine.ItemUses;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.Inventory
{
    static class ItemDat
    {
        internal const int defaultStackSize = 99;
        internal static readonly Vector2f defaultItemScale = new Vector2f(4, 4);
        internal static Texture[] textures;
        internal static int[] stackSizes;
        internal static string[] names;
        internal static string[] descriptions;
        internal static ItemUse[] itemFunctions;
        internal static Vector2f[] itemScales;
        public const int itemCount = 5;
        public static void Initialize()
        {
            //initialize data
            textures = new Texture[itemCount];
            stackSizes = new int[itemCount];
            names = new string[itemCount];
            descriptions = new string[itemCount];
            itemFunctions = new ItemUse[itemCount];
            itemScales = new Vector2f[itemCount];
            

            //smile
            textures[0] = Game.GetTexture("../../../Resources/mouse test.png");
            stackSizes[0] = defaultStackSize;
            names[0] = "silly guy";
            descriptions[0] = "got too silly";
            itemFunctions[0] = new PlaceTile(0);
            itemScales[0] = defaultItemScale;

            //grass
            textures[1] = Game.GetTexture("../../../Resources/high quality grass.png");
            stackSizes[1] = defaultStackSize;
            names[1] = "grass";
            descriptions[1] = "its some grass";
            itemFunctions[1] = new PlaceTile(1);
            itemScales[1] = defaultItemScale;

            //samon
            textures[2] = Game.GetTexture("../../../Resources/samon.png");
            stackSizes[2] = defaultStackSize;
            names[2] = "samon";
            descriptions[2] = "without the L";
            itemFunctions[2] = new PlaceTile(2);
            itemScales[2] = defaultItemScale;

            //pickaxe
            textures[3] = Game.GetTexture("../../../Resources/pickaxe.png");
            stackSizes[3] = 1;
            names[3] = "pickaxe";
            descriptions[3] = "lets you break things other than the floor";
            itemFunctions[3] = new RemoveTileEntity();
            itemScales[3] = defaultItemScale;

            //Tables
            textures[4] = Game.GetTexture("../../../Resources/2x2table.png");
            stackSizes[4] = 99;
            names[4] = "Table";
            descriptions[4] = "places a table";
            itemFunctions[4] = new PlaceTileEntity();
            itemScales[4] = new Vector2f(2,2);
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
        public static Vector2f GetScale(int ID)
        {
            if (ID < itemCount && ID >= 0) { return itemScales[ID]; }
            return defaultItemScale;
        }
        public static void UseItem()
        {
            if (Game._Mouse.item.ID >= 0)
            {
                if (itemFunctions[Game._Mouse.item.ID] != null)
                {
                    itemFunctions[Game._Mouse.item.ID].Use();
                }
            }
        }

    }
}
