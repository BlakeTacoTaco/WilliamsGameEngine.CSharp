using SFML.Graphics;
using GameEngine;
using System;
using SFML.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics.Glsl;

namespace MyGame.GameEngine.Inventory
{
    internal class Item
    {
        public int ID;
        public int amount;
        public Item(int ID, int amount)
        {
            this.ID = ID;
            this.amount = amount;
        }
        //makes sure the item isnt 3 airs or 0 rocks or something
        public void MakeValid()
        {
            if (ID == -1) { amount = 0; }
            if (amount <= 0) { ID = -1; }
            if(amount >= ItemDat.GetStackSize(ID)) { amount = ItemDat.GetStackSize(ID); }
        }
        public void AddItem(Item item)
        {
            if(item.ID == this.ID || this.ID == -1)
            {
                this.ID = item.ID;
                if (item.amount + amount <= ItemDat.GetStackSize(ID)) { this.amount += item.amount; item.amount = 0; }
                else { item.amount -= (ItemDat.GetStackSize(ID) - amount); amount = ItemDat.GetStackSize(ID); }
            }
            item.MakeValid();
            MakeValid();
        }
    }
}
