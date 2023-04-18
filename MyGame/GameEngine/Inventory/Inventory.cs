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
using SFML.Window;
using MyGame.GameEngine.General_UI;

namespace MyGame.GameEngine.Inventory
{
    internal class Inventory : GameObject
    {
        internal Vector2f scale = new Vector2f(4, 4);
        internal const int sizex = 9;
        internal const int sizey = 4;
        internal Item[] _items;
        public override void Draw() { }
        public override void Update(Time elapsed)
        {
        }
        public virtual void Sort()
        {
            int[] items = new int[ItemDat.itemCount];

            //counts up how many of each item there are and clears the inventory
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i].ID >= 0 && _items[i].ID < ItemDat.itemCount)
                {
                    items[_items[i].ID] += _items[i].amount;
                }
                _items[i] = new Item(-1,0);
            }

            //goes through every item type in order and adds it back to the inventory
            int currentSlot = 0;
            for(int i = 0; i < items.Length; i++)
            {
                while (items[i] > 0)
                {
                    _items[currentSlot] = new Item(i, items[i]);
                    items[i] -= ItemDat.GetStackSize(i);
                    currentSlot++;
                }
            }
        }
        public virtual void AddItem(Item item)
        {
            int i = 0;
            while(i < _items.Length && item.amount >= 0)
            {
                _items[i].AddItem(item);
                i++;
            }
            
        }
        public override Vector2f GetPosition()
        {
            throw new NotImplementedException();
        }
        public override void SetPosition(Vector2f position)
        {
            throw new NotImplementedException();
        }
    }
}
