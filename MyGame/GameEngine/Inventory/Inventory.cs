using GameEngine;
using SFML.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.GameEngine.Inventory
{
    internal class Inventory : GameObject
    {
        private Vector2f scale = new Vector2f(4, 4);
        private const int sizex = 9;
        private const int sizey = 4;
        private ItemSlot[] backgrounds;
        public Inventory(Scene scene)
        {
            backgrounds = new ItemSlot[sizex * sizey];
            for(int i = 0; i < sizex * sizey; i++)
            {
                backgrounds[i] = new ItemSlot(this, i, scale, new Vector2f((i % sizex) * scale.X * 20, (i / sizex) * scale.Y * 20));
                backgrounds[i].SetItem(new Item(Game.Random.Next(4) - 1, Game.Random.Next(80) + 1));
                scene.AddUiElement(backgrounds[i]);
            }
        }
        public override void Draw()
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i].Draw();
            }
        }
        public override void Update(Time elapsed) { }
        public void SlotClicked(int ID)
        {
            if (backgrounds[ID]._item.ID != Game._Mouse.item.ID)
            {
                Item temp = backgrounds[ID]._item;
                backgrounds[ID].SetItem(Game._Mouse.item);
                Game._Mouse.SetItem(temp);
            }
            else if (ItemDat.GetStackSize(backgrounds[ID]._item.ID) >= backgrounds[ID]._item.amount + Game._Mouse.item.amount)
            {
                backgrounds[ID].SetItem(new Item(backgrounds[ID]._item.ID, backgrounds[ID]._item.amount + Game._Mouse.item.amount));
                Game._Mouse.SetItem(new Item(-1, 0));
            }
            else
            {
                Game._Mouse.SetItem(new Item(Game._Mouse.item.ID, (Game._Mouse.item.amount + backgrounds[ID]._item.amount) % ItemDat.GetStackSize(Game._Mouse.item.ID)));
                backgrounds[ID].SetItem(new Item(Game._Mouse.item.ID, ItemDat.GetStackSize(Game._Mouse.item.ID)));
            }
        }
    }
}
