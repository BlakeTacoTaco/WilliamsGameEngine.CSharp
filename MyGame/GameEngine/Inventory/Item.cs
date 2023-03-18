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
    }
}
